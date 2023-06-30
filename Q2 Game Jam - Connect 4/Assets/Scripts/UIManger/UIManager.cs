using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScreen[] screen;
    public UIScreen currentScreen;
    public static UIManager instUIM;


    public Canvas LvlSelect;

    public Canvas winScreen;
    


    private void Awake()
    {
        instUIM = this;
        currentScreen.canvas.enabled = true;
    }
    public void SwitchScreen(ScreenType screenType)
    {
        Debug.Log("switch screen");
        currentScreen.canvas.enabled = false;
        foreach (UIScreen onScreen in screen)
        {
            if (onScreen.screenType == screenType)
            {
                onScreen.canvas.enabled = true;
                currentScreen = onScreen;
                break;
            }

        }
    }

    public void levelSelectorOn()
    {
        LvlSelect.enabled = true;
    }

    public void levelSelectorOff()
    {
        LvlSelect.enabled = false;
    }

    public void winOn()
    {
        winScreen.enabled = true;
    }

    public void winOff()
    {
        winScreen.enabled = false;
    }





}
