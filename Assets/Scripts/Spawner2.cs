using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    //public Transform spawner;
    public GameObject objectToSpawn2;
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

    

    public void SpawnObj2()
    {

        //Spawn Obj 1
        GameObject spawnObj = GameObject.Instantiate(objectToSpawn2, transform.position, Quaternion.identity) as GameObject;
        spawnObj.transform.parent = GameObject.Find("InGame").transform;

        int index = Random.Range(0, GameManager.i.buttonSprite.Length);
        spawnObj.GetComponent<MoveToTarget2>().sprite = GameManager.i.buttonSprite[index];
        spawnObj.GetComponent<MoveToTarget2>().colorIndex = index;

    }
}
