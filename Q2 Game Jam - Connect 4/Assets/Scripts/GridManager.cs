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
        coinsToWin = 4;
        foreach (Cell cell in cells)
        {
            cell.GetComponent<Cell>().Reset();
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
                    //colLimit = true;
                    Debug.Log("Same : colume Level reached");
                    break;
                }
                else
                {

                    if (cellT == cellType.red)
                        cells[i - 1, SCol].GetComponent<Cell>().changeRED();

                    if (cellT == cellType.yellow)
                        cells[i - 1, SCol].GetComponent<Cell>().changeYellow();


                    GameManager.GMinst.turnChange();

                    //if (i-1 == 0)
                    //{
                    //    colLimit = true;
                    //    Debug.Log("colume Level reached");
                    //}
                    //else
                    //{
                    //    colLimit = false;
                    //}
                    break;

                }

            }
            if (i == 5)
            {
                if (cells[i, SCol].cellType == cellType.none)
                {
                    colLimit = false;
                    if (cellT == cellType.red)
                        cells[i, SCol].GetComponent<Cell>().changeRED();

                    if (cellT == cellType.yellow)
                        cells[i, SCol].GetComponent<Cell>().changeYellow();
                    GameManager.GMinst.turnChange();
                    break;
                }
            }




        }
    }



    public void checkWin()
    {

        for (int col = 0; col <= column - 1; col++)
        {
            for (int row = 0; row <= Row - 1; row++)
            {
                //Debug.Log(cells[row, col]);
                if (cells[row, col].cellType != cellType.none)
                {

                    if (cells[row, col].cellType == cellType.red)
                    {


                        if (row + 1 > 5 || row - 1 < 0 || col + 1 > 6 || col - 1 < 0)
                        {
                            // Horizontal Forward
                            if (cells[row, col + 1].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (row + i > 5 || row - i < 0 || col + i > 6 || col - i < 0)
                                    {
                                        if (cells[row, col + i].cellType == cellType.red)
                                        {

                                            Debug.Log("Checking Horizontal Forward");
                                            coinsToWin -= 1;
                                            if (coinsToWin == 0)
                                            {
                                                Debug.Log("Red wins : Horizontally Forward");
                                                //break;
                                            }

                                        }

                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {

                            }

                            //Horizontal Backward
                            if (cells[row, col - 1].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (cells[row, col - i].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Horizontal Backward");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Horizontally Backward");
                                            // break;
                                        }

                                    }
                                    else
                                    {

                                    }

                                }

                            }
                            else
                            {

                            }


                            //Vertical Downward
                            if (cells[row + 1, col].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {

                                    if (cells[row + i, col].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Vertical downward");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Vertically Downward");
                                            //break;
                                        }

                                    }
                                    else
                                    {

                                    }

                                }

                            }
                            else
                            {

                            }


                            //Vertical Upward
                            if (cells[row - 1, col].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {

                                    if (cells[row - i, col].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Vertical Upward");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Vertically Upward");
                                            //break;
                                        }


                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {

                            }

                            //Diagonal(Left down - Right up)
                            if (cells[row - 1, col + 1].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {

                                    if (cells[row - i, col + i].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Diagonal(Left down - Right up)");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Diagonal(Left down - Right up)");
                                            //break;
                                        }


                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {

                            }

                            //Diagonal(Right up - Left down)
                            if (cells[row + 1, col - 1].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {

                                    if (cells[row + i, col - i].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Diagonal(Right up - Left down)");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Diagonal(Right up - Left down)");
                                            //break;
                                        }

                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {

                            }


                            //Diagonal(Right down - Left up)
                            if (cells[row - 1, col - 1].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {

                                    if (cells[row - i, col - i].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Diagonal(Right down - Left up)");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Diagonal(Right down - Left up)");
                                            //break;
                                        }

                                    }
                                    else
                                    {

                                    }
                                }

                            }
                            else
                            {

                            }

                           // Diagonal(Left up - Right down)
                            if (cells[row + 1, col + 1].cellType == cellType.red)
                            {
                                coinsToWin = 3;
                                for (int i = 1; i <= 3; i++)
                                {

                                    if (cells[row + i, col + i].cellType == cellType.red)
                                    {
                                        Debug.Log("Checking Diagonal(Left up - Right down)");
                                        coinsToWin -= 1;
                                        if (coinsToWin == 0)
                                        {
                                            Debug.Log("Red wins : Diagonal(Left up - Right down");
                                            //break;
                                        }

                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {

                            }

                        }

                    }
                }
            }
        }

    }


}

















