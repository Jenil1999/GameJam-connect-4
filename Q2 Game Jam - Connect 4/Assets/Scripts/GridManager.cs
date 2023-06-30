using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Cell cell;
    public CellChild game;
    public Transform gridParent;

    public int row;
    public int column;

    

    Vector2 rowdiff = new Vector2(0, 0); //new Vector2(2, 0);
    Vector2 coldiff = new Vector2(0, 0); //new Vector2(0,-2);

    void Start()
    {
        for (int i = 1; i <= column; i++)
        {
            for (int j = 1; j <= row; j++)
            {
                Cell obj = Instantiate(cell, (Vector2)cell.transform.position + coldiff + rowdiff, Quaternion.identity,gridParent);
                Instantiate(game, (Vector2)game.transform.position + coldiff + rowdiff, Quaternion.identity, obj.transform);
                coldiff += new Vector2(0, -1);
                
                obj.name = "[" + j + "," + i + "]";
                
                //obj.name = "[" + i + "," + j + "]";
            }
            coldiff = new Vector2(0, 0);
            rowdiff += new Vector2(1, 0);

        }
    }




}



