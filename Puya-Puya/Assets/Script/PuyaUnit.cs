using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyaUnit : MonoBehaviour
{
    private Color[] colorArray = { Color.blue, Color.green, Color.red, Color.cyan };
    public bool activelyFalling = true;
    public bool forcedDownwards = false;

    public int colorIdx;

    void Awake()
    {
        colorIdx = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().color = colorArray[colorIdx];
    }

    public IEnumerator DropToFloor()
    {
        WaitForSeconds wait = new WaitForSeconds(.25f);
        Vector3 currentPos = RoundVector(gameObject.transform.position);
        for (int row = (int)currentPos.y - 1; row >= 0; row--)
        {
            int currentX = (int)currentPos.x;
            if (Grid.IsEmpty(currentX, row))
            {
                forcedDownwards = true;
                Grid.Clear(currentX, row + 1);
                Grid.Add(currentX, row, gameObject.transform);
                gameObject.transform.position += Vector3.down;
                yield return wait;
            }
            else
            {
                activelyFalling = false;
                forcedDownwards = false;
                break;
            }
        }
        forcedDownwards = false;
        activelyFalling = false;
    }

    public void DropToFloorExternal()
    {
        StartCoroutine(DropToFloor());
    }

    public Vector3 RoundVector(Vector3 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }
}
