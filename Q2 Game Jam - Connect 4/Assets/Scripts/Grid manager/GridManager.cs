using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridManager : MonoBehaviour
{
    public Cell cell;
    public Transform gridParent;

    List<int> AvoidMoves = new();

    public int rowCount;
    public int columnCount;

    //int curRowIndex;
    //int curColIndex;
    //int coinsToWin = 4;

    public static bool movelimit = false;

    bool resultfound = false;

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
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                Cell obj = Instantiate(cell, (Vector2)cell.transform.position + _coldiff + _rowdiff, Quaternion.identity, gridParent);
                _coldiff += new Vector2(0, -0.9f);
                obj.rowIndex = j;
                obj.colIndex = i;
                obj.name = "[" + j + "," + i + "]";
                cells[j, i] = obj;

            }
            _coldiff = new Vector2(0, 0);
            _rowdiff += new Vector2(0.9f, 0);

        }

    }

    public void Reset()
    {
        //coinsToWin = 4;
        foreach (Cell cell in cells)
        {
            cell.GetComponent<Cell>().ChangeType(cellType.none);
        }
        resultfound = false;
        //AvoidMoves.Clear();
    }


    public void Checkcolume(int SCol, cellType cellT)
    {
        for (int i = 0; i < rowCount; i++)
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
                        GameManager.instance.BDBOT();
                    }
                    break;
                }
                else
                {
                    GameManager.instance.LerpCoin(i - 1, SCol, cells[i - 1, SCol], cellT);

                    break;

                }

            }
            if (i == 5)
            {
                if (cells[i, SCol].cellType == cellType.none)
                {
                    GameManager.instance.LerpCoin(i, SCol, cells[i, SCol], cellT);

                    break;
                }
            }




        }
    }

    public void ShowWinner(cellType currentType, string direction)
    {
        Debug.Log(currentType + " wins " + direction);
        UIManager.instUIM.WinOn();
        GameManager.instance.UpdateWinText(currentType);
    }




    public void CheckWin(int rowIndex, int colIndex, cellType currentType)
    {

        // 3 Coin match
        //horizontal Forward
        if (colIndex + 3 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex + 1] != null && cells[rowIndex, colIndex + 1].cellType == currentType && cells[rowIndex, colIndex + 2] != null && cells[rowIndex, colIndex + 2].cellType == currentType && cells[rowIndex, colIndex + 3] != null && cells[rowIndex, colIndex + 3].cellType == currentType)
        {
            ShowWinner(currentType, "Horizontally");
        }

        //horizontal Backward
        if (colIndex - 3 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex - 1] != null && cells[rowIndex, colIndex - 1].cellType == currentType && cells[rowIndex, colIndex - 2] != null && cells[rowIndex, colIndex - 2].cellType == currentType && cells[rowIndex, colIndex - 3] != null && cells[rowIndex, colIndex - 3].cellType == currentType)
        {
            ShowWinner(currentType, "Horizontally");
        }

        //vertical
        if (rowIndex + 3 < rowCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex + 1, colIndex] != null && cells[rowIndex + 1, colIndex].cellType == currentType && cells[rowIndex + 2, colIndex] != null && cells[rowIndex + 2, colIndex].cellType == currentType && cells[rowIndex + 3, colIndex] != null && cells[rowIndex + 3, colIndex].cellType == currentType)
        {
            ShowWinner(currentType, "vertical");
        }

        //Diagonal (Left down - Right up)
        if (rowIndex - 3 > -1 && colIndex + 3 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex + 1] != null && cells[rowIndex - 1, colIndex + 1].cellType == currentType && cells[rowIndex - 2, colIndex + 2] != null && cells[rowIndex - 2, colIndex + 2].cellType == currentType && cells[rowIndex - 3, colIndex + 3] != null && cells[rowIndex - 3, colIndex + 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Left down - Right up");
        }

        //Diagonal (Right up - Left down)
        if (rowIndex + 3 < rowCount && colIndex - 3 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex + 1, colIndex - 1] != null && cells[rowIndex + 1, colIndex - 1].cellType == currentType && cells[rowIndex + 2, colIndex - 2] != null && cells[rowIndex + 2, colIndex - 2].cellType == currentType && cells[rowIndex + 3, colIndex - 3] != null && cells[rowIndex + 3, colIndex - 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Right up - Left downp");
        }


        //Diagonal (Right down - Left up)
        if (rowIndex - 3 > -1 && colIndex - 3 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex - 1] != null && cells[rowIndex - 1, colIndex - 1].cellType == currentType && cells[rowIndex - 2, colIndex - 2] != null && cells[rowIndex - 2, colIndex - 2].cellType == currentType && cells[rowIndex - 3, colIndex - 3] != null && cells[rowIndex - 3, colIndex - 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Right down - Left up");
        }

        //Diagonal (Left up - Right down)
        if (rowIndex + 3 < rowCount && colIndex + 3 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex + 1, colIndex + 1] != null && cells[rowIndex + 1, colIndex + 1].cellType == currentType && cells[rowIndex + 2, colIndex + 2] != null && cells[rowIndex + 2, colIndex + 2].cellType == currentType && cells[rowIndex + 3, colIndex + 3] != null && cells[rowIndex + 3, colIndex + 3].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Left up - Right down");
        }


        // Middle(2-1-1) coins

        //Horizontal(Middle right)
        if (colIndex - 2 > -1 && colIndex + 1 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex - 1] != null && cells[rowIndex, colIndex - 1].cellType == currentType && cells[rowIndex, colIndex - 2] != null && cells[rowIndex, colIndex - 2].cellType == currentType && cells[rowIndex, colIndex + 1] != null && cells[rowIndex, colIndex + 1].cellType == currentType)
        {
            ShowWinner(currentType, "Horizontal Middle right");
        }

        //Horizontal(MIddle left)
        if (colIndex - 1 > -1 && colIndex + 2 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex, colIndex - 1] != null && cells[rowIndex, colIndex - 1].cellType == currentType && cells[rowIndex, colIndex + 1] != null && cells[rowIndex, colIndex + 1].cellType == currentType && cells[rowIndex, colIndex + 2] != null && cells[rowIndex, colIndex + 2].cellType == currentType)
        {
            ShowWinner(currentType, " Horizontal Middle left");
        }

        //Diagonal(Left down - right up : middle Left)
        if (rowIndex + 1 < rowCount && rowIndex - 2 > -1 && colIndex + 2 < columnCount && colIndex - 1 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex + 1] && cells[rowIndex - 1, colIndex + 1].cellType == currentType && cells[rowIndex - 2, colIndex + 2] && cells[rowIndex - 2, colIndex + 2].cellType == currentType && cells[rowIndex + 1, colIndex - 1] && cells[rowIndex + 1, colIndex - 1].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(Left down - right up : middle Left)");
        }

        //Diagonal(left down - right up : middle right)
        if (rowIndex + 2 < rowCount && rowIndex - 1 > -1 && colIndex - 2 > -1 && colIndex + 1 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex + 1] != null && cells[rowIndex - 1, colIndex + 1].cellType == currentType && cells[rowIndex + 1, colIndex - 1] != null && cells[rowIndex + 1, colIndex - 1].cellType == currentType && cells[rowIndex + 2, colIndex - 2] != null && cells[rowIndex + 2, colIndex - 2].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(left down - right up : middle right)");
        }

        //Diagonal(rightdown - leftup : middle left)
        if (rowIndex + 2 < rowCount && rowIndex - 1 > -1 && colIndex + 2 < columnCount && colIndex - 1 > -1 && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex - 1] != null && cells[rowIndex - 1, colIndex - 1].cellType == currentType && cells[rowIndex + 1, colIndex + 1] != null && cells[rowIndex + 1, colIndex + 1].cellType == currentType && cells[rowIndex + 2, colIndex + 2] != null && cells[rowIndex + 2, colIndex + 2].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(rightdown - leftup : middle left)");
        }

        //Diagonal(rightdown - leftup : middle right)
        if (rowIndex - 2 > -1 && rowIndex + 1 < rowCount && colIndex - 2 > -1 && colIndex + 1 < columnCount && cells[rowIndex, colIndex] != null && cells[rowIndex, colIndex].cellType == currentType && cells[rowIndex - 1, colIndex - 1] != null && cells[rowIndex - 1, colIndex - 1].cellType == currentType && cells[rowIndex + 1, colIndex + 1] != null && cells[rowIndex + 1, colIndex + 1].cellType == currentType && cells[rowIndex - 2, colIndex - 2] != null && cells[rowIndex - 2, colIndex - 2].cellType == currentType)
        {
            ShowWinner(currentType, "Diagonal(rightdown - leftup : middle right)");
        }


    }

    public void DetectionResult(int rowMove, int colMove, cellType currentType)
    {
        Debug.Log("successful detection");
        if (rowMove < 5) // ToDO remove hard number 
        {
            if (cells[rowMove + 1, colMove] != null)
            {
                if (cells[rowMove + 1, colMove].cellType == cellType.red || cells[rowMove + 1, colMove].cellType == cellType.yellow) //TODO : CHECK FOR NUn directly
                {
                    if (currentType == cellType.yellow)
                    {
                        Debug.Log("Bot detected winning chance at: " + colMove);
                        GameManager.instance.Botmove(colMove);

                    }
                    if (currentType == cellType.red)
                    {
                        Debug.Log("Bot detected that Player's win move will be : " + colMove);
                        GameManager.instance.Botmove(colMove);

                    }
                }

                else
                {

                    if (currentType == cellType.yellow)
                    {
                        Debug.Log("Bot win detected but not possible");
                        BotDetection(cellType.red);
                        resultfound = false;
                    }

                    if (currentType == cellType.red)
                    {
                        Debug.Log("winning move detected but coin cannot be placed there");
                        GameManager.instance.Botmove(7);
                        resultfound = false;
                        //if (rowMove < 4 && GameManager.IsBotDifHard == true)
                        //{

                        //    if (cells[rowMove + 1, colMove] != null && cells[rowMove + 1, colMove].cellType == cellType.none)
                        //    {


                        //        if (rowMove + 2 < rowCount && cells[rowMove + 2, colMove] != null)
                        //        {
                        //            if (cells[rowMove + 2, colMove].cellType == cellType.red || cells[rowMove + 2, colMove].cellType == cellType.yellow)
                        //            {
                        //                for (int i = 0; i < GameManager.RMoves.Count; i++)
                        //                {
                        //                    if (colMove == GameManager.RMoves[i])
                        //                    {
                        //                        GameManager.RMoves.Remove(colMove);
                        //                        StartCoroutine(AddColBack(colMove));
                        //                        Debug.Log("Way 2 : Move removed: " + colMove + " Elements remaining:  " + GameManager.RMoves.Count);
                        //                    }
                        //                }
                        //            }
                        //        }


                        //    }

                        //}
                        //if(rowMove == 4 && GameManager.IsBotDifHard == true)
                        //{
                        //    if (cells[rowMove + 1, colMove] != null && cells[rowMove + 1, colMove].cellType == cellType.none)
                        //    {
                        //        for (int i = 0; i < GameManager.RMoves.Count; i++)
                        //        {
                        //            if (colMove == GameManager.RMoves[i])
                        //            {
                        //                GameManager.RMoves.Remove(colMove);
                        //                StartCoroutine(AddColBack(colMove));
                        //                Debug.Log("Lowest row, Move removed: " + colMove + " Elements remaining:  " + GameManager.RMoves.Count);
                        //            }
                        //        }
                        //    }
                        //}

                    }

                }

            }

        }
        if (rowMove == 5)
        {
            Debug.Log("Bot detected that Player's win move will be : " + colMove);
            GameManager.instance.Botmove(colMove);
        }


    }

    //IEnumerator AddColBack(int ColMove)
    // {
    //     yield return new WaitForSeconds(8);
    //     Debug.Log(ColMove + "Added back");
    //     GameManager.RMoves.Add(ColMove);
    // }

    public void BotDetection(cellType CurrentType)
    {
        //if (AvoidMoves.Count != 0)
        //{
        //    AvoidMoves.Clear();

        //}

        for (int col = 0; col < columnCount; col++)
        {
            for (int row = 0; row < rowCount; row++)
            {

                if (cells[row, col].cellType == CurrentType)
                {
                    // For 3 coin detection
                    // Horizontal forward
                    if (col + 2 < columnCount && cells[row, col + 1] != null && cells[row, col + 1].cellType == CurrentType && cells[row, col + 2] != null && cells[row, col + 2].cellType == CurrentType)
                    {
                        if (col + 3 < columnCount && cells[row, col + 3] != null && cells[row, col + 3].cellType == cellType.none)
                        {
                            //check for 4th cell
                            DetectionResult(row, col + 3, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }
                    // Horizontal backward
                    if (col - 2 >= 0 && cells[row, col - 1] != null && cells[row, col - 1].cellType == CurrentType && cells[row, col - 2] != null && cells[row, col - 2].cellType == CurrentType)
                    {
                        if (col - 3 >= 0 && cells[row, col - 3] != null && cells[row, col - 3].cellType == cellType.none)
                        {
                            //check for 4th cell
                            DetectionResult(row, col - 3, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }


                    //Vertical 

                    if (row - 2 >= 0 && cells[row - 1, col] != null && cells[row - 1, col].cellType == CurrentType && cells[row - 2, col] != null && cells[row - 2, col].cellType == CurrentType)
                    {
                        if (row - 3 >= 0 && cells[row - 3, col] != null && cells[row - 3, col].cellType == cellType.none)
                        {

                            DetectionResult(row, col, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }

                    if (row + 2 < rowCount && cells[row + 1, col] != null && cells[row + 1, col].cellType == CurrentType && cells[row + 2, col] != null && cells[row + 2, col].cellType == CurrentType)
                    {
                        if (row + 3 < rowCount && cells[row + 3, col] != null && cells[row + 3, col].cellType == cellType.none)
                        {

                            DetectionResult(row, col, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }



                    //Diagonal(Left down - Right up)

                    if (row - 2 >= 0 && col + 2 < columnCount && cells[row - 1, col + 1] != null && cells[row - 1, col + 1].cellType == CurrentType && cells[row - 2, col + 2] != null && cells[row - 2, col + 2].cellType == CurrentType)
                    {
                        if (row - 3 >= 0 && col + 3 < columnCount && cells[row - 3, col + 3] != null && cells[row - 3, col + 3].cellType == cellType.none)
                        {
                            DetectionResult(row - 3, col + 3, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }

                    //Diagonal(Right up - Left down)

                    if (row + 2 < rowCount && col - 2 >= 0 && cells[row + 1, col - 1] != null && cells[row + 1, col - 1].cellType == CurrentType && cells[row + 2, col - 2] != null && cells[row + 2, col - 2].cellType == CurrentType)
                    {
                        if (row + 3 < rowCount && col - 3 >= 0 && cells[row + 3, col - 3] != null && cells[row + 3, col - 3].cellType == cellType.none)
                        {
                            DetectionResult(row + 3, col - 3, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }


                    //Diagonal(Right down - Left up)

                    if (row - 2 >= 0 && col - 2 >= 0 && cells[row - 1, col - 1] != null && cells[row - 1, col - 1].cellType == CurrentType && cells[row - 2, col - 2] != null && cells[row - 2, col - 2].cellType == CurrentType)
                    {
                        if (row - 3 >= 0 && col - 3 >= 0 && cells[row - 3, col - 3] != null && cells[row - 3, col - 3].cellType == cellType.none)
                        {
                            DetectionResult(row - 3, col - 3, CurrentType);
                            resultfound = true;
                            break;
                        }

                    }

                    //Diagonal(Left up - Right down)

                    if (row + 2 < rowCount && col + 2 < columnCount && cells[row + 1, col + 1] != null && cells[row + 1, col + 1].cellType == CurrentType && cells[row + 2, col + 2] != null && cells[row + 2, col + 2].cellType == CurrentType)
                    {
                        if (row + 3 < rowCount && col + 3 < columnCount && cells[row + 3, col + 3] != null && cells[row + 3, col + 3].cellType == cellType.none)
                        {
                            DetectionResult(row + 3, col + 3, CurrentType);
                            resultfound = true;
                            break;
                        }
                    }

                    // For Middle Detection (2-1-1)
                    // Horizontal Forward
                    if (col + 3 < columnCount && cells[row, col + 1] != null && cells[row, col + 1].cellType == CurrentType && cells[row, col + 2] != null && cells[row, col + 2].cellType == cellType.none && cells[row, col + 3] != null && cells[row, col + 3].cellType == CurrentType)
                    {
                        DetectionResult(row, col + 2, CurrentType);
                        resultfound = true;
                        break;
                    }
                    //Horizontal Backward
                    if (col - 3 >= 0 && cells[row, col - 1] != null && cells[row, col - 1].cellType == CurrentType && cells[row, col - 2] != null && cells[row, col - 2].cellType == cellType.none && cells[row, col - 3] != null && cells[row, col - 3].cellType == CurrentType)
                    {
                        DetectionResult(row, col - 2, CurrentType);
                        resultfound = true;
                        break;
                    }
                    //Vertical
                    if (row - 3 >= 0 && cells[row - 1, col] != null && cells[row - 1, col].cellType == CurrentType && cells[row - 2, col] != null && cells[row - 2, col].cellType == cellType.none && cells[row - 3, col] != null && cells[row - 3, col].cellType == CurrentType)
                    {
                        DetectionResult(row - 2, col, CurrentType);
                        resultfound = true;
                        break;
                    }


                    //Diagonal(Left down - Right up)
                    if (row - 3 >= 0 && col + 3 < columnCount && cells[row - 1, col + 1] != null && cells[row - 1, col + 1].cellType == CurrentType && cells[row - 2, col + 2] != null && cells[row - 2, col + 2].cellType == cellType.none && cells[row - 3, col + 3] != null && cells[row - 3, col + 3].cellType == CurrentType)
                    {
                        DetectionResult(row - 2, col + 2, CurrentType);
                        resultfound = true;
                        break;
                    }

                    //Diagonal(Right up - Left down)
                    if (row + 3 < rowCount && col - 3 >= 0 && cells[row + 1, col - 1] != null && cells[row + 1, col - 1].cellType == CurrentType && cells[row + 2, col - 2] != null && cells[row + 2, col - 2].cellType == cellType.none && cells[row + 3, col - 3] != null && cells[row + 3, col - 3].cellType == CurrentType)
                    {
                        DetectionResult(row + 2, col - 2, CurrentType);
                        resultfound = true;
                        break;
                    }

                    //Diagonal(Right down - Left up)
                    if (row - 3 >= 0 && col - 3 >= 0 && cells[row - 1, col - 1] != null && cells[row - 1, col - 1].cellType == CurrentType && cells[row - 2, col - 2] != null && cells[row - 2, col - 2].cellType == cellType.none && cells[row - 3, col - 3] != null && cells[row - 3, col - 3].cellType == CurrentType)
                    {
                        DetectionResult(row - 2, col - 2, CurrentType);
                        resultfound = true;
                        break;
                    }

                    //Diagonal(Left up - Right down)
                    if (row + 3 < rowCount && col + 3 < columnCount && cells[row + 1, col + 1] != null && cells[row + 1, col + 1].cellType == CurrentType && cells[row + 2, col + 2] != null && cells[row + 2, col + 2].cellType == cellType.none && cells[row + 3, col + 3] != null && cells[row + 3, col + 3].cellType == CurrentType)
                    {
                        DetectionResult(row + 2, col + 2, CurrentType);
                        resultfound = true;
                        break;
                    }


                }
                else
                {
                    Debug.Log("not current type");
                    //if (col == 6 && row==5) //ToDO : remove hard number 
                    //{

                    //}
                }
            }
        }
        if (!resultfound)
        {
            if (CurrentType == cellType.yellow)
            {
                BotDetection(cellType.red);
            }
            if (CurrentType == cellType.red)
            {
                Debug.Log("Random move should happen");
                GameManager.instance.Botmove(7);


            }
            
        }
        resultfound = false;
    }


}


















