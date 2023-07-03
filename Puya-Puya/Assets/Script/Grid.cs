using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Grid : MonoBehaviour
{
    public Text PointP1, PointP2;
    public bool P1;
    public Transform[,] gameBoard = new Transform[6, 12];
    public Text TextP1,TextP2;
    public GameObject BackgroundVictoryP1,BackgroundVictoryP2;
    private int pointParPuya = 100, resultat,resultatP1,resultatP2;
    public AudioSource Explosion;

    public void VictoryByPoint() 
    {
        if(resultatP1 >= 10000 ) 
        {
            TextP1.text = "You Win" ;
            TextP2.text = "You Loose";
            BackgroundVictoryP1.SetActive(true);
            BackgroundVictoryP2.SetActive(true);
            Time.timeScale = 0;
        }
        else if( resultatP2 >= 10000) 
        {
            TextP2.text = "You Win";
            TextP1.text = "You Loose";
            BackgroundVictoryP1.SetActive(true);
            BackgroundVictoryP2.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void VictoryByFull(bool OverP1) 
    {
        if (OverP1 == false)
        {
            TextP1.text = "You Win";
            TextP2.text = "You Loose";
            BackgroundVictoryP1.SetActive(true);
            BackgroundVictoryP2.SetActive(true);
            Time.timeScale = 0;
        }
        else if (OverP1 == true)
        {
            TextP2.text = "You Win";
            TextP1.text = "You Loose";
            BackgroundVictoryP1.SetActive(true);
            BackgroundVictoryP2.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void AddScore(int resultat) 
    {
        if (P1 == true) 
        {
            resultatP1 += resultat;
            PointP1.text = resultatP1.ToString();
        }
        else 
        {
            resultatP2 += resultat;
            PointP2.text = resultatP2.ToString();
        }
        VictoryByPoint();
    }

    public bool WithinBorders(Vector3 target)
{
    int x = Convert.ToInt32(target.x);
    int y = Convert.ToInt32(target.y);

    bool withinPositiveBounds = x >= 0 && x < 6 && y >= 0 && y < 12;
    
    bool withinNegativeBounds = x >= -6 && x < 0 && y >= -12 && y < 0;

    return withinPositiveBounds || withinNegativeBounds;
}

   public bool FreeSpace(Vector3 target, Transform parentTransform)
{
    Vector3 v = WorldPosToGridPos(target);
    if (WithinBorders(v))
    {
          int x = Convert.ToInt32(v.x);
        int y = Convert.ToInt32(v.y);

        if (x >= 0 && x < gameBoard.GetLength(0) && y >= 0 && y < gameBoard.GetLength(1))
        {
            return gameBoard[x, y] == null || gameBoard[x, y].parent == parentTransform;
        }
    }
    return false;
}

    public bool IsEmpty(int col, int row)
    {
        //Debug.Log("next row within border: " + WithinBorders(new Vector3(col, row, 0)));
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

        Vector3 v=WorldPosToGridPos(new Vector3(col,row));

        //print(row);
        //print(col);
        //print(v);

        gameBoard[Convert.ToInt32(v.x), Convert.ToInt32(v.y)] = null;
    }

    public void ClearByGridPos(int col, int row)
    {
        gameBoard[col, row] = null;
    }

    public void Add(float col, float row, Transform obj)
    {
        Vector3 v = WorldPosToGridPos(new Vector3(col, row));

        Debug.Log("before convertion" + col + "," + row);
        Debug.Log("after convertion" + v);

        gameBoard[Convert.ToInt32(v.x), Convert.ToInt32(v.y)] = obj;
    }

    public void AddByGridPos(int col, int row, Transform obj)
    {
        gameBoard[col, row] = obj;
    }

    public void Delete(Transform puyo)
    {
        Vector3 pos = puyo.position;
        Vector3 gridPos = WorldPosToGridPos(pos);

        int col = (int)gridPos.x;
        int row = (int)gridPos.y;

        if (WithinBorders(gridPos) && gameBoard[col, row] != null)
        {
            gameBoard[col, row] = null;
            UnityEngine.Object.Destroy(puyo.gameObject);
        }
        
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
            resultat = groupToDelete.Count * pointParPuya;
            Explosion.Play();
            DeleteUnits(groupToDelete);
            DropAllColumns();
            AddScore(resultat);
            resultat = 0;
            return true;
        }
        else
        {
            DropAllColumns();
            return false;
        }
    }

    public void DropAllColumns()
    {
        for (int row = 11; row >= 0; row--)
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
            Vector3 neighborPos = currentUnit.position + direction;
            Vector3 gridPos = WorldPosToGridPos(neighborPos);

            if (WithinBorders(gridPos) && !IsEmpty((int)gridPos.x, (int)gridPos.y) && ColorMatches((int)gridPos.x, (int)gridPos.y, currentUnit))
            {
                Transform nextUnit = gameBoard[(int)gridPos.x, (int)gridPos.y];

                // Ajouter des messages de débogage ici
                Debug.Log("Neighbor detected at (" + (int)gridPos.x + ", " + (int)gridPos.y + ")");

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
                        Debug.Log("fd");
                        return true;
                    }
                    else if (gameBoard[col, row].gameObject.GetComponent<PuyaUnit>().activelyFalling)
                    {
                        Debug.Log("af");
                        return true;
                    }
                    else
                    {
                        Debug.Log("no falling blocks");
                    }
                }
            }
        }
        return false;
    }

    public Vector3 WorldPosToGridPos(Vector3 pos)
    {
        Vector3 posRetour = new Vector3();
        if (gameObject.transform.position.x < 0)
        {
            posRetour.x = Convert.ToInt32((pos.x - (-3.8f)) / -0.7f);
            posRetour.y = Convert.ToInt32((pos.y - 3.8f) / -0.7f);
        }
        else
        {
            posRetour.x = Convert.ToInt32((pos.x - 3.8f) / 0.7f);
            posRetour.y = Convert.ToInt32((pos.y - 3.8f) / -0.7f);
        }
        return posRetour;
    }
}