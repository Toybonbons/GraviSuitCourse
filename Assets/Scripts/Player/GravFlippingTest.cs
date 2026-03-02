using UnityEngine;

public class GravFlippingTest : MonoBehaviour
{
    public Vector3 targetGrav;

    PlayerMain playerController;

    void Start()
    {
        playerController = PlayerMain.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        playerController.changeGravity(targetGrav);
    }
}
