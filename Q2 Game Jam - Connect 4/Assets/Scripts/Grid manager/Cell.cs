using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cellType
{
    none,red,yellow
}



public class Cell : MonoBehaviour
{
    public SpriteRenderer child;
    public cellType cellType;
    public Color childcolorRED;
    public Color childcolorYellow;
    public Color reset;
    public static Color childcolorREDstat;
    public static Color childcolorYellowstat;

    private void Awake()
    {
        childcolorREDstat = childcolorRED;
        childcolorYellowstat = childcolorYellow;
    }
    private void Start()
    {
        cellType = cellType.none;
    }
    public void changeRED()
    {
        child.color = childcolorRED;
        cellType = cellType.red;
       // Debug.Log(this.gameObject.name + "of type : " + cellType);
    }
    public void changeYellow()
    {
        child.color = childcolorYellow;
        cellType = cellType.yellow;
        //Debug.Log(this.gameObject.name + "of type : " + cellType);
    }

    public void Reset()
    {
        child.color = reset;
        cellType = cellType.none;
        //Debug.Log(this.gameObject.name + "of type : " + cellType);
    }

    public void ChangeType(cellType ct)
    {
        if (ct == cellType.red)
        {
            child.color = childcolorRED;
            cellType = cellType.red;
        }

        if(ct == cellType.yellow)
        {
            child.color = childcolorYellow;
            cellType = cellType.yellow;
        }

        if(ct == cellType.none)
        {
             child.color = reset;
        cellType = cellType.none;
        }
    }
   
}
