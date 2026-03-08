using System;
using UnityEngine;

public class RoomDoors : MonoBehaviour
{
    private bool opened;
    private EndlessGen genScript;

    [SerializeField] GameObject doorMesh;

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
        genScript.genIncrement();

        Destroy(doorMesh);
    }
}
