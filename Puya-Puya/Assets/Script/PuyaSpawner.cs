using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuyaSpawner : MonoBehaviour
{
    private BlocPuya activePuyo;
    public Grid grid1;
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
        if (grid1.WhatToDelete())
        {
            StartCoroutine(DelayDelete());
        }

        StartCoroutine(DelaySpawn());
    }

    private bool GameIsOver()
    {
        if(ControlleurP1 ==  true){
        return
            grid1.gameBoard[2, 0] != null ||
            grid1.gameBoard[3, 0] != null;
        }else{
             return
            grid1.gameBoard[2, 11] != null ||
            grid1.gameBoard[3, 11] != null;
        }
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
        //else if () 
        //{
        //// wait black
        //}
        else
        {
            Debug.Log("spawn");
<<<<<<< Updated upstream
            activePuyo = Instantiate((GameObject)Resources.Load("Puya"), ControlleurP1 == true ? posSpawnP1 : posSpawnP2, Quaternion.identity).GetComponent<BlocPuya>();
=======
            activePuyo = Instantiate((GameObject)Resources.Load("Puya"), (ControlleurP1 == true? posSpawnP1:posSpawnP2), Quaternion.identity).GetComponent<BlocPuya>();
>>>>>>> Stashed changes
            activePuyo.grid = grid1;
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