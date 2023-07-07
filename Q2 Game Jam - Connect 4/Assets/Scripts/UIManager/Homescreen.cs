using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Homescreen : UIScreen
{
    public GameObject P2;
    [Range(0.1f, 2)]
    public float _animeDur = 0.2f;

    [Header("Animated UI")] // Remember to remove it once done
    public GameObject ShopBTN;
    public Transform ShopBTNPos;
    public GameObject MuteBTN;
    public Transform MuteBTNPos;
    public GameObject UnMuteBTN;
    public GameObject Logo;
    public Transform LogoPos;
    public GameObject PvBBTN;
    public Transform PvBBTNPos;
    public GameObject PvPBTN;
    public Transform PvPBTNPos;

    private void Start()
    {
        MuteBTN.transform.DOMove(MuteBTNPos.position, _animeDur - 0.5f);
        ShopBTN.transform.DOMove(ShopBTNPos.position, _animeDur - 0.5f);
        Logo.transform.DOMove(LogoPos.position, _animeDur + 0.2f).SetEase(Ease.OutBounce).OnComplete(() => StartIdle());
        PvBBTN.transform.DOMove(PvBBTNPos.position, _animeDur - 0.2f);
        PvPBTN.transform.DOMove(PvPBTNPos.position, _animeDur - 0.2f);
    }

    public void StartIdle()
    {
        //Logo.transform.DOMove(LogoPos.position, _animeDur + 0.2f).SetEase(Ease.OutBounce);
        Logo.transform.DOMove(new Vector3(LogoPos.position.x, LogoPos.position.y - 30, LogoPos.position.z), 0.8f).SetLoops(-1, loopType: LoopType.Yoyo);
        PvBBTN.transform.DOMove(new Vector3(PvBBTNPos.position.x, PvBBTNPos.position.y - 30, PvBBTNPos.position.z), 0.8f).SetLoops(-1, loopType: LoopType.Yoyo);
        PvPBTN.transform.DOMove(new Vector3(PvPBTNPos.position.x, PvPBTNPos.position.y - 30, PvPBTNPos.position.z), 0.8f).SetLoops(-1, loopType: LoopType.Yoyo).OnComplete(() => StartIdle()); ;
        
    }

    public void ShopOpen()
    {
        Debug.Log("Shop soon");
    }

    public void BotGame()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(false);
        UIManager.instUIM.levelSelectorOff();
        UIManager.instUIM.BotSelectorOff();
        GameManager.GMinst.gamebotstart();
        GridManager.gridMinst.Reset();
        PvPBTN.SetActive(true);
    }

    public void PvPGame()
    {
        UIManager.instUIM.SwitchScreen(ScreenType.Gameplay);
        P2.SetActive(true);
        UIManager.instUIM.levelSelectorOff();
        UIManager.instUIM.BotSelectorOff();

        GameManager.GMinst.gamestart();
        GridManager.gridMinst.Reset();
    }

    public void OpenLvlSelect()
    {
        UIManager.instUIM.levelSelectorOn();
        UIManager.instUIM.BotSelectorOff();
    }
    public void OpenBotSelect()
    {
        UIManager.instUIM.BotSelectorOn();
        UIManager.instUIM.levelSelectorOff();
        PvPBTN.SetActive(false);
    }

    public void HardBotGame()
    {
        Debug.Log("Hard mode Soon ");
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
        UIManager.instUIM.BotSelectorOff();
        PvPBTN.SetActive(true);
    }

}
