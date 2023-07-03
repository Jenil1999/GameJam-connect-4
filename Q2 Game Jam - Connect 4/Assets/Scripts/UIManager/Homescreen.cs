using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homescreen : UIScreen
{
    public GameObject P2;
    public GameObject MuteBTN;
    public GameObject UnMuteBTN;

    public void BotGame()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(false);
        UIManager.instUIM.levelSelectorOff();
        GameManager.GMinst.gamebotstart();
        GridManager.gridMinst.Reset();
    }

    public void PvPGame()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(true);
        UIManager.instUIM.levelSelectorOff();

        GameManager.GMinst.gamestart();
        GridManager.gridMinst.Reset();
    }

    public void OpenLvlSelect()
    {
        UIManager.instUIM.levelSelectorOn();
    }

    public void fourPlayerGame()
    {
        Debug.Log("4 player mode Soon ");
    }

    public void Mute()
    {
        MuteBTN.SetActive(false);
        UnMuteBTN.SetActive(true);
    }

    public void Unmute()
    {
        MuteBTN.SetActive(true);
        UnMuteBTN.SetActive(false);
    }

    public void Outside()
    {
        UIManager.instUIM.levelSelectorOff();
    }

}
