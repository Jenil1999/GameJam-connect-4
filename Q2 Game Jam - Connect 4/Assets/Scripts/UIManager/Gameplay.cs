using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : UIScreen
{
    public void CloseBTN()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.HomeScreen);
        GameManager.GMinst.gameend();
    }

    public void Playagain()
    {
        UIManager.instUIM.winOff();
        GridManager.gridMinst.Reset();
    }




}
