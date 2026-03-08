using System.Collections;
using UnityEngine;

public class GraviWalls : MonoBehaviour
{
    public Vector3 targetGrav;
    public float cooldown = 0f,maxCooldown = 0.25f;
    private bool enterState, appliedState;


    PlayerMain playerController;

    void Start()
    {
        playerController = PlayerMain.instance;
    }

    //Triggers
    void OnTriggerEnter(Collider other)
    {
        enterState = true;

        if (cooldown == 0) applyGrav();
    }

    void OnTriggerExit(Collider other)
    {
        enterState = false;

        if (cooldown == 0) resetGrav();
    }

    //GravApplication

    void applyGrav()
    {
        appliedState = true;
        cooldown = maxCooldown;

        playerController.changeGravity(targetGrav);

        StartCoroutine(cooldownFunc());
    }

    void resetGrav()
    {
        appliedState = false;
        cooldown = maxCooldown;

        playerController.changeGravity(Vector3.down);

        StartCoroutine(cooldownFunc());
    }

    //Cooldown
    IEnumerator cooldownFunc()
    {
        yield return new WaitForSeconds(maxCooldown);

        cooldown = 0f;
        
        if (appliedState != enterState)
        {
            Debug.Log("APPLIED GRAVITY IS DIFFERENT TO CORRECT ONE!");

            if (enterState) applyGrav();
            else resetGrav();
        }
    }
}
