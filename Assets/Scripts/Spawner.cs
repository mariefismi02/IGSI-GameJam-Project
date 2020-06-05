using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public Transform spawner;
    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnObj1();
        //SpawnObj2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObj1()
    {

        //Spawn Obj 1
        GameObject spawnObj = GameObject.Instantiate(objectToSpawn, transform.position, Quaternion.identity) as GameObject;
        spawnObj.transform.parent = GameObject.Find("InGame").transform;

        int index = Random.Range(0, GameManager.i.buttonSprite.Length);
        spawnObj.GetComponent<MoveToTarget1>().sprite = GameManager.i.buttonSprite[index];
        spawnObj.GetComponent<MoveToTarget1>().colorIndex = (GameManager.i.buttonSprite.Length - 1)  - index;
    }

    
}
