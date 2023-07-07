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

    public static bool movelimit = false;

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

    public void ShowWinner(cellType currentType, string direction)
    {
        Debug.Log(currentType + " wins " + direction);
        UIManager.instUIM.winOn();
        GameManager.GMinst.updateWinText(currentType);
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
                                    ShowWinner(currentType, "Horizontally");
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
                                    ShowWinner(currentType, "Vertically");
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
                                    ShowWinner(currentType, "Diagonal(Left down - Right up)");
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
                                    ShowWinner(currentType, "Diagonal(Right down - Left up)");
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

    public void DetectionResult(int rowMove, int colMove)
    {
        Debug.Log("successful detection");
        if (rowMove < 5)
        {
            if (cells[rowMove + 1, colMove] != null)
            {
                if (cells[rowMove + 1, colMove].cellType == cellType.red || cells[rowMove + 1, colMove].cellType == cellType.yellow)
                {
                    Debug.Log("Bot detected that Player's win move will be : " + colMove);
                    GameManager.GMinst.Botmove(colMove);
                }

                else
                {
                    Debug.Log("winning move detected but coin cannot be placed there");
                }

            }

        }
        if (rowMove == 5)
        {
            Debug.Log("Bot detected that Player's win move will be : " + colMove);
            GameManager.GMinst.Botmove(colMove);
        }


    }


    public void BotDetection()
    {
        for (int col = 0; col <= column - 1; col++)
        {
            for (int row = 0; row <= Row - 1; row++)
            {

                if (cells[row, col].cellType == cellType.red)
                {
                    // Horizontal forward
                    if (col + 2 <= 6 && cells[row, col + 1] != null && cells[row, col + 1].cellType == cellType.red && cells[row, col + 2] != null && cells[row, col + 2].cellType == cellType.red)
                    {


                        if (col + 3 <= 6 && cells[row, col + 3] != null && cells[row, col + 3].cellType == cellType.none)
                        {
                            //check for 4th cell
                            DetectionResult(row, col + 3);
                            break;
                        }




                    }
                    // Horizontal backward
                    if (col - 2 >= 0 && cells[row, col - 1] != null && cells[row, col - 1].cellType == cellType.red && cells[row, col - 2] != null && cells[row, col - 2].cellType == cellType.red)
                    {


                        if (col - 3 >= 0 && cells[row, col - 3] != null && cells[row, col - 3].cellType == cellType.none)
                        {
                            //check for 4th cell
                            DetectionResult(row, col - 3);
                            break;
                        }




                    }


                    //Vertical 

                    if (row - 2 <= 5 && cells[row - 1, col] != null && cells[row - 1, col].cellType == cellType.red && cells[row - 2, col] != null && cells[row - 2, col].cellType == cellType.red)
                    {



                        if (row - 3 <= 5 && cells[row - 3, col] != null && cells[row - 3, col].cellType == cellType.none)
                        {

                            DetectionResult(row, col);
                            break;
                        }





                    }




                    //Diagonal(Left down - Right up)

                    if (row - 2 >= 0 && col + 2 <= 6 && cells[row - 1, col + 1] != null && cells[row - 1, col + 1].cellType == cellType.red && cells[row - 2, col + 2] != null && cells[row - 2, col + 2].cellType == cellType.red)
                    {



                        if (row - 3 >= 0 && col + 3 <= 6 && cells[row - 3, col + 3] != null && cells[row - 3, col + 3].cellType == cellType.none)
                        {


                            DetectionResult(row - 3, col + 3);
                            break;
                        }



                    }

                    //Diagonal(Right up - Left down)

                    if (row + 2 <= 5 && col - 2 >= 0 && cells[row + 1, col - 1] != null && cells[row + 1, col - 1].cellType == cellType.red && cells[row + 2, col - 2] != null && cells[row + 2, col - 2].cellType == cellType.red)
                    {



                        if (row + 3 <= 5 && col - 3 >= 0 && cells[row + 3, col - 3] != null && cells[row + 3, col - 3].cellType == cellType.none)
                        {


                            DetectionResult(row + 3, col - 3);
                            break;
                        }



                    }


                    //Diagonal(Right down - Left up)

                    if (row - 2 >= 0 && col - 2 >= 0 && cells[row - 1, col - 1] != null && cells[row - 1, col - 1].cellType == cellType.red && cells[row - 2, col - 2] != null && cells[row - 2, col - 2].cellType == cellType.red)
                    {



                        if (row - 3 >= 0 && col - 3 >= 0 && cells[row - 3, col - 3] != null && cells[row - 3, col - 3].cellType == cellType.none)
                        {
                            DetectionResult(row - 3, col - 3);
                            break;

                        }




                    }

                    //Diagonal(Left up - Right down)

                    if (row + 2 <= 5 && col + 2 <= 6 && cells[row + 1, col + 1] != null && cells[row + 1, col + 1].cellType == cellType.red && cells[row + 2, col + 2] != null && cells[row + 2, col + 2].cellType == cellType.red)
                    {


                        if (row + 3 <= 5 && col + 3 <= 6 && cells[row + 3, col + 3] != null && cells[row + 3, col + 3].cellType == cellType.none)
                        {

                            DetectionResult(row + 3, col + 3);
                            break;
                        }




                    }
                }
                else
                {
                    if (col == 6)
                    {
                        GameManager.GMinst.Botmove(7);
                        break;
                    }
                    
                }
            }
        }
    }
}


















