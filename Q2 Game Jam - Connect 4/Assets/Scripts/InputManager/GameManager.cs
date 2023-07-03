using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<Player> allPlayers;

    int turns = 1;

    bool IsGamePlaying = false;
    bool IsPlayerBOT = false;

    Vector2 mousePosUpdated;

    public Text turnText;

    public Color Bottext;
    public Color P1text;
    public Color P2text;


    List<Cell> cells = new List<Cell>();

    public GameObject coin1;
    public GameObject coin2;

    public LayerMask layer;

    public Image p1Pointer;
    public Image p2Pointer;
    public Image pBotPointer;

    Player currentPlayer;

    //public plyenum Number;

    public static GameManager GMinst;

    private void Awake()
    {
        GMinst = this;
    }

    public void Start()
    {

        p1Pointer.enabled = false;
        p2Pointer.enabled = false;
        pBotPointer.enabled = false;
    }

    public void gamestart()
    {
        IsGamePlaying = true;
        //callplayer(plyenum.Player1);
        turns++;
        p1Pointer.enabled = true;
        turnText.text = "Player 1 's turn";
        
    }

    public void gamebotstart()
    {
        IsGamePlaying = true;
        IsPlayerBOT = true;
        //callplayer(plyenum.Player1);
        turns++;
        p1Pointer.enabled = true;
        turnText.text = "Player's turn";
        
    }


    public void gameend()
    {
        IsGamePlaying = false;
        IsPlayerBOT = false;
        turns = 1;

        foreach (Cell usedcell in cells)
        {
            usedcell.GetComponent<Cell>().Reset();
        }
        cells.Clear();



    }





    public void callplayer(plyenum plyenum)
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosUpdated = mousePos;





        foreach (Player pl in allPlayers)
        {
            if (pl.Number == plyenum)
            {
                currentPlayer = pl;
                break;
            }


        }
        Debug.Log(currentPlayer.playerName + "'s turnfinised");
        if (currentPlayer.Number == plyenum.Player1)
        {
            //GameObject obj = Instantiate(coin1, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            //coins.Add(obj);
            if (IsPlayerBOT)
            {
                p1Pointer.enabled = false;
                pBotPointer.enabled = true;
                turnText.text = "Bot's turn";
                turnText.color = Bottext;
            }

            else
            {
                p1Pointer.enabled = false;
                p2Pointer.enabled = true;
                turnText.text = "Player 2's turn";
                turnText.color = P2text;
            }
        }

        if (currentPlayer.Number == plyenum.Player2)
        {
            //GameObject obj = Instantiate(coin2, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            //coins.Add(obj);
            p1Pointer.enabled = true;
            p2Pointer.enabled = false;
            turnText.text = "Player 1 's turn";
            turnText.color = P1text;
        }

        if (currentPlayer.Number == plyenum.playerBOT)
        {
            // GameObject obj = Instantiate(coin2, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            //coins.Add(obj);
            p1Pointer.enabled = true;
            pBotPointer.enabled = false;
            turnText.text = "Player's turn";
            turnText.color = P1text;
        }

        //Debug.Log(Input.mousePosition);
    }

    public void Reset()
    {
        foreach(Cell usedcell in cells)
        {
            usedcell.GetComponent<Cell>().Reset();
        }
        cells.Clear();
    }






    private void Update()
    {
        mousePosUpdated = Input.mousePosition;
        mousePosUpdated = Camera.main.ScreenToWorldPoint(mousePosUpdated);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics2D.Raycast(mousePosUpdated, Vector3.forward, layer))
            {

                if (IsGamePlaying)
                {
                    if (IsPlayerBOT)
                    {
                        if (turns != 1)
                        {
                            if (turns % 2 == 0)
                            {
                                callplayer(plyenum.Player1);
                                turns++;
                             }
                            else
                            {
                                callplayer(plyenum.playerBOT);
                                turns++;
                            }

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (turns != 1)
                        {
                            if (turns % 2 == 0)
                            {
                                callplayer(plyenum.Player1);
                                turns++;
                            }
                            else
                            {
                                callplayer(plyenum.Player2);
                                turns++;
                            }

                        }
                        else
                        {

                        }

                    }
                }
            }

        }

        //objcell = cells[cellrow, cellcol];
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    objcell.GetComponent<Cell>().changeRED();
        //    Debug.Log(objcell.name);
        //}
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    objcell.GetComponent<Cell>().changeYellow();
        //    Debug.Log(objcell.name);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosUpdated.x, mousePosUpdated.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(mousePosUpdated, Vector3.forward);
            if (Physics2D.Raycast(mousePosUpdated, Vector3.forward))
            {
                if (hit.collider != null)
                {
                    if (turns != 1)
                    {
                        if (turns % 2 == 0)
                        {
                            hit.collider.GetComponent<Cell>().changeYellow();
                            cells.Add(hit.collider.GetComponent<Cell>());
                        }
                        else
                        {
                            hit.collider.GetComponent<Cell>().changeRED();
                            cells.Add(hit.collider.GetComponent<Cell>());
                            GridManager.gridMinst.checkWin();
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


