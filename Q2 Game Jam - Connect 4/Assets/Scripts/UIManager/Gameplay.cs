using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : UIScreen
{
    public void CloseBTN()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.HomeScreen);
        GameManager.GMinst.Gameend();
        UIManager.instUIM.WinOff();
        GameManager.IsBotDifHard = false;

    }

    public void Playagain()
    {
        AudioManager.AM.PlayResetAudio();
        UIManager.instUIM.WinOff();
        UIManager.instUIM.DrawScreenOff();
        GridManager.gridMinst.Reset();
        GameManager.GMinst.Reset();
    }



    //public Image img;
    //public float waitTime = 10f;
    //private void Update()
    //{
    //    img.fillAmount -= 1.0f / waitTime * Time.deltaTime;
    //}



}
