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
    private GameObject lastRoom;

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

    List<GameObject> getRoomTable()
    {
        List<GameObject> roomList = new List<GameObject>();

        roomList.AddRange(roomLists.easyRooms);

        if (currentRoom >= roomLists.mediumUnlock) roomList.AddRange(roomLists.mediumRooms);
        if (currentRoom >= roomLists.hardUnlock) roomList.AddRange(roomLists.hardRooms);

        if (lastRoom) roomList.Remove(lastRoom);

        return roomList;
    }

    GameObject chooseRoom(string diff)
    {
        List<GameObject> chosenRoomTable = getRoomTable();

        int randNum = Random.Range(0, chosenRoomTable.Count());

        lastRoom = chosenRoomTable[randNum];
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
