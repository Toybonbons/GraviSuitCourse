using UnityEngine;

public class PitTrigger : MonoBehaviour
{
    PlayerMain playerController;

    void Start()
    {
        playerController = PlayerMain.instance;
    }


    void OnTriggerEnter(Collider other)
    {
        playerController.pitBoost();
    }
}
