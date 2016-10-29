using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    public static ScoreController _instance;

    public static string Key {
        get { return "MyBestScore"; }
    }

    [HideInInspector]
    public List<AddScoreItem> addScoreItemList = new List<AddScoreItem>();

    public GameObject AddScoreObject;
    public Text scoreTxt;
    public Text myBestScoreTxt;
    private GameObject addItem;


    private int myBestScore;

    private int _score;
    private int score
    {
        get { return _score; }
        set
        {
            _score = value;
            if (value > myBestScore)
            {
                PlayerPrefs.SetInt(Key,value);
                myBestScore = value;
                myBestScoreTxt.text = _score.ToString();
            }
        }
    }

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
	void Start ()
	{
	    score = 0;
	    scoreTxt.text = score.ToString();

        myBestScore = PlayerPrefs.GetInt(Key);
        myBestScoreTxt.text = myBestScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddScore(int addNum)
    {
        score += addNum;
        scoreTxt.text = score.ToString();
        //播放加分的动画或特效

        if (addScoreItemList.Count <= 0)
        {
            addItem = Instantiate(AddScoreObject);
            ResetAddScoreItemState(addItem);
            addItem.GetComponent<AddScoreItem>().AddScore(addNum);
        }
        else
        {
            addItem = addScoreItemList[0].gameObject;
            addScoreItemList.RemoveAt(0);
            ResetAddScoreItemState(addItem);
            addItem.GetComponent<AddScoreItem>().AddScore(addNum);
        }
    }

    private void ResetAddScoreItemState(GameObject item)
    {
        item.transform.SetParent(scoreTxt.transform.parent);
        item.transform.position = scoreTxt.transform.position;
        item.transform.localScale = Vector3.one;
        item.gameObject.SetActive(true);
    }

    public void RemoveAddScoreItem(AddScoreItem item)
    {
        item.gameObject.SetActive(false);
        addScoreItemList.Add(item);
    }

    public void ResetScore()
    {
        score = 0;
        scoreTxt.text = score.ToString();
    }
}
