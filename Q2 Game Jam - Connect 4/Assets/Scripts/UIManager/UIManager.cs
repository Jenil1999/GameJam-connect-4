using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScreen[] screen;
    public UIScreen currentScreen;
    public static UIManager instUIM;


    public Canvas LvlSelect;
    public Canvas BotSelect;
    public Canvas winScreen;
    public Canvas drawScreen;


    private void Awake()
    {
        instUIM = this;
        currentScreen.canvas.enabled = true;
    }
    public void SwitchScreen(ScreenType screenType)
    {
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
    public void DrawScreenOn()
    {
        drawScreen.enabled = true;
    }
    public void DrawScreenOff()
    {
        drawScreen.enabled = false;
    }
    public void LevelSelectorOn()
    {
        LvlSelect.enabled = true;
    }

    public void LevelSelectorOff()
    {
        LvlSelect.enabled = false;
    }

    public void WinOn()
    {
        
        winScreen.enabled = true;
    }

    public void WinOff()
    {
        winScreen.enabled = false;
    }
    public void BotSelectorOn()
    {
        BotSelect.enabled = true;
    }

    public void BotSelectorOff()
    {
        BotSelect.enabled = false;
    }
}
