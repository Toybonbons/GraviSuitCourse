using UnityEngine;

public class EndlessGen : MonoBehaviour
{
    [Header("Room Lists")]
    [SerializeField] GameObject testRoom;

    [Header("Storage Places")]
    [SerializeField] GameObject genRoomStorage;


    [Header("Debug Config")]
    [SerializeField] float roomCount;

    private Vector3 nextAnchor;


    //Unity Func

    void Start()
    {
        nextAnchor = new Vector3(0, 0.5f,0);

        testGeneration();
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

    void createNewRoom()
    {
        GameObject roomModel = Instantiate(testRoom);
        Transform roomTrans = roomModel.transform;

        Transform nodeStorage = roomTrans.Find("Nodes");
        Transform startNode = nodeStorage.Find("StartNode");
        Transform endNode = nodeStorage.Find("EndNode");

        Vector3 startOffset = roomTrans.position - startNode.position;

        roomTrans.position = nextAnchor + startOffset;

        roomTrans.parent = genRoomStorage.transform;
        nextAnchor = endNode.position;
    }

    //Room Positioning


}
