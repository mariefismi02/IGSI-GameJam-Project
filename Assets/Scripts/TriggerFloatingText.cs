using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFloatingText : MonoBehaviour
{

    public GameObject floatingTextPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PopUpFloatingText();
        }
    }

    public void PopUpFloatingText()
    {
        GameObject spawnObj = GameObject.Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity) as GameObject;
        spawnObj.transform.parent = this.gameObject.transform;
    }
}
