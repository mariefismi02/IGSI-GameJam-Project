using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorBlink : MonoBehaviour
{
    public Color[] color;

    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColors(Color[] col)
    {
        color = col;
    }

    IEnumerator Blinking()
    {
        for(int i=0; i<color.Length; i++)
        {
            camera.backgroundColor = color[i];
            if (i == color.Length - 1) i = 0;
            yield return new WaitForSeconds(.75f);
        }
    }

    public void StartBlinking()
    {
        StartCoroutine("Blinking");
    }

    public void SetDefaultColors()
    {
        Color[] colors = color;
        colors[1] = color[0];
        colors[2] = color[0];

        SetColors(colors);
    }
}
