using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {

        int currentScore = PlayerPrefs.GetInt("score", 0);

        scoreText.text = "HIGH SCORE: " + currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
