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

    public static bool colLimit = false;

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
        //coinsToWin = 4;
        foreach (Cell cell in cells)
        {
            cell.GetComponent<Cell>().ChangeType(cellType.none);
        }
    }


    public void checkcolume(int SCol, cellType cellT)
    {
        for (int i = 0; i < Row; i++)
        {
            if (cells[i, SCol].cellType == cellType.none)
            {

            }
            else
            {
                if (i == 0)
                {

                    Debug.Log("Same : colume Level reached");
                    if (GameManager.IsBotTurn)
                    {
                        Debug.Log("Bot redirected");
                        GameManager.GMinst.BDBOT();
                    }
                    break;
                }
                else
                {
                    GameManager.GMinst.LerpCoin(SCol, cells[i - 1, SCol], cellT);

                    break;

                }

            }
            if (i == 5)
            {
                if (cells[i, SCol].cellType == cellType.none)
                {
                    GameManager.GMinst.LerpCoin(SCol, cells[i, SCol], cellT);
                    
                    break;
                }
            }




        }
    }

    public void ShowWinner(cellType currentType)
    {

    }


    public void checkWin(cellType currentType)
    {

        for (int col = 0; col <= column - 1; col++)
        {
            for (int row = 0; row <= Row - 1; row++)
            {

                if (cells[row, col].cellType == currentType)
                {

                    // Horizontal
                    if (col + 1 <= 6 && cells[row, col + 1].cellType == currentType)
                    {
                        coinsToWin = 3;
                        for (int i = 1; i <= 3; i++)
                        {
                            if (col + i <= 6 && cells[row, col + i].cellType == currentType)
                            {
                                coinsToWin -= 1;
                                if (coinsToWin == 0)
                                {
                                    Debug.Log(currentType + " wins : Horizontally ");
                                    UIManager.instUIM.winOn();
                                    GameManager.GMinst.updateWinText(currentType);

                                    //if (currentType == cellType.red)
                                    //    GameManager.GMinst.updateWinText(0);
                                    //if (currentType == cellType.yellow)
                                    //    GameManager.GMinst.updateWinText(1);

                                }

                            }
                            else
                            {
                                break;
                            }


                        }
                    }

                    //Vertical 

                    if (row + 1 <= 5 && cells[row + 1, col].cellType == currentType)
                    {
                        coinsToWin = 3;
                        for (int i = 1; i <= 3; i++)
                        {

                            if (row + i <= 5 && cells[row + i, col].cellType == currentType)
                            {
                                //Debug.Log("Checking Red Vertical downward");
                                coinsToWin -= 1;
                                if (coinsToWin == 0)
                                {
                                    Debug.Log(currentType + " wins : Vertically ");
                                    UIManager.instUIM.winOn();
                                    GameManager.GMinst.updateWinText(currentType);

                                    //if (currentType == cellType.red)
                                    //    GameManager.GMinst.updateWinText(0);
                                    //if (currentType == cellType.yellow)
                                    //    GameManager.GMinst.updateWinText(1);
                                }

                            }
                            else
                            {
                                break;
                            }


                        }

                    }




                    //Diagonal(Left down - Right up)

                    if (row - 1 >= 0 && col + 1 <= 6 && cells[row - 1, col + 1].cellType == currentType)
                    {
                        coinsToWin = 3;
                        for (int i = 1; i <= 3; i++)
                        {

                            if (row - i >= 0 && col + i <= 6 && cells[row - i, col + i].cellType == currentType)
                            {
                                //Debug.Log("Checking Red Diagonal(Left down - Right up)");
                                coinsToWin -= 1;
                                if (coinsToWin == 0)
                                {
                                    Debug.Log(currentType + " wins : Diagonal Right");
                                    UIManager.instUIM.winOn();
                                    GameManager.GMinst.updateWinText(currentType);

                                    //if (currentType == cellType.red)
                                    //    GameManager.GMinst.updateWinText(0);
                                    //if (currentType == cellType.yellow)
                                    //    GameManager.GMinst.updateWinText(1);
                                }


                            }
                            else
                            {
                                break;
                            }

                        }
                    }


                    //Diagonal(Right down - Left up)

                    if (row - 1 >= 0 && col - 1 >= 0 && cells[row - 1, col - 1].cellType == currentType)
                    {
                        coinsToWin = 3;
                        for (int i = 1; i <= 3; i++)
                        {

                            if (row - i >= 0 && col - i >= 0 && cells[row - i, col - i].cellType == currentType)
                            {
                                //Debug.Log("Checking Red Diagonal(Right down - Left up)");
                                coinsToWin -= 1;
                                if (coinsToWin == 0)
                                {
                                    Debug.Log(currentType + " wins : Diagonal left ");
                                    UIManager.instUIM.winOn();
                                    GameManager.GMinst.updateWinText(currentType);

                                    //if (currentType == cellType.red)
                                    //    GameManager.GMinst.updateWinText(0);
                                    //if (currentType == cellType.yellow)
                                    //    GameManager.GMinst.updateWinText(1);
                                }

                            }
                            else
                            {
                                break;
                            }

                        }

                    }


                }
            }
        }
    }
}



















