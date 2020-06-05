using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    private Intro intro;
    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {

        intro = GetComponentInParent<Intro>();

        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.IsGameOver())
        {
            rb2d.velocity = Vector2.zero;
        } else
        if(intro.started){
            rb2d.velocity = new Vector2(-intro.scrollSpeed, 0);
        }
	}
}
