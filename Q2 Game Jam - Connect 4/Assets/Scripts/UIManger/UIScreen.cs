using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ScreenType
{
    HomeScreen,
    Gameplay
  
}

public class UIScreen : MonoBehaviour
{
    public ScreenType screenType;
    [HideInInspector]
    public Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
}
