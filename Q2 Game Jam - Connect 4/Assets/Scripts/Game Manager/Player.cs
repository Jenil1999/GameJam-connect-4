using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Plyenum
{
    Player1,
    Player2,
    Player3,
    Player4,
    playerBOT
}



[System.Serializable]
public class Player : MonoBehaviour
{
    public string playerName;
    public Plyenum Number;
}
