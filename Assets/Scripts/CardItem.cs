using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{

    public Vector3 _targetPos;

    private  float moveSpeed = 3;
    private float needScend = 0.1f;

    public Text text;
    [HideInInspector]
    public int cardNum;

    // Use this for initialization
    void Start()
    {
        text = GetComponentInChildren<Text>();
        GetComponent<Animator>().Play(0);
    }

    public void SetValue(int value)
    {
        if (text)
        {
            text.text = value.ToString();
        }
        cardNum = value;
    }

    public void SetMoveValue(Vector3 targetPos)
    {
        moveSpeed = Vector3.Distance(transform.position,targetPos) / needScend;
        _targetPos = targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetPos != transform.position)
        {
            CardMove();
        }
    }

    void CardMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
    }
}
