using UnityEngine;

[CreateAssetMenu(fileName = "RoomLists", menuName = "Scriptable Objects/RoomLists")]
public class RoomLists : ScriptableObject
{
    [Header("Normal Lists")]
    public GameObject[] easyRooms;
    public GameObject[] mediumRooms;
    public GameObject[] graviRooms;

}
