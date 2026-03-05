using System.Collections;
using UnityEngine;

public class GraviWalls : MonoBehaviour
{
    public Vector3 targetGrav;
    public float cooldown = 0f,maxCooldown = 0.25f;


    PlayerMain playerController;

    void Start()
    {
        playerController = PlayerMain.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER!");

        if (cooldown == 0)
        {
            cooldown = maxCooldown;
            playerController.changeGravity(targetGrav);

            StartCoroutine(cooldownFunc());
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT!");
        if (cooldown == 0)
        {
            cooldown = maxCooldown;
            playerController.changeGravity(Vector3.down);

            StartCoroutine(cooldownFunc());
        }
        
    }

    IEnumerator cooldownFunc()
    {
        yield return new WaitForSeconds(maxCooldown);

        cooldown = 0f;
    }
}
