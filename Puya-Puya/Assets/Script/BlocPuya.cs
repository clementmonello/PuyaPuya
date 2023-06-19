using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocPuya : MonoBehaviour
{
    public GameObject[] unitArray = new GameObject[2];

    public float fallSpeed = 1;

    public float interval = 0;

    private Vector3 left = Vector3.left;
    private Vector3 right = Vector3.right;
    private Vector3 down = Vector3.down;
    private Vector3 up = Vector3.up;

    private float gridStep = 0.7f;

    private bool puyoUnitDropsFinished = false;

    public PuyaSpawner ps;

    public Grid grid;

    void Start()
    {
        if (transform.position.x < 0)
        {
            unitArray[0] = Instantiate(GetRandomPuyaPrefab(), new Vector2(ps.posSpawnP1.x, 3.8f), Quaternion.identity);
            unitArray[1] = Instantiate(GetRandomPuyaPrefab(), new Vector2(ps.posSpawnP1.y, 3.8f), Quaternion.identity);
        }
        else
        {
            unitArray[0] = Instantiate(GetRandomPuyaPrefab(), new Vector2(ps.posSpawnP2.x, 3.8f), Quaternion.identity);
            unitArray[1] = Instantiate(GetRandomPuyaPrefab(), new Vector2(ps.posSpawnP2.y, 3.8f), Quaternion.identity);
        }
        unitArray[0].transform.parent = gameObject.transform;
        unitArray[1].transform.parent = gameObject.transform;
        unitArray[0].GetComponent<PuyaUnit>().grid = grid;
        unitArray[1].GetComponent<PuyaUnit>().grid = grid;
        UpdateGameBoard();
    }

    GameObject GetRandomPuyaPrefab()
    {
        string[] puyoPrefabNames = { "PuyaUnitBlue", "PuyaUnitGreen", "PuyaUnitViolet", "PuyaUnitYellow", "PuyaUnitRed" };

        string randomPrefabName = puyoPrefabNames[Random.Range(0, puyoPrefabNames.Length)];

        return (GameObject)Resources.Load(randomPrefabName);
    }

    void Update()
    {
        AutoDrop();       
    }

    void AutoDrop()

    {
        if (interval > fallSpeed)
        {
            MoveDown();
            interval = 0;
        }
        else
        {
            interval += Time.deltaTime;
        }
    }


    public void MoveLeft()
    {
        if (ValidMove(left))
        {
            Move(left, transform);
        }
    }

    public void MoveRight()
    {
        if (ValidMove(right))
        {
            Move(right, transform);
        }
    }

    public void MoveDown()
    {
        if (ValidMove(down))
        {
            Move(down, transform);
        }
        else
        {
            DisableSelf();
        }
    }

    public void RotateLeft()
    {
        Vector3 vect = GetClockwiseRotationVector();

        if (ValidRotate(vect))
        {
            Move(vect, unitArray[1].transform);
        }
    }

    public void RotateRight()
    {
        Vector3 vect = GetCounterClockwiseRotationVector();
        if (ValidRotate(vect))
        {
            Move(vect, unitArray[1].transform);
        }
    }

    void Move(Vector3 vector, Transform target)
    {
        ClearCurrentGameboardPosition();
        target.position += vector*gridStep;
        UpdateGameBoard();
    }

    void ClearCurrentGameboardPosition()
    {
        foreach (Transform puyaUnit in transform)
        {

            grid.Clear(puyaUnit.transform.position.x, puyaUnit.transform.position.y);
        }
    }

    void UpdateGameBoard()
    {
        foreach (Transform puyaUnit in transform)
        {

            grid.Add(puyaUnit.position.x, puyaUnit.position.y, puyaUnit);
        }
    }

    Vector3 GetClockwiseRotationVector()
    {
        Vector3 puyaUnitPos = unitArray[1].transform.position;
        print("puyaUnitPos" + puyaUnitPos);

        if (Vector3.Distance(puyaUnitPos + left * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(-1, -1);
        }
        else if (Vector3.Distance(puyaUnitPos + up * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(-1, +1);
        }
        else if (Vector3.Distance(puyaUnitPos + right * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(+1, +1);
        }
        else if (Vector3.Distance(puyaUnitPos + down * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(+1, -1);
        }
        else { return new Vector3(0, 0); }
        
    }

    Vector3 GetCounterClockwiseRotationVector()
    {
        Vector3 puyaUnitPos = unitArray[1].transform.position;

        print("RoundVector"+puyaUnitPos);
        print("Position"+unitArray[0].transform.position);

        if (Vector3.Distance(puyaUnitPos + left * gridStep, unitArray[0].transform.position) < 0.1f)
        {
             return new Vector3(-1, +1);
        }
        else if (Vector3.Distance(puyaUnitPos + up * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(+1, +1);
        }
        else if (Vector3.Distance(puyaUnitPos + right * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(+1, -1);
        }
        else if (Vector3.Distance(puyaUnitPos + down * gridStep, unitArray[0].transform.position) < 0.1f)
        {
            return new Vector3(-1, -1);
        }

        return new Vector3(0, 0);
    }

    bool ActivelyFalling()
    {
        return unitArray[0].GetComponent<PuyaUnit>().activelyFalling ||
            unitArray[1].GetComponent<PuyaUnit>().activelyFalling;
    }

  

    public bool ValidMove(Vector3 direction)
    {
        foreach (Transform puya in transform)
        {
            Vector3 newPosition = new Vector3(puya.position.x + direction.x* gridStep, puya.position.y + direction.y* gridStep, 0);
            if (!grid.FreeSpace(newPosition, transform))
            {
                return false;
            }
        }
        return true;
    }

    bool ValidRotate(Vector3 direction)
    {
        Vector3 puyaPos = unitArray[1].transform.position;
        Vector3 newPosition = new Vector3(puyaPos.x + direction.x, puyaPos.y + direction.y);

        return grid.FreeSpace(newPosition, transform);
    }

    private void DropPuyaUnits()
    {
        foreach (Transform puyaUnit in transform)
        {
            StartCoroutine(puyaUnit.gameObject.GetComponent<PuyaUnit>().DropToFloor());
        }
    }


    public Vector3 RoundVector(Vector3 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    void DisableSelf()
    {
        gameObject.GetComponent<PlayerController>().enabled = false;
        DropPuyaUnits();
        enabled = false;
        StartCoroutine(SpawnNextBlock());
    }

    IEnumerator SpawnNextBlock()
    {
        yield return new WaitUntil(() => !ActivelyFalling());

        ps.SpawnPuyo();
    }
}
