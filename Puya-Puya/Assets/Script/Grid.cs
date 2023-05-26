using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public Transform[,] gameBoard = new Transform[6, 12];

    public bool WithinBorders(Vector3 target)
    {
        return target.x > -1 &&
            target.x < 6 &&
            target.y > -1 &&
            target.y < 12;
    }
    public bool FreeSpace(Vector3 target, Transform parentTransform)
    {
        if (WithinBorders(target))
        {
            return gameBoard[(int)target.x, (int)target.y] == null ||
                gameBoard[(int)target.x, (int)target.y].parent == parentTransform;
        }
        return false;
    }

    public bool IsEmpty(int col, int row)
    {
        if (WithinBorders(new Vector3(col, row, 0)))
        {
            return gameBoard[col, row] == null;
        }
        return false;
    }

    public bool ColorMatches(int col, int row, Transform puyoUnit)
    {
        if (WithinBorders(new Vector3(col, row, 0)))
        {
            return gameBoard[col, row].GetComponent<PuyaUnit>().colorIdx == puyoUnit.GetComponent<PuyaUnit>().colorIdx;
        }
        return false;
    }

    public bool HasMatchingNeighbor(Vector2 pos, Vector3 direction, Transform puyoUnitTransform)
    {
        Vector2 newPos = new Vector2(pos.x + direction.x, pos.y + direction.y);
        return !IsEmpty((int)newPos.x, (int)newPos.y) && ColorMatches((int)newPos.x, (int)newPos.y, puyoUnitTransform);
    }

    public void Clear(float col, float row)
    {
        gameBoard[(int)col, (int)row] = null;
    }

    public void Add(float col, float row, Transform obj)
    {
        gameBoard[(int)col, (int)row] = obj;
    }

    public void Delete(Transform puyo)
    {
        Vector2 pos = new Vector2(Mathf.Round(puyo.position.x), Mathf.Round(puyo.position.y));
        gameBoard[(int)pos.x, (int)pos.y] = null;
        UnityEngine.Object.Destroy(puyo.gameObject);
    }

    public bool WhatToDelete()
    {
        List<Transform> groupToDelete = new List<Transform>();

        for (int row = 0; row < 12; row++)
        {
            for (int col = 0; col < 6; col++)
            {
                List<Transform> currentGroup = new List<Transform>();

                if (gameBoard[col, row] != null)
                {
                    Transform current = gameBoard[col, row];
                    if (groupToDelete.IndexOf(current) == -1)
                    {
                        AddNeighbors(current, currentGroup);
                    }
                }

                if (currentGroup.Count >= 4)
                {
                    foreach (Transform puyo in currentGroup)
                    {
                        groupToDelete.Add(puyo);
                    }
                }
            }
        }

        if (groupToDelete.Count != 0)
        {
            DeleteUnits(groupToDelete);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DropAllColumns()
    {
        for (int row = 0; row < 12; row++)
        {
            for (int col = 0; col < 6; col++)
            {
                if (gameBoard[col, row] != null)
                {
                    Transform puyoUnit = gameBoard[col, row];
                    puyoUnit.gameObject.GetComponent<PuyaUnit>().DropToFloorExternal();
                }
            }
        }
    }

    public void AddNeighbors(Transform currentUnit, List<Transform> currentGroup)
    {
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.right, Vector3.left };
        if (currentGroup.IndexOf(currentUnit) == -1)
        {
            currentGroup.Add(currentUnit);
        }
        else
        {
            return;
        }

        foreach (Vector3 direction in directions)
        {
            int nextX = (int)(Mathf.Round(currentUnit.position.x) + Mathf.Round(direction.x));
            int nextY = (int)(Mathf.Round(currentUnit.position.y) + Mathf.Round(direction.y));

            if (!IsEmpty(nextX, nextY) && ColorMatches(nextX, nextY, currentUnit))
            {
                Transform nextUnit = gameBoard[nextX, nextY];
                AddNeighbors(nextUnit, currentGroup);
            }
        }
    }

    public void DeleteUnits(List<Transform> unitsToDelete)
    {
        foreach (Transform unit in unitsToDelete)
        {
            Delete(unit);
        }
    }

    public bool AnyFallingBlocks()
    {
        for (int row = 11; row >= 0; row--)
        {
            for (int col = 0; col < 6; col++)
            {
                if (gameBoard[col, row] != null)
                {
                    if (gameBoard[col, row].gameObject.GetComponent<PuyaUnit>().forcedDownwards)
                    {
                        return true;
                    }
                    else if (gameBoard[col, row].gameObject.GetComponent<PuyaUnit>().activelyFalling)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void DebugBoard()
    {
        string res = "";

        for (int row = 11; row >= 0; row--)
        {
            res += $"{row} :";
            for (int col = 0; col < 6; col++)
            {
                if (gameBoard[col, row] == null)
                {
                    res += "o ";
                }
                else
                {
                    int idx = gameBoard[col, row].gameObject.GetComponent<PuyaUnit>().colorIdx;
                    string[] colorArray = { "B", "G", "Y", "V" };
                    res += $"{colorArray[idx]} ";
                }
            }
            res += "\n";
        }
        Debug.Log(res);
    }
}
