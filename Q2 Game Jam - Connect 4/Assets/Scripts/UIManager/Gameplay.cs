using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : UIScreen
{
    public void CloseBTN()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.HomeScreen);
        GameManager.GMinst.Gameend();
        UIManager.instUIM.WinOff();
    }

    public void Playagain()
    {
        UIManager.instUIM.WinOff();
        GridManager.gridMinst.Reset();
        GameManager.GMinst.Reset();
    }




}
