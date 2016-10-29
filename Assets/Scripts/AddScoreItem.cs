using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddScoreItem : MonoBehaviour
{

    private Animator anima;
    private Text text;
    // Use this for initialization
    void Start()
    {
    }

    public void AddScore(int addNum)
    {
        anima = GetComponent<Animator>();
        text = GetComponent<Text>();
        text.text = "+" + addNum;
        anima.Play(0);
        float time = anima.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Invoke("RemoveItemToList", time);
    }

    void RemoveItemToList()
    {
        ScoreController._instance.RemoveAddScoreItem(this);
    }
}
