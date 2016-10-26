using UnityEngine;
using System.Collections;

public class CardChange
{

    public int fromIndexX = -1;
    public int fromIndexY = -1;

    public int toIndexX = -1;
    public int toIndexY = -1;

    public int createIndexX = -1;
    public int createIndexY = -1;

    public int desIndexX = -1;
    public int desIndexY = -1;

    public int createNum = 0;

    public CardChange(int fromX = -1, int fromY = -1, int toX = -1, int toY = -1, int createX = -1, int createY = -1,int desX = -1, int desY = -1, int createN = 0)
    {
        fromIndexX = fromX;
        fromIndexY = fromY;
        toIndexX = toX;
        toIndexY = toY;
        createIndexX = createX;
        createIndexY = createY;
        desIndexX = desX;
        desIndexY = desY;
        createNum = createN;
    }
}
