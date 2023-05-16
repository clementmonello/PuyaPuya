using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    int nbRow = 12;
    int nbColumn = 6;
    public List<List<Puya>> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new List<List<Puya>>();
        for (int i = 0; i < nbRow; i++)
        {
            grid.Add(new List<Puya>());
            for (int j = 0; j < nbColumn; j++)
            {
                grid[i].Add(null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool inBound(int x, int y)
    {
        if (x >= 0 && x < nbRow && y >= 0 && y < nbColumn)
        {
            return true;
        }
        else return false;
    }

    public bool isFree(int x,int y)
    {
        if (grid[x][y] == null)
        {
            return true;
        }
        else return false;
    }

    public void pushDownGrid()
    {
        for(int i = nbRow - 2; i >= 0; i--)
        {
            for (int j =0; j < nbColumn-1; j++)
            {
                if (isFree(i + 1, j))
                {
                    grid[i + 1][j] = grid[i][j];
                    grid[i][j] = null;
                }
            }
        }
    }

    public void debugGrid()
    {
        for (int i = 0; i < nbRow; i++)
        {
            string res = "";
            for (int j = 0; j < nbColumn; j++)
            {
                try
                {
                    res += grid[i][j].color.ToString() + " ";
                }
                catch(Exception e)
                {
                    res += 0 + " ";
                }
            }
            Debug.Log(res);
        }
    }
}
