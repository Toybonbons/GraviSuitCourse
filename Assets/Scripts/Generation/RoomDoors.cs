using System;
using UnityEngine;

public class RoomDoors : MonoBehaviour
{
    private bool opened;
    private EndlessGen genScript;

    [SerializeField] Animator doorAnim;

    void Start()
    {
        genScript = EndlessGen.instance;
    }

    //Triggers
    void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        opened = true;
        openDoor();
    }


    //Door Func
    void openDoor()
    {
        doorAnim.SetTrigger("Open");
        genScript.genIncrement();
    }
}
