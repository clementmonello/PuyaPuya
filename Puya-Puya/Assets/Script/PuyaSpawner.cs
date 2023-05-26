using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyaSpawner : MonoBehaviour
{
    private BlocPuya activePuyo;
    public Grid grid1;
    public Grid grid2;

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
        grid1.DebugBoard();
        return
            grid1.gameBoard[(int)transform.position.x, (int)transform.position.y] != null ||
            grid1.gameBoard[(int)transform.position.x + 1, (int)transform.position.y] != null;
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
            //GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().alpha = 1;
            enabled = false;
            Debug.Log("game over");
        }
        else
        {
            activePuyo = Instantiate((GameObject)Resources.Load("Puya"), transform.position, Quaternion.identity).GetComponent<BlocPuya>();
            activePuyo.grid = grid1;
            activePuyo.ps = this;
        }
    }
}
