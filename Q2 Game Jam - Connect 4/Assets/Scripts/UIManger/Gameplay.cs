using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public void CloseBTN()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.HomeScreen);
        InputManager.IMinst.gameend();
    }
}
