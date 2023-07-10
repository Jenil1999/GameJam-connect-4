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
        AudioManager.AM.PlayResetAudio();
        UIManager.instUIM.WinOff();
        UIManager.instUIM.DrawScreenOff();
        GridManager.gridMinst.Reset();
        GameManager.GMinst.Reset();
    }




}
