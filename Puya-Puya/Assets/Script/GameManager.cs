using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.VisualScripting;
using BaseTemplate.Behaviours;

public class GameManager : MonoSingleton<GameManager>
{
    public List<GameObject> listPuyaAvailable;
    public List<GameObject> listPuyaP1;
    public List<GameObject> listPuyaP2;
    [SerializeField] Grid gridP1;
    [SerializeField] Grid gridP2;
    [SerializeField] Vector2 posSpawnP1=new Vector2(-5.9f,-5.2f);
    [SerializeField] Vector2 posSpawnP2=new Vector2(5.22f,5.92f);
    
    [SerializeField] float timer=1f;

    public void spawnPuya()
    {
        int puya1 = Random.Range(0, listPuyaAvailable.Count);
        int puya2 = Random.Range(0, listPuyaAvailable.Count);
        Instantiate(listPuyaAvailable[puya1], new Vector3(posSpawnP1.x, 3.82f), new Quaternion());
        Instantiate(listPuyaAvailable[puya2], new Vector3(posSpawnP1.y, 3.82f), new Quaternion());
        listPuyaP1.Add(listPuyaAvailable[puya1]);
        gridP1.grid[0][2] = listPuyaAvailable[puya1].GetComponent<PuyaData>().p;
        gridP1.grid[0][3] = listPuyaAvailable[puya2].GetComponent<PuyaData>().p;
    }
    public void spawnPuya2()
    {
        int puya1 = Random.Range(0, listPuyaAvailable.Count);
        int puya2 = Random.Range(0, listPuyaAvailable.Count);
        Instantiate(listPuyaAvailable[puya1], new Vector3(posSpawnP2.x, 3.82f), new Quaternion());
        Instantiate(listPuyaAvailable[puya2], new Vector3(posSpawnP2.y, 3.82f), new Quaternion());
        gridP2.grid[0][2] = listPuyaAvailable[puya1].GetComponent<PuyaData>().p;
        gridP2.grid[0][3] = listPuyaAvailable[puya2].GetComponent<PuyaData>().p;
    }



    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1f;
            gridP1.pushDownGrid();
            gridP2.pushDownGrid();
        }
    }

    public void loadMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
