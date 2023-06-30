using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public List<Player> allPlayers;

    int turns = 0;

    bool IsGamePlaying = false;

    public GameObject coin1;
    public GameObject coin2;


    Player currentPlayer;

    //public plyenum Number;

    public static InputManager IMinst;

    private void Awake()
    {
        IMinst = this;
    }

    public void Start()
    {
        //foreach(Player pl in allPlayers)
        //{
        //    pl.PlayerId = plid;
        //    Debug.Log(pl.PlayerId);
        //    plid++;
        //}

    }

    public void gamestart()
    {
        IsGamePlaying = true;
    }

    public void gameend()
    {
        IsGamePlaying = false;
    }

    public void callplayer(plyenum plyenum)
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);




        foreach (Player pl in allPlayers)
        {
            if (pl.Number == plyenum)
            {
                currentPlayer = pl;
                break;
            }


        }
        Debug.Log(currentPlayer.playerName+"'s turn");
        if(currentPlayer.Number == plyenum.Player1)
        Instantiate(coin1, new Vector3(mousePos.x,mousePos.y,0), Quaternion.identity);

        if (currentPlayer.Number == plyenum.Player2)
            Instantiate(coin2, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);

        //Debug.Log(Input.mousePosition);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (IsGamePlaying)
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
        }

        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

    }




}
