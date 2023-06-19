using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyaSpawner : MonoBehaviour
{
    private BlocPuya activePuyo;

    void Start()
    {
        SpawnPuyo();
    }

    public void SpawnPuyo()
    {
        if (Grid.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        }

        StartCoroutine(DelaySpawn());
    }

    private bool GameIsOver()
    {
        return
            Grid.gameBoard[(int)transform.position.x, (int)transform.position.y] != null ||
            Grid.gameBoard[(int)transform.position.x + 1, (int)transform.position.y] != null;
    }

    IEnumerator DelayDelete()
    {
        Grid.DropAllColumns();
        yield return new WaitUntil(() => !Grid.AnyFallingBlocks());
        if (Grid.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        };

    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitUntil(() => !Grid.AnyFallingBlocks() && !Grid.WhatToDelete());
        if (GameIsOver())
        {
            GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().alpha = 1;
            enabled = false;
        }
        else
        {
            activePuyo = Instantiate((GameObject)Resources.Load("Puya"), transform.position, Quaternion.identity).GetComponent<BlocPuya>();
        }
    }
}
