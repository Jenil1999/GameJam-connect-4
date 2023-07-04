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
        turns++;
        p1Pointer.enabled = true;
        turnText.text = "Player 1 's turn";

    }

    public void gamebotstart()
    {
        IsGamePlaying = true;
        IsPlayerBOT = true;
        turns++;
        p1Pointer.enabled = true;
        turnText.text = "Player's turn";

    }


    public void gameend()
    {
        IsGamePlaying = false;
        IsPlayerBOT = false;
        turns = 1;
    }





    public void callplayer(plyenum plyenum)
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosUpdated = mousePos;



       // if (GridManager.colLimit == false)
        //{
            foreach (Player pl in allPlayers)
            {
                if (pl.Number == plyenum)
                {
                    currentPlayer = pl;
                    break;
                }


            }
            //Debug.Log(currentPlayer.playerName + "'s turnfinised");
            if (currentPlayer.Number == plyenum.Player1)
            {

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
                p1Pointer.enabled = true;
                p2Pointer.enabled = false;
                turnText.text = "Player 1 's turn";
                turnText.color = P1text;

            }

            if (currentPlayer.Number == plyenum.playerBOT)
            {
                p1Pointer.enabled = true;
                pBotPointer.enabled = false;
                turnText.text = "Player's turn";
                turnText.color = P1text;

            }

        //}



    }


    public void turnChange()
    {

        if (IsGamePlaying)
        {
            if (IsPlayerBOT)
            {
                if (turns != 1)
                {
                    if (turns % 2 == 0)
                    {
                        //if (GridManager.colLimit == false)
                        //{
                            callplayer(plyenum.Player1);
                            turns++;

                        //}
                    }
                    else
                    {
                        //if (GridManager.colLimit == false)
                        //{
                            callplayer(plyenum.playerBOT);
                            turns++;

                        //}
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
                        //if (GridManager.colLimit == false)
                        //{
                            callplayer(plyenum.Player1);
                            turns++;

                        //}
                    }
                    else
                    {

                        //if (GridManager.colLimit == false)
                        //{
                            callplayer(plyenum.Player2);
                            turns++;

                        //}
                    }

                }
                else
                {

                }

            }
        }
    }


    private void Update()
    {
        mousePosUpdated = Input.mousePosition;
        mousePosUpdated = Camera.main.ScreenToWorldPoint(mousePosUpdated);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosUpdated.x, mousePosUpdated.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(mousePosUpdated, Vector3.forward);
            if (Physics2D.Raycast(mousePosUpdated, Vector3.forward, layer))
            {
                if (hit.collider != null)
                {
                    if (turns != 1)
                    {
                        // Debug.Log(hit.collider.name);
                        int c = hit.collider.GetComponent<Column>().colNUm;

                        if (turns % 2 == 0)
                        {

                            // GridManager.gridMinst.checkWin();
                            GridManager.gridMinst.checkcolume(c, cellType.red);

                            GridManager.gridMinst.checkWin();



                        }
                        else
                        {
                            GridManager.gridMinst.checkcolume(c, cellType.yellow);


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


