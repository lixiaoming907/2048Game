using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatasController
{

    public delegate void Loser();

    public Loser loser;


    int[,] intValue = new int[4, 4];

    private int tabIndex = 0;


    //随机生成一个元素2
    public CardChange RedomCreateValue()
    {
        int row = Random.Range(0, 4);
        int col = Random.Range(0, 4);

        while (intValue[row, col] != 0)
        {
            row = Random.Range(0, 4);
            col = Random.Range(0, 4);
        }
        intValue[row, col] = 2;

        CardChange cardChange = new CardChange(-1, -1, -1, -1, row, col, -1, -1, intValue[row, col]);
        //检查是否输了
        CheckLose();
        return cardChange;
    }

    public List<CardChange> MoveToLeft()
    {
        List<CardChange> changeList = new List<CardChange>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                CardChange change = new CardChange();
                if (j == 0)
                {
                    tabIndex = j;
                }
                else
                {
                    if (intValue[i, j] == 0)
                    {
                        continue;
                    }
                    else if (intValue[i, j] != 0 && intValue[i, tabIndex] == 0)
                    {
                        intValue[i, tabIndex] = intValue[i, j];
                        intValue[i, j] = 0;

                        change.fromIndexX = i;
                        change.fromIndexY = j;
                        change.toIndexX = i;
                        change.toIndexY = tabIndex;
                        changeList.Add(change);
                    }
                    else if (intValue[i, j] != 0 && intValue[i, tabIndex] != 0 && intValue[i, j] != intValue[i, tabIndex])
                    {
                        intValue[i, tabIndex + 1] = intValue[i, j];
                        if (Mathf.Abs(j - tabIndex) > 1)
                        {
                            intValue[i, j] = 0;

                            change.fromIndexX = i;
                            change.fromIndexY = j;
                            change.toIndexX = i;
                            change.toIndexY = tabIndex + 1;
                            changeList.Add(change);
                        }
                        tabIndex++;
                    }
                    else if (intValue[i, j] != 0 && intValue[i, tabIndex] != 0 && intValue[i, j] == intValue[i, tabIndex])
                    {
                        intValue[i, tabIndex] *= 2;
                        intValue[i, j] = 0;

                        change.fromIndexX = i;
                        change.fromIndexY = j;
                        change.toIndexX = i;
                        change.toIndexY = tabIndex;
                        change.createNum = intValue[i, tabIndex];
                        changeList.Add(change);

                        tabIndex++;
                    }
                }
            }
        }
        return changeList;
    }

    public List<CardChange> MoveToRight()
    {
        List<CardChange> changeList = new List<CardChange>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j >= 0; j--)
            {
                CardChange change = new CardChange();
                if (j == 3)
                {
                    tabIndex = j;
                }
                else
                {
                    if (intValue[i, j] == 0)
                    {
                        continue;
                    }
                    else if (intValue[i, j] != 0 && intValue[i, tabIndex] == 0)
                    {
                        intValue[i, tabIndex] = intValue[i, j];
                        intValue[i, j] = 0;

                        change.fromIndexX = i;
                        change.fromIndexY = j;
                        change.toIndexX = i;
                        change.toIndexY = tabIndex;
                        changeList.Add(change);
                    }
                    else if (intValue[i, j] != 0 && intValue[i, tabIndex] != 0 && intValue[i, j] != intValue[i, tabIndex])
                    {
                        intValue[i, tabIndex - 1] = intValue[i, j];
                        if (Mathf.Abs(j - tabIndex) > 1)
                        {
                            intValue[i, j] = 0;

                            change.fromIndexX = i;
                            change.fromIndexY = j;
                            change.toIndexX = i;
                            change.toIndexY = tabIndex - 1;
                            changeList.Add(change);
                        }
                        tabIndex--;
                    }
                    else if (intValue[i, j] != 0 && intValue[i, tabIndex] != 0 && intValue[i, j] == intValue[i, tabIndex])
                    {
                        intValue[i, tabIndex] *= 2;
                        intValue[i, j] = 0;

                        change.fromIndexX = i;
                        change.fromIndexY = j;
                        change.toIndexX = i;
                        change.toIndexY = tabIndex;
                        change.createNum = intValue[i, tabIndex];
                        changeList.Add(change);

                        tabIndex--;
                    }
                }
            }
        }
        return changeList;
    }

    public List<CardChange> MoveToUp()
    {
        List<CardChange> changeList = new List<CardChange>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                CardChange change = new CardChange();
                if (j == 0)
                {
                    tabIndex = j;
                }
                else
                {
                    if (intValue[j, i] == 0)
                    {
                        continue;
                    }
                    else if (intValue[j, i] != 0 && intValue[tabIndex, i] == 0)
                    {
                        intValue[tabIndex, i] = intValue[j, i];
                        intValue[j, i] = 0;

                        change.fromIndexX = j;
                        change.fromIndexY = i;
                        change.toIndexX = tabIndex;
                        change.toIndexY = i;
                        changeList.Add(change);
                    }
                    else if (intValue[j, i] != 0 && intValue[tabIndex, i] != 0 && intValue[j, i] != intValue[tabIndex, i])
                    {
                        intValue[tabIndex + 1, i] = intValue[j, i];
                        if (Mathf.Abs(j - tabIndex) > 1)
                        {
                            intValue[j, i] = 0;

                            change.fromIndexX = j;
                            change.fromIndexY = i;
                            change.toIndexX = tabIndex + 1;
                            change.toIndexY = i;
                            changeList.Add(change);
                        }
                        tabIndex++;

                    }
                    else if (intValue[j, i] != 0 && intValue[tabIndex, i] != 0 && intValue[j, i] == intValue[tabIndex, i])
                    {
                        intValue[tabIndex, i] *= 2;
                        intValue[j, i] = 0;

                        change.fromIndexX = j;
                        change.fromIndexY = i;
                        change.toIndexX = tabIndex;
                        change.toIndexY = i;
                        change.createNum = intValue[tabIndex, i];
                        changeList.Add(change);

                        tabIndex++;
                    }
                }
            }
        }
        return changeList;
    }

    public List<CardChange> MoveToButtom()
    {
        List<CardChange> changeList = new List<CardChange>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j >= 0; j--)
            {
                CardChange change = new CardChange();
                if (j == 3)
                {
                    tabIndex = j;
                }
                else
                {
                    if (intValue[j, i] == 0)
                    {
                        continue;
                    }
                    else if (intValue[j, i] != 0 && intValue[tabIndex, i] == 0)
                    {
                        intValue[tabIndex, i] = intValue[j, i];
                        intValue[j, i] = 0;

                        change.fromIndexX = j;
                        change.fromIndexY = i;
                        change.toIndexX = tabIndex;
                        change.toIndexY = i;
                        changeList.Add(change);
                    }
                    else if (intValue[j, i] != 0 && intValue[tabIndex, i] != 0 && intValue[j, i] != intValue[tabIndex, i])
                    {
                        intValue[tabIndex - 1, i] = intValue[j, i];
                        if (Mathf.Abs(j - tabIndex) > 1)
                        {
                            intValue[j, i] = 0;

                            change.fromIndexX = j;
                            change.fromIndexY = i;
                            change.toIndexX = tabIndex - 1;
                            change.toIndexY = i;
                            changeList.Add(change);
                        }
                        tabIndex--;
                    }
                    else if (intValue[j, i] != 0 && intValue[tabIndex, i] != 0 && intValue[j, i] == intValue[tabIndex, i])
                    {
                        intValue[tabIndex, i] *= 2;
                        intValue[j, i] = 0;

                        change.fromIndexX = j;
                        change.fromIndexY = i;
                        change.toIndexX = tabIndex;
                        change.toIndexY = i;
                        change.createNum = intValue[tabIndex, i];
                        changeList.Add(change);

                        tabIndex--;
                    }
                }
            }
        }
        return changeList;
    }

    public void ResetGameState()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                intValue[i, j] = 0;
            }
        }
    }

    void CheckLose()
    {
        int lastValue = 0;
        for (int i = 0; i < 4; i++)
        {
            lastValue = 0;

            for (int j = 0; j < 4; j++)
            {
                if (intValue[i, j] == 0)
                {
                    return;
                }
                else if (j == 0)
                {
                    lastValue = intValue[i, j];
                }
                else
                {
                    if (lastValue == intValue[i, j])
                    {
                        return;
                    }
                    else
                    {
                        lastValue = intValue[i, j];
                    }
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            lastValue = 0;

            for (int j = 0; j < 4; j++)
            {
                if (intValue[j, i] == 0)
                {
                    return;
                }
                else if (j == 0)
                {
                    lastValue = intValue[j, i];
                }
                else
                {
                    if (lastValue == intValue[j, i])
                    {
                        return;
                    }
                    else
                    {
                        lastValue = intValue[j, i];
                    }
                }
            }
        }
        //输了
        loser();
    }
}
