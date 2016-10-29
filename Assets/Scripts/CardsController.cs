using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CardsController : MonoBehaviour
{

    public static CardsController _instance;

    public GameObject Panel;

    public GameObject winPanel;
    public GameObject losePanel;

    public Transform[] oneRowCards;
    public Transform[] towRowCards;
    public Transform[] thrRowCards;
    public Transform[] forRowCards;

    private bool canMove = false;

    private Vector2 touchDeltaPosition;

    private DatasController datas;

    private CardItem[,] items = new CardItem[4, 4];

    private List<CardChange> createCardsIndexList = new List<CardChange>();
    private List<CardChange> destroyCardsIndexList = new List<CardChange>();
    private List<CardChange> moveCardsIndexList = new List<CardChange>();

    private List<CardItem> cardItemList = new List<CardItem>(); 

    void Awake()
    {
        _instance = this;
        datas = new DatasController();
        datas.loser = onLose;
    }

    private void onLose()
    {
        losePanel.SetActive(true);
    }


    //重新初始化游戏
    public void ResetGame()
    {
        datas.ResetGameState();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (items[i, j] != null)
                {
                    DestroyCardItem(items[i, j]);
                    items[i, j] = null;
                }
            }
        }
        createCardsIndexList.Clear();
        destroyCardsIndexList.Clear();
        moveCardsIndexList.Clear();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        ScoreController._instance.ResetScore();
        Start();
    }

    public void GoBackToLaunchScene()
    {
        SceneManager.LoadScene(0);
    }

    // Use this for initialization
    void Start()
    {
        if (createCardsIndexList.Count > 0)
            createCardsIndexList.Clear();
        CreateCards(2);
    }

    void CreateCards(int num)
    {
        for (int i = 0; i < num; i++)
        {
            createCardsIndexList.Add(datas.RedomCreateValue());
        }
        CreateCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.W))
            {
                canMove = false;
                MoveToUp();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                canMove = false;
                MoveToButtom();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                canMove = false;
                MoveToRight();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                canMove = false;
                MoveToLeft();
            }

#elif UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y) && touchDeltaPosition.x < 0)
                {
                    canMove = false;
                    MoveToLeft();
                }
                else if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y) &&
                         touchDeltaPosition.x > 0)
                {
                    canMove = false;
                    MoveToRight();
                }
                else if (Mathf.Abs(touchDeltaPosition.x) < Mathf.Abs(touchDeltaPosition.y) &&
                         touchDeltaPosition.y < 0)
                {
                    canMove = false;
                    MoveToButtom();
                }
                else if (Mathf.Abs(touchDeltaPosition.x) < Mathf.Abs(touchDeltaPosition.y) &&
                         touchDeltaPosition.y > 0)
                {
                    canMove = false;
                    MoveToUp();
                }
                touchDeltaPosition = new Vector2(0, 0);
            }
#endif
        }
    }

    private void MoveToLeft()
    {
        moveCardsIndexList = datas.MoveToLeft();
        MoveCards();
    }

    private void MoveToRight()
    {
        moveCardsIndexList = datas.MoveToRight();
        MoveCards();
    }

    private void MoveToButtom()
    {
        moveCardsIndexList = datas.MoveToButtom();
        MoveCards();
    }

    private void MoveToUp()
    {
        moveCardsIndexList = datas.MoveToUp();
        MoveCards();
    }

    void MoveCards()
    {
        if (moveCardsIndexList.Count == 0)
        {
            canMove = true;
            return;
        }

        int totleScore = 0;

        for (int i = 0; i < moveCardsIndexList.Count; i++)
        {
            int froX = moveCardsIndexList[i].fromIndexX;
            int froY = moveCardsIndexList[i].fromIndexY;
            int toX = moveCardsIndexList[i].toIndexX;
            int toY = moveCardsIndexList[i].toIndexY;
            int crNum = moveCardsIndexList[i].createNum;

            if (crNum != 0)
            {
                DestroyCardItem(items[toX, toY]);
                CardChange change = new CardChange(-1, -1, -1, -1, toX, toY, toX, toY, crNum);
                destroyCardsIndexList.Add(change);
                createCardsIndexList.Add(change);
                totleScore += crNum;
            }

            switch (toX)
            {
                case 0:
                    items[froX, froY].SetMoveValue(oneRowCards[toY].position);
                    break;
                case 1:
                    items[froX, froY].SetMoveValue(towRowCards[toY].position);
                    break;
                case 2:
                    items[froX, froY].SetMoveValue(thrRowCards[toY].position);
                    break;
                case 3:
                    items[froX, froY].SetMoveValue(forRowCards[toY].position);
                    break;
            }

            items[toX, toY] = items[froX, froY];
            items[froX, froY] = null;

        }
        //加分
        if (totleScore != 0)
        {
            ScoreController._instance.AddScore(totleScore);
        }
        moveCardsIndexList.Clear();
        StartCoroutine(TestNest());
    }

    private IEnumerator TestNest()
    {
        yield return new WaitForSeconds(0.1f);
        DestroyCardsAndCreateCards();
    }

    //在移动卡片完成之后销毁叠加的卡片和生成新卡片并恢复滑动权限
    void DestroyCardsAndCreateCards()
    {
        for (int i = 0; i < destroyCardsIndexList.Count; i++)
        {
            int x = destroyCardsIndexList[i].desIndexX;
            int y = destroyCardsIndexList[i].desIndexY;
            DestroyCardItem(items[x, y]);
            items[x, y] = null;
        }
        destroyCardsIndexList.Clear();
        CreateCards(1);
    }

    GameObject CheckCardItem(int needNum)
    {
        GameObject needObject;
        for (int i = 0; i < cardItemList.Count; i++)
        {
            if (cardItemList[i].cardNum == needNum)
            {
                needObject = cardItemList[i].gameObject;
                needObject.gameObject.SetActive(true);
                cardItemList.Remove(cardItemList[i]);
                return needObject;
            }
        }
        needObject = Instantiate(Resources.Load(needNum.ToString())) as GameObject;
        return needObject;
    }

    void DestroyCardItem(CardItem item)
    {
        item.gameObject.SetActive(false);
        cardItemList.Add(item);
    }

    //生成卡片
    void CreateCards()
    {
        bool isWin = false;
        for (int i = 0; i < createCardsIndexList.Count; i++)
        {
            int x = createCardsIndexList[i].createIndexX;
            int y = createCardsIndexList[i].createIndexY;
            GameObject card = CheckCardItem(createCardsIndexList[i].createNum);
            card.transform.SetParent(Panel.transform);
            card.GetComponent<CardItem>().SetValue(createCardsIndexList[i].createNum);
            if (createCardsIndexList[i].createNum == 2048)
                isWin = true;
            switch (x)
            {
                case 0:
                    card.transform.position = oneRowCards[y].position;
                    card.GetComponent<CardItem>().SetMoveValue(oneRowCards[y].position);
                    break;
                case 1:
                    card.transform.position = towRowCards[y].position;
                    card.GetComponent<CardItem>().SetMoveValue(towRowCards[y].position);
                    break;
                case 2:
                    card.transform.position = thrRowCards[y].position;
                    card.GetComponent<CardItem>().SetMoveValue(thrRowCards[y].position);
                    break;
                case 3:
                    card.transform.position = forRowCards[y].position;
                    card.GetComponent<CardItem>().SetMoveValue(forRowCards[y].position);
                    break;
            }
            card.transform.localScale = Vector3.one;
            items[x, y] = card.GetComponent<CardItem>();
        }
        createCardsIndexList.Clear();

        if (isWin)
        {
            winPanel.SetActive(true);
            return;
        }

        canMove = true;
    }
}
