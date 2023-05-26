using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BlocPuya puya;

    void Start()
    {
        puya = GetComponent<BlocPuya>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            puya.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            puya.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            puya.MoveDown();
        }
        else if (Input.GetKeyDown("z"))
        {
            puya.RotateLeft();
        }
        else if (Input.GetKeyDown("x"))
        {
            puya.RotateRight();
        }
    }
}
