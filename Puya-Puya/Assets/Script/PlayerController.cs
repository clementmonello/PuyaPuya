using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BlocPuya puya;
    public bool ControlleurP1;

    void Start()
    {
        puya = GetComponent<BlocPuya>();
    }

    void Update()
    {
        if (ControlleurP1 == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                puya.MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                puya.MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                puya.MoveDown();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                puya.RotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                puya.RotateRight();
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                FindObjectOfType<PauseManager>().CallPauseScreen();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                puya.MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                puya.MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                puya.MoveDown();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                puya.RotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                puya.RotateRight();
            }
        }
    }
}