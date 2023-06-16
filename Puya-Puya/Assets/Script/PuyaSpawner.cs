using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyaSpawner : MonoBehaviour
{
    private BlocPuya activePuyo;
    public Grid grid1;
    public Grid grid2;
    public Vector2 posSpawnP1 = new Vector2(-5.9f, -5.2f);
    public Vector2 posSpawnP2 = new Vector2(5.2f, 5.9f);

    void Start()
    {
        SpawnPuyo();
    }

    public void SpawnPuyo()
    {
        if (grid1.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        }

        StartCoroutine(DelaySpawn());
    }

    private bool GameIsOver()
    {
        return
            grid1.gameBoard[2, 0] != null ||
            grid1.gameBoard[3, 0] != null;
    }

    IEnumerator DelayDelete() //ici c'est les combos
    {
        grid1.DropAllColumns();
        yield return new WaitUntil(() => !grid1.AnyFallingBlocks());
        if (grid1.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        };
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitUntil(() => !grid1.AnyFallingBlocks() && !grid1.WhatToDelete());
        if (GameIsOver())
        {
            enabled = false;
            Debug.Log("game over");
        }
        else
        {
            Debug.Log("spawn");
            activePuyo = Instantiate((GameObject)Resources.Load("Puya"), posSpawnP1, Quaternion.identity).GetComponent<BlocPuya>();
            activePuyo.grid = grid1;
            activePuyo.ps = this;
        }
        //grid1.DebugBoard();
    }
}
