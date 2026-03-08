using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessGen : MonoBehaviour
{
    [Header("Room Lists")]
    [SerializeField] GameObject testRoom;
    [SerializeField] RoomLists roomLists;
    [SerializeField] GameObject doorTemp;

    [Header("Storage Places")]
    [SerializeField] GameObject genRoomStorage;

    [Header("Values")]
    public int currentRoom = 0;
    private int playerInRoom = 0;
    public List<GameObject> activeRooms;

    [Header("Debug Config")]
    [SerializeField] float roomCount;
    [SerializeField] int maxLoadedRooms = 5;

    private Vector3 nextAnchor;


    public static EndlessGen instance;


    //Unity Func

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        nextAnchor = new Vector3(0, 0.5f,0);

        createNewRoom();
    }

    //Generation Func

    void testGeneration()
    {
        for (int i = 0; i < roomCount; i++)
        {
            Debug.Log($"GEN ROOM {i + 1}");
            createNewRoom();
        }
    }

    public void genIncrement()
    {
        createNewRoom();

        playerInRoom += 1;

        if (playerInRoom - maxLoadedRooms > 0)
        {
            unloadLastRoom();
        }
    }

    void createNewRoom()
    {
        GameObject roomModel = Instantiate(selectRoomDiff());
        Transform roomTrans = roomModel.transform;

        Transform nodeStorage = roomTrans.Find("Nodes");
        Transform startNode = nodeStorage.Find("StartNode");
        Transform endNode = nodeStorage.Find("EndNode");

        Vector3 startOffset = roomTrans.position - startNode.position;

        roomTrans.position = nextAnchor + startOffset;

        roomTrans.parent = genRoomStorage.transform;
        nextAnchor = endNode.position;

        //Door
        createNewDoor(endNode);

        if (currentRoom == 0) createNewDoor(startNode);

        //Saving
        activeRooms.Add(roomModel);
        currentRoom += 1;
    }

    void unloadLastRoom()
    {
        GameObject room = activeRooms[0];
        activeRooms.RemoveAt(0);

        Destroy(room);
    }

    //Room Selection

    GameObject selectRoomDiff()
    {
        string chosenDiff = "easy";

        return chooseRoom(chosenDiff);
    }

    GameObject chooseRoom(string diff)
    {
        GameObject[] chosenRoomTable = roomLists.easyRooms;

        int randNum = Random.Range(0, chosenRoomTable.Count());

        return chosenRoomTable[randNum];
    }

    //Door Gen

    void createNewDoor(Transform node)
    {
        GameObject door = Instantiate(doorTemp);

        door.transform.SetPositionAndRotation(node.position, node.rotation);
        door.transform.parent = node;
    }

}
