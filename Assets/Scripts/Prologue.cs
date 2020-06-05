using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToMenu", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
