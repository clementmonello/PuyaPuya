using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuyaUnit : MonoBehaviour
{
    public bool activelyFalling = true;
    public bool forcedDownwards = false;
    public float gridStep = 0.7f;
    public int colorIdx;

    public Grid grid;


    public IEnumerator DropToFloor()
    {
        WaitForSeconds wait = new WaitForSeconds(.25f);

        Vector3 currentPos = WorldPosToGridPos(gameObject.transform.position);
        print("CurrentPos : " + currentPos);
        for (int row = Convert.ToInt32(currentPos.y) +1; row <12; row++)
        {
            int currentY = Convert.ToInt32(currentPos.x);
            Debug.Log("currentX " + currentY);
            Debug.Log("ligne " + row);
            if (grid.IsEmpty(currentY, row))
            {
                forcedDownwards = true;
                grid.ClearByGridPos(currentY, row-1);
                grid.AddByGridPos(currentY, row, gameObject.transform);
                gameObject.transform.position += Vector3.down*gridStep;
                yield return wait;
            }
            else
            {
                break;
            }
        }
        activelyFalling = false;
        forcedDownwards = false;
    }

    public void DropToFloorExternal()
    {
        StartCoroutine(DropToFloor());
    } 

    public Vector3 WorldPosToGridPos(Vector3 pos)
    {
        Vector3 posRetour = new Vector3();

        if (gameObject.transform.position.x < 0)//grille de gauche  position:-600,0  size:450,900
        {
            posRetour.x = Convert.ToInt32((pos.x - (-3.8f)) / -0.7f);
            posRetour.y = Convert.ToInt32((pos.y - 3.8f) / -0.7f);
        }
        else
        {
            posRetour.x = Convert.ToInt32((pos.x - 3.8f) / 0.7f);
            posRetour.y = Convert.ToInt32((pos.y - 3.8f) / 0.7f);
        }
        //Debug.Log(posRetour);
        return posRetour;
    }

    public Vector3 RoundVector(Vector3 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }
}
