using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuyaSpawner : MonoBehaviour
{
    private BlocPuya activePuyo;
    public Grid Grid1;
    public bool ControlleurP1;
    public Vector2 posSpawnP1 = new Vector2(-5.9f, -5.2f);
    public Vector2 posSpawnP2 = new Vector2(5.2f, 5.9f);
    public Sprite[] listImagePlayer;
    public Image previewP1;
    public Image previewP2;

    void Start()
    {
        LoadPictures();
        SpawnPuyo();
    }

    public void SpawnPuyo()
    {
        if (Grid1.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        }
        StartCoroutine(DelaySpawn());
    }

    private bool GameIsOver()
    {
        if(ControlleurP1 ==  true){
        return
            Grid1.gameBoard[2, 0] != null ||
            Grid1.gameBoard[3, 0] != null;
        }else{
             return
            Grid1.gameBoard[2, 0] != null ||
            Grid1.gameBoard[3, 0] != null;
        }
    }

    IEnumerator DelayDelete() //ici c'est les combos
    {
        Grid1.DropAllColumns();
        yield return new WaitUntil(() => !Grid1.AnyFallingBlocks());
        if (Grid1.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        };
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitUntil(() => !Grid1.AnyFallingBlocks() && !Grid1.WhatToDelete());
        if (GameIsOver())
        {
            if (ControlleurP1 == true) 
            {
                enabled = false;
                Debug.Log("game over P1");
                Grid1.VictoryByFull(ControlleurP1);
            }
            else if(ControlleurP1 == false) 
            {
                enabled = false;
                Debug.Log("Game Over P2");
                Grid1.VictoryByFull(ControlleurP1);
            }
        }
        else
        {
            Debug.Log("spawn");
            activePuyo = Instantiate((GameObject)Resources.Load("Puya"), (ControlleurP1 == true? posSpawnP1:posSpawnP2), Quaternion.identity).GetComponent<BlocPuya>();            
            activePuyo.grid = Grid1;
            activePuyo.ps = this;
            activePuyo.GetComponent<PlayerController>().ControlleurP1 = ControlleurP1;
        }
        //grid1.DebugBoard();
    }

    public void LoadPictures()
    {
        if (ControlleurP1 == true)
        {
            previewP1.sprite = listImagePlayer[PlayerPrefs.GetInt("ImageP1")];
        }
        else
        {
            previewP2.sprite = listImagePlayer[PlayerPrefs.GetInt("ImageP2")];
        }
    }
}