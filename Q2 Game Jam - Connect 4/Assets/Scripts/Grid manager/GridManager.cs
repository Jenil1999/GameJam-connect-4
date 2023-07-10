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

    //int coinsToWin = 4;

    public static bool movelimit = false;

    public Cell[,] cells = new Cell[6, 7];

    Vector2 _rowdiff = new Vector2(0, 0); //new Vector2(2, 0);
    Vector2 _coldiff = new Vector2(0, 0); //new Vector2(0,-2);

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
                Cell obj = Instantiate(cell, (Vector2)cell.transform.position + _coldiff + _rowdiff, Quaternion.identity, gridParent);
                _coldiff += new Vector2(0, -1);
                obj.rowIndex = j;
                obj.colIndex = i;
                obj.name = "[" + j + "," + i + "]";
                cells[j, i] = obj;

            }
            _coldiff = new Vector2(0, 0);
            _rowdiff += new Vector2(1, 0);

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


    public void Checkcolume(int SCol, cellType cellT)
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
                    GameManager.GMinst.LerpCoin(i-1,SCol, cells[i - 1, SCol], cellT);

                    break;

                }

            }
            if (i == 5)
            {
                if (cells[i, SCol].cellType == cellType.none)
                {
                    GameManager.GMinst.LerpCoin(i,SCol, cells[i, SCol], cellT);

                    break;
                }
            }




        }
    }

    public void ShowWinner(cellType currentType, string direction)
    {
        Debug.Log(currentType + " wins " + direction);
        UIManager.instUIM.WinOn();
        AudioManager.AM.PlayWinAudio();
        GameManager.GMinst.UpdateWinText(currentType);
    }


    public void CheckWin(int rowIndex, int colIndex, cellType currentType)
    {
        //proto method for CheckWin

        // 3 Coin match
        //horizontal Forward
        if (colIndex + 3 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex + 1] != null && cells[rowIndex, colIndex + 1].cellType == currentType && cells[rowIndex, colIndex + 2] != null && cells[rowIndex, colIndex + 2].cellType == currentType && cells[rowIndex, colIndex + 3] != null && cells[rowIndex, colIndex + 3].cellType == currentType)
        {
            ShowWinner(currentType, "Horizontally");
        }

        //horizontal Backward
        if (colIndex - 3 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex - 1] != null && cells[rowIndex, colIndex - 1].cellType == currentType && cells[rowIndex, colIndex - 2] != null && cells[rowIndex, colIndex - 2].cellType == currentType && cells[rowIndex, colIndex - 3] != null && cells[rowIndex, colIndex - 3].cellType == currentType)
        {
            ShowWinner(currentType, "Horizontally");
        }

        //vertical
        if (rowIndex + 3 < Row && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex + 1, colIndex] != null && cells[rowIndex + 1, colIndex].cellType == currentType && cells[rowIndex + 2, colIndex] != null && cells[rowIndex + 2, colIndex].cellType == currentType && cells[rowIndex + 3, colIndex] != null && cells[rowIndex + 3, colIndex].cellType == currentType)
        {
            ShowWinner(currentType, "vertical");
        }

        //Diagonal (Left down - Right up)
        if (rowIndex - 3 > -1 && colIndex + 3 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex + 1] != null && cells[rowIndex - 1, colIndex + 1].cellType == currentType && cells[rowIndex - 2, colIndex + 2] != null && cells[rowIndex - 2, colIndex + 2].cellType == currentType && cells[rowIndex - 3, colIndex + 3] != null && cells[rowIndex - 3, colIndex + 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Left down - Right up");
        }

        //Diagonal (Right up - Left down)
        if (rowIndex + 3 < Row && colIndex - 3 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex + 1, colIndex - 1] != null && cells[rowIndex + 1, colIndex - 1].cellType == currentType && cells[rowIndex + 2, colIndex - 2] != null && cells[rowIndex + 2, colIndex - 2].cellType == currentType && cells[rowIndex + 3, colIndex - 3] != null && cells[rowIndex + 3, colIndex - 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Right up - Left downp");
        }


        //Diagonal (Right down - Left up)
        if (rowIndex - 3 > -1 && colIndex - 3 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex - 1] != null && cells[rowIndex - 1, colIndex - 1].cellType == currentType && cells[rowIndex - 2, colIndex - 2] != null && cells[rowIndex - 2, colIndex - 2].cellType == currentType && cells[rowIndex - 3, colIndex - 3] != null && cells[rowIndex - 3, colIndex - 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Right down - Left up");
        }

        //Diagonal (Left up - Right down)
        if (rowIndex + 3 < Row && colIndex + 3 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex + 1, colIndex + 1] != null && cells[rowIndex + 1, colIndex + 1].cellType == currentType && cells[rowIndex + 2, colIndex + 2] != null && cells[rowIndex + 2, colIndex + 2].cellType == currentType && cells[rowIndex + 3, colIndex + 3] != null && cells[rowIndex + 3, colIndex + 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Left up - Right down");
        }


        // Middle(2-1-1) coins

        //Horizontal(Middle right)
        if (colIndex - 2 > -1 && colIndex + 1 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex - 1] != null && cells[rowIndex, colIndex - 1].cellType == currentType && cells[rowIndex, colIndex - 2] != null && cells[rowIndex, colIndex - 2].cellType == currentType && cells[rowIndex, colIndex + 1] != null && cells[rowIndex, colIndex + 1].cellType == currentType)
        {
            ShowWinner(currentType, "Horizontal Middle right");
        }

        //Horizontal(MIddle left)
        if (colIndex - 1 > -1 && colIndex + 2 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex - 1] != null && cells[rowIndex, colIndex - 1].cellType == currentType && cells[rowIndex, colIndex + 1] != null && cells[rowIndex, colIndex + 1].cellType == currentType && cells[rowIndex, colIndex + 2] != null && cells[rowIndex, colIndex + 2].cellType == currentType)
        {
            ShowWinner(currentType, " Horizontal Middle left");
        }

        //Diagonal(Left down - right up : middle Left)
        if (rowIndex + 1 < Row && rowIndex - 2 > -1 && colIndex + 2 < column && colIndex - 1 > -1 &&  cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex -1,colIndex +1] && cells[rowIndex-1,colIndex+1].cellType == currentType && cells[rowIndex - 2, colIndex + 2] && cells[rowIndex - 2, colIndex + 2].cellType == currentType && cells[rowIndex + 1, colIndex - 1] && cells[rowIndex + 1, colIndex - 1].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Left down - right up : middle Left)");
        }

        //Diagonal(left down - right up : middle right)
        if (rowIndex + 2 < Row && rowIndex - 1 > -1 && colIndex - 2 > -1 && colIndex + 1 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex -1,colIndex +1] != null && cells[rowIndex -1,colIndex +1].cellType == currentType && cells[rowIndex + 1, colIndex - 1] != null && cells[rowIndex + 1, colIndex - 1].cellType == currentType && cells[rowIndex + 2, colIndex - 2] != null && cells[rowIndex + 2, colIndex - 2].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(left down - right up : middle right)");
        }

        //Diagonal(rightdown - leftup : middle left)
        if (rowIndex + 2 < Row && rowIndex - 1 > -1 && colIndex + 2 < column && colIndex - 1 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex - 1] != null && cells[rowIndex - 1, colIndex - 1].cellType == currentType && cells[rowIndex + 1, colIndex + 1] != null && cells[rowIndex + 1, colIndex + 1].cellType == currentType && cells[rowIndex + 2, colIndex + 2] != null && cells[rowIndex + 2, colIndex + 2].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(rightdown - leftup : middle left)");
        }

        //Diagonal(rightdown - leftup : middle right)
        if (rowIndex - 2 > -1 && rowIndex + 1 < Row && colIndex - 2 > -1 && colIndex + 1 < column && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex - 1] != null && cells[rowIndex - 1, colIndex - 1].cellType == currentType && cells[rowIndex + 1, colIndex + 1] != null && cells[rowIndex + 1, colIndex + 1].cellType == currentType && cells[rowIndex - 2, colIndex - 2] != null && cells[rowIndex - 2, colIndex - 2].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(rightdown - leftup : middle right)");
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
                    // For 3 coin detection
                    // Horizontal forward
                    if (col + 2 < column && cells[row, col + 1] != null && cells[row, col + 1].cellType == cellType.red && cells[row, col + 2] != null && cells[row, col + 2].cellType == cellType.red)
                    {
                        if (col + 3 < column && cells[row, col + 3] != null && cells[row, col + 3].cellType == cellType.none)
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

                    if (row - 2 >= 0 && cells[row - 1, col] != null && cells[row - 1, col].cellType == cellType.red && cells[row - 2, col] != null && cells[row - 2, col].cellType == cellType.red)
                    {
                        if (row - 3 >= 0 && cells[row - 3, col] != null && cells[row - 3, col].cellType == cellType.none)
                        {

                            DetectionResult(row, col);
                            break;
                        }

                    }

                   



                    //Diagonal(Left down - Right up)

                    if (row - 2 >= 0 && col + 2 < column && cells[row - 1, col + 1] != null && cells[row - 1, col + 1].cellType == cellType.red && cells[row - 2, col + 2] != null && cells[row - 2, col + 2].cellType == cellType.red)
                    {
                        if (row - 3 >= 0 && col + 3 < column && cells[row - 3, col + 3] != null && cells[row - 3, col + 3].cellType == cellType.none)
                        {
                            DetectionResult(row - 3, col + 3);
                            break;
                        }

                    }

                    //Diagonal(Right up - Left down)

                    if (row + 2 < Row && col - 2 >= 0 && cells[row + 1, col - 1] != null && cells[row + 1, col - 1].cellType == cellType.red && cells[row + 2, col - 2] != null && cells[row + 2, col - 2].cellType == cellType.red)
                    {
                        if (row + 3 < Row && col - 3 >= 0 && cells[row + 3, col - 3] != null && cells[row + 3, col - 3].cellType == cellType.none)
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

                    if (row + 2 < Row && col + 2 < column && cells[row + 1, col + 1] != null && cells[row + 1, col + 1].cellType == cellType.red && cells[row + 2, col + 2] != null && cells[row + 2, col + 2].cellType == cellType.red)
                    {
                        if (row + 3 < Row && col + 3 < column && cells[row + 3, col + 3] != null && cells[row + 3, col + 3].cellType == cellType.none)
                        {
                            DetectionResult(row + 3, col + 3);
                            break;
                        }
                    }

                    // For Middle Detection (2-1-1)
                    // Horizontal Forward
                    if(col + 3 < column && cells[row, col + 1] != null && cells[row, col + 1].cellType == cellType.red && cells[row, col + 2] != null && cells[row, col + 2].cellType == cellType.none && cells[row, col + 3] != null && cells[row, col + 3].cellType == cellType.red)
                    {
                        DetectionResult(row, col + 2);
                        break;
                    }
                    //Horizontal Backward
                    if (col - 3 >= 0 && cells[row, col - 1] != null && cells[row, col - 1].cellType == cellType.red && cells[row, col - 2] != null && cells[row, col - 2].cellType == cellType.none && cells[row, col - 3] != null && cells[row, col - 3].cellType == cellType.red)
                    {
                        DetectionResult(row, col - 2);
                        break;
                    }
                    //Vertical
                    if(row - 3 >=0 && cells[row - 1, col] != null && cells[row - 1, col].cellType == cellType.red && cells[row - 2, col] != null && cells[row - 2, col].cellType == cellType.none && cells[row - 3, col] != null && cells[row - 3, col].cellType == cellType.red)
                    {
                        DetectionResult(row - 2, col);
                        break;
                    }


                    //Diagonal(Left down - Right up)
                    if (row - 3 >= 0 && col + 3 < column && cells[row - 1, col + 1] != null && cells[row - 1, col + 1].cellType == cellType.red && cells[row - 2, col + 2] != null && cells[row - 2, col + 2].cellType == cellType.none && cells[row - 3, col + 3] != null && cells[row - 3, col + 3].cellType == cellType.red)
                    {
                        DetectionResult(row - 2, col + 2);
                        break;
                    }

                    //Diagonal(Right up - Left down)
                    if(row + 3 <Row && col - 3 >= 0 && cells[row + 1, col - 1] != null && cells[row + 1, col - 1].cellType == cellType.red && cells[row + 2, col - 2] != null && cells[row + 2, col - 2].cellType == cellType.none && cells[row + 3, col - 3] != null && cells[row + 3, col - 3].cellType == cellType.red)
                    {
                        DetectionResult(row + 2, col - 2);
                        break;
                    }

                    //Diagonal(Right down - Left up)
                    if(row - 3 >= 0 && col - 3 >= 0 && cells[row - 1, col - 1] != null && cells[row - 1, col - 1].cellType == cellType.red && cells[row - 2, col - 2] != null && cells[row - 2, col - 2].cellType == cellType.none && cells[row - 3, col - 3] != null && cells[row - 3, col - 3].cellType == cellType.red)
                    {
                        DetectionResult(row - 2, col - 2);
                        break;
                    }

                    //Diagonal(Left up - Right down)
                    if(row + 3 < Row && col + 3 < column && cells[row + 1, col + 1] != null && cells[row + 1, col + 1].cellType == cellType.red && cells[row + 2, col + 2] != null && cells[row + 2, col + 2].cellType == cellType.none && cells[row + 3, col + 3] != null && cells[row + 3, col + 3].cellType == cellType.red)
                    {
                        DetectionResult(row + 3, col + 3);
                        break;
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


















