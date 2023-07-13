using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    public List<Player> allPlayers;

    int turns = 1;
    int rowIndex;
    int colIndex;

    bool IsGamePlaying = false;
    bool IsPlayerBOT = false;
    bool HasWon = false;

    public static bool IsBotDifHard = false;
    public static bool IsBotTurn = false;
    bool botturn = true;

    public static List<int> RMoves;

    public AudioClip coinDropSound;

    public Text turnText;

    public Color Bottext;
    public Color P1text;
    public Color P2text;

    public ParticleSystem _ps;

    //List<Cell> cells = new List<Cell>();

    public GameObject coin1;
    public GameObject coin2;

    public LayerMask layer;

    public Image p1Pointer;
    public Image p2Pointer;
    public Image pBotPointer;

    public Text winText;
    [Header("lerpcoin")]
    GameObject SC;
    cellType CT;
    Cell cellObj;
    [Range(0.5f, 1.5f)]
    public float desiredDuration = 0.5f;
    public GameObject coinPFB;

    Player currentPlayer;


    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        _ps.Stop();
    }

    public void Start()
    {
        RMoves = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
        p1Pointer.enabled = false;
        p2Pointer.enabled = false;
        pBotPointer.enabled = false;
    }

    public void Gamestart()
    {
        IsGamePlaying = true;
        turns++;
        p1Pointer.enabled = true;
        HasWon = false;
        RMoves = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        turnText.text = "Player 1 's turn";
        turnText.color = P1text;
        p1Pointer.enabled = true;
        p2Pointer.enabled = false;
        pBotPointer.enabled = false;
    }

    public void Gamebotstart()
    {
        IsGamePlaying = true;
        IsPlayerBOT = true;
        turns++;
        p1Pointer.enabled = true;
        HasWon = false;
        RMoves = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        turnText.text = "Player's turn";
        turnText.color = P1text;
        p1Pointer.enabled = true;
        p2Pointer.enabled = false;
        pBotPointer.enabled = false;
    }


    public void Gameend()
    {
        IsGamePlaying = false;
        IsPlayerBOT = false;
        IsBotTurn = false;
        turns = 1;

        _ps.Stop();
    }

    public void Reset()
    {
        HasWon = false;
        IsGamePlaying = true;
        turns = 2;
        turnText.text = "Player's turn";
        turnText.color = P1text;
        RMoves = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        p1Pointer.enabled = true;
        p2Pointer.enabled = false;
        pBotPointer.enabled = false;

        _ps.Stop();
    }



    public void UpdateWinText(cellType cellType)
    {
        if (HasWon == false)
        {

            HasWon = true;
            if (cellType == cellType.red)
            {
                AudioManager.AM.PlayWinSound();
                _ps.Play();
                winText.text = "Player 1 Wins !!";
                winText.color = P1text;
            }

            if (cellType == cellType.yellow)
            {
                if (IsPlayerBOT)
                {
                    AudioManager.AM.PlayLoseSound();
                    winText.text = "BOT Wins !!";
                    winText.color = Bottext;
                }
                else
                {
                    AudioManager.AM.PlayWinSound();
                    _ps.Play();
                    winText.text = "Player 2 Wins !!";
                    winText.color = P2text;
                }
            }
        }
    }


    public void Callplayer(Plyenum plyenum)
    {

        foreach (Player pl in allPlayers)
        {
            if (pl.Number == plyenum)
            {
                currentPlayer = pl;
                break;
            }


        }

        if (currentPlayer.Number == Plyenum.Player1)
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

        if (currentPlayer.Number == Plyenum.Player2)
        {
            p1Pointer.enabled = true;
            p2Pointer.enabled = false;
            turnText.text = "Player 1 's turn";
            turnText.color = P1text;

        }

        if (currentPlayer.Number == Plyenum.playerBOT)
        {
            p1Pointer.enabled = true;
            pBotPointer.enabled = false;
            turnText.text = "Player's turn";
            turnText.color = P1text;

        }
    }


    public void TurnChange()
    {
        if (HasWon == false)
        {

            if (IsGamePlaying)
            {
                if (IsPlayerBOT)
                {
                    if (turns != 1)
                    {
                        if (turns % 2 == 0)
                        {
                            Callplayer(Plyenum.Player1);
                            turns++;
                        }
                        else
                        {
                            Callplayer(Plyenum.playerBOT);
                            turns++;
                        }
                    }
                }
                else
                {
                    if (turns != 1)
                    {
                        if (turns % 2 == 0)
                        {
                            Callplayer(Plyenum.Player1);
                            turns++;
                        }
                        else
                        {
                            Callplayer(Plyenum.Player2);
                            turns++;
                        }

                    }
                }
            }
        }
    }


    public void BDBOT()
    {
        if (turns < 43)
        {
            botturn = true;
            IsBotTurn = true;
            if (IsBotDifHard)
            {
                GridManager.gridMinst.BotDetection(cellType.yellow);
            }
            else
            {
                GridManager.gridMinst.BotDetection(cellType.red);
            }
        }


        if (turns == 43)
        {
            UIManager.instUIM.DrawScreenOn();
            Botmove(7);
            Debug.Log("Match draw");
        }
    }

    public void Botmove(int col)
    {
        Debug.Log("Current column: "+col);
        if (botturn)
        {
            if (col == 7)
            {

                Debug.Log("No Detection, Random move");
                int c = Random.Range(0, RMoves.Count);
                //StartCoroutine(BotThinking(RMoves[c], cellType.yellow));
                 GridManager.gridMinst.Checkcolume(RMoves[c], cellType.yellow);
                botturn = false;

            }

            else
            {
               // StartCoroutine(BotThinking(col, cellType.yellow));
                GridManager.gridMinst.Checkcolume(col, cellType.yellow);
                botturn = false;

            }

        }

    }

    IEnumerator BotThinking(int col, cellType currenttype)
    {
        //IsGamePlaying = false;
        float thinkingTime = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(thinkingTime);
        GridManager.gridMinst.Checkcolume(col, currenttype);


    }

    public void LerpCoin(int row, int col, Cell obj, cellType ct)
    {
        IsGamePlaying = false;
        CT = ct;
        cellObj = obj;
        rowIndex = row;
        colIndex = col;
        SC = Instantiate(coinPFB, new Vector2(coinPFB.transform.position.x + col, coinPFB.transform.position.y), Quaternion.identity);
        if (ct == cellType.red)
        {
            SC.GetComponent<SpriteRenderer>().color = Cell.childcolorREDstat;
        }
        if (ct == cellType.yellow)
        {
            SC.GetComponent<SpriteRenderer>().color = Cell.childcolorYellowstat;
        }
        AudioManager.AM.Playaudio(coinDropSound);
        SC.transform.DOMove(obj.transform.position, desiredDuration).SetEase(Ease.OutBounce).OnComplete(() => OndropCompelete());
    }

    public void OndropCompelete()
    {
        cellObj.GetComponent<Cell>().ChangeType(CT);
        GridManager.gridMinst.CheckWin(rowIndex, colIndex, CT);
        //GridManager.gridMinst.CheckWinV2(rowIndex, colIndex, CT);
        Destroy(SC);
        IsGamePlaying = true;
        TurnChange();

        if (IsBotTurn)
        {
            if (HasWon == false)
            {
                BDBOT();
                IsBotTurn = false;

            }
        }
    }


    public void TakeInputvsBOT(Vector2 inputPostion)
    {
        if (HasWon == false)
        {
            if (IsGamePlaying)
            {
                IsBotTurn = false;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(inputPostion.x, inputPostion.y, 0));
                RaycastHit2D hit = Physics2D.Raycast(inputPostion, Vector3.forward);
                if (Physics2D.Raycast(inputPostion, Vector3.forward, layer))
                {
                    if (hit.collider != null)
                    {
                        if (turns != 1)
                        {
                            int c = hit.collider.GetComponent<Cell>().colIndex;  //hit.collider.GetComponent<Column>().colNUm;
                            GridManager.gridMinst.Checkcolume(c, cellType.red);

                        }
                        else
                        {

                        }

                    }

                }
                IsBotTurn = true;
            }
        }
        else
        {
            Debug.Log("Game over , NO INPUT");
        }
    }

    public void TakeInputTwoPlayer(Vector2 inputPostion)
    {
        if (HasWon == false)
        {
            if (IsGamePlaying)
            {

                Ray ray = Camera.main.ScreenPointToRay(new Vector3(inputPostion.x, inputPostion.y, 0));
                RaycastHit2D hit = Physics2D.Raycast(inputPostion, Vector3.forward);
                if (Physics2D.Raycast(inputPostion, Vector3.forward, layer))
                {
                    if (hit.collider != null)
                    {
                        if (turns != 1)
                        {
                            int c = hit.collider.GetComponent<Cell>().colIndex;  //hit.collider.GetComponent<Column>().colNUm;
                            if (turns % 2 == 0)
                            {
                                GridManager.gridMinst.Checkcolume(c, cellType.red);

                            }
                            else
                            {
                                GridManager.gridMinst.Checkcolume(c, cellType.yellow);

                            }

                        }
                        else
                        {

                        }

                    }

                }
            }
        }
        else
        {
            Debug.Log("Game over , NO INPUT");
        }
    }

    private void Update()
    {
        if (IsGamePlaying)
        {
            if (IsPlayerBOT)
            {
                CheckInput();
            }
            else
            {
                CheckInput();
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputPostion = Input.mousePosition;
            inputPostion = Camera.main.ScreenToWorldPoint(inputPostion);
            if (IsPlayerBOT)
            {
                TakeInputvsBOT(inputPostion);
            }
            else
            {
                TakeInputTwoPlayer(inputPostion);
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 inputPosition = touch.position;
                inputPosition = Camera.main.ScreenToWorldPoint(inputPosition);

                if (IsPlayerBOT)
                {
                    TakeInputvsBOT(inputPosition);
                }
                else
                {
                    TakeInputTwoPlayer(inputPosition);
                }
            }
        }
    }








}



