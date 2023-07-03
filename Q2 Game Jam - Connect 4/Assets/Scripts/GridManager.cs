using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridManager : MonoBehaviour
{
    public Cell cell;
    public Transform gridParent;

    public int Row;
    public int column;

    int coinsToWin = 4;

    [Range(0, 5)]
    public int cellrow;
    [Range(0, 6)]
    public int cellcol;

    Cell objcell;

    public Cell[,] cells = new Cell[6, 7];

    Vector2 rowdiff = new Vector2(0, 0); //new Vector2(2, 0);
    Vector2 coldiff = new Vector2(0, 0); //new Vector2(0,-2);

    public static GridManager gridMinst;

    private void Awake()
    {
        gridMinst = this;
    }
    void Start()
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                Cell obj = Instantiate(cell, (Vector2)cell.transform.position + coldiff + rowdiff, Quaternion.identity, gridParent);
                coldiff += new Vector2(0, -1);

                obj.name = "[" + j + "," + i + "]";
                cells[j, i] = obj;

            }
            coldiff = new Vector2(0, 0);
            rowdiff += new Vector2(1, 0);

        }

    }

    public void Reset()
    {
        coinsToWin = 4;
    }


    public void checkWin()
    {
        //foreach(Cell cell in cells)
        //{
        //}

        for (int col = 0; col < column-1; col++)
        {
            for (int row = 0; row < Row-1; row++)
            {
                if (cells[row, col].cellType != cellType.none)
                {
                    if (cells[row, col].cellType == cellType.red)
                    {
                        //coinsToWin = 4;

                        //Horizontal Forward

                        if (cells[row, col + 1].cellType == cellType.red || cells[row, col - 1].cellType == cellType.red)
                        {
                            for (int i = 1; i <= 3; i++)
                            {
                                //if (row < 5 || row > 0 || col > 0 || col < 6)
                                if (col < 0 || col > 6 || row > 5 || row < 0)
                                {
                                    break;
                                }
                                else
                                {
                                    if (cells[row, col + i].cellType == cellType.red)
                                    {
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Horizontally");
                                            break;
                                        }
                                        else
                                        {
                                            
                                        }
                                    }
                                    if (cells[row, col - i].cellType == cellType.red)
                                    {
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Horizontally");
                                            break;
                                        }
                                        else
                                        {
                                            
                                        }
                                    }

                                }

                            }

                        }
                        else if (cells[row + 1, col].cellType == cellType.red || cells[row - 1, col].cellType == cellType.red)
                        {
                            //Vertical
                            for (int i = 1; i <= 3; i++)
                            {
                                if (col < 0 || col > 6 || row > 5 || row < 0)
                                {
                                    break;
                                }
                                else
                                {
                                    if (cells[row + i, col].cellType == cellType.red)
                                    {
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Vertically");
                                            break;
                                        }
                                        else
                                        {
                                            
                                        }
                                    }

                                    if (cells[row - i, col].cellType == cellType.red)
                                    {

                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Vertically");
                                            break;
                                        }
                                        else
                                        {
                                           
                                        }

                                    }
                                }

                            }

                        }

                        //Diagonal(left down)
                        //else if(cells[row +1,col - 1].cellType == cellType.red || cells[row - 1,col + 1].cellType == cellType.red)
                        //{

                        //}












                        //Diagonal(right down)




                    }
                }
            }
        }

    }


}



