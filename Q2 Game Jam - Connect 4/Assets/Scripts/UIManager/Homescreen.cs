using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Homescreen : UIScreen
{
    public GameObject P2;
    public GameObject Pbot;
    [Range(0.1f, 2)]
    public float _animeDur = 0.2f;

    public GameObject ShopBTN;
    public Transform ShopBTNPos;
    public GameObject MuteBTN;
    public Transform MuteBTNPos;
    public Transform MuteBTNstartPos;
    public GameObject UnMuteBTN;
    public GameObject Logo;
    public Transform LogostartPos;
    public Transform LogoPos;
    public GameObject PvBBTN;
    public Transform PvBBTNstartPos;
    public Transform PvBBTNPos;
    public GameObject PvPBTN;
    public Transform PvPBTNstartPos;
    public Transform PvPBTNPos;

    private void Start()
    {
        MuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.5f);
        UnMuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.5f);
        ShopBTN.transform.DOMove(ShopBTNPos.position, _animeDur - 0.5f);
        Logo.transform.DOMove(LogoPos.position/*new Vector2(Logo.transform.position.x, Logo.transform.position.y - 1139)*/, _animeDur + 0.2f).SetEase(Ease.OutBounce).OnComplete(() => StartIdle());
        PvBBTN.transform.DOMove(PvBBTNPos.position, _animeDur - 0.2f);
        PvPBTN.transform.DOMove(PvPBTNPos.position, _animeDur - 0.2f);
    }

    public void AnimOnStart()
    {
        MuteBTN.transform.position = new Vector2(MuteBTNstartPos.position.x, MuteBTNstartPos.position.y); ;
        UnMuteBTN.transform.position = new Vector2(MuteBTNstartPos.position.x, MuteBTNstartPos.position.y);
        PvBBTN.transform.position = new Vector2(PvBBTNstartPos.position.x, PvBBTNstartPos.position.y);
        PvPBTN.transform.position = new Vector2(PvPBTNstartPos.position.x, PvPBTNstartPos.position.y);
        MuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.9f);
        UnMuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.9f);
        ShopBTN.transform.DOMove(ShopBTNPos.position, _animeDur - 0.5f);
        PvBBTN.transform.DOMove(PvBBTNPos.position, _animeDur - 0.5f);
        PvPBTN.transform.DOMove(PvPBTNPos.position, _animeDur - 0.5f);
    }

    public void AnimOnExitBotgame()
    {
        MuteBTN.transform.DOMove(MuteBTNstartPos.position, _animeDur - 0.9f);
        UnMuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.9f);
        PvBBTN.transform.DOMove(PvBBTNstartPos.position, _animeDur - 0.5f).OnComplete(() => BotGame()); 
        PvPBTN.transform.DOMove(PvPBTNstartPos.position, _animeDur - 0.5f);
    }
    public void AnimOnExitHardBotgame()
    {
        MuteBTN.transform.DOMove(MuteBTNstartPos.position, _animeDur - 0.9f);
        UnMuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.9f);
        PvBBTN.transform.DOMove(PvBBTNstartPos.position, _animeDur - 0.5f).OnComplete(() => HardBotGame());
        PvPBTN.transform.DOMove(PvPBTNstartPos.position, _animeDur - 0.5f);
    }
    public void AnimOnExitPvPgame()
    {
        MuteBTN.transform.DOMove(MuteBTNstartPos.position, _animeDur - 0.9f);
        UnMuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.9f);
        PvBBTN.transform.DOMove(PvBBTNstartPos.position, _animeDur - 0.2f).OnComplete(() => PvPGame());
        PvPBTN.transform.DOMove(PvPBTNstartPos.position, _animeDur - 0.2f);
    }


    public void StartIdle()
    {
        Logo.transform.DOMove(new Vector3(Logo.transform.position.x, Logo.transform.position.y - 30, Logo.transform.position.z), 0.8f).SetLoops(-1, loopType: LoopType.Yoyo).OnComplete(() => StartIdle());
    }

    public void ShopOpen()
    {
        Debug.Log("Shop soon");
    }

    public void BotGame()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(false);
        Pbot.SetActive(true);
        UIManager.instUIM.LevelSelectorOff();
        UIManager.instUIM.BotSelectorOff();
        GameManager.GMinst.Gamebotstart();
        GridManager.gridMinst.Reset();
        PvPBTN.SetActive(true);
    }

    public void HardBotGame()
    {
        //Debug.Log("Hard mode Soon ");
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(false);
        Pbot.SetActive(true);
        UIManager.instUIM.LevelSelectorOff();
        UIManager.instUIM.BotSelectorOff();
        GameManager.GMinst.Gamebotstart();
        GridManager.gridMinst.Reset();
        PvPBTN.SetActive(true);
        GameManager.IsBotDifHard = true;
    }


    public void PvPGame()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(true);
        Pbot.SetActive(false);
        UIManager.instUIM.LevelSelectorOff();
        UIManager.instUIM.BotSelectorOff();
        GameManager.GMinst.Gamestart();
        GridManager.gridMinst.Reset();
    }

    public void FourPlayerGame()
    {
        Debug.Log("4 player mode Soon ");
    }


    public void OpenLvlSelect()
    {
        UIManager.instUIM.LevelSelectorOn();
        UIManager.instUIM.BotSelectorOff();
    }
    public void OpenBotSelect()
    {
        UIManager.instUIM.BotSelectorOn();
        UIManager.instUIM.LevelSelectorOff();
        PvPBTN.SetActive(false);
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
        UIManager.instUIM.LevelSelectorOff();
        UIManager.instUIM.BotSelectorOff();
        PvPBTN.SetActive(true);
    }

}
