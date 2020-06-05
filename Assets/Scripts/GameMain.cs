using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] Player gameControl;
    [SerializeField] Color backColor;

    [SerializeField] Spawner spawner1;
    [SerializeField] Spawner2 spawner2;
    bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsGameOver()) {
            music.Stop();
            music.loop = false;
            CancelInvoke();
        };

        if (GameManager.IsGamePaused())
        {
            music.Pause();
            CancelInvoke();
        } else
        {
            if (!music.isPlaying)
            {
                music.UnPause();

                InvokeRepeating("SpawnBullet", .5f, 3f);
            }
        }

    }

    public void StartGame()
    {
        Camera.main.GetComponent<BackgroundColorBlink>().StartBlinking();
        gameObject.GetComponentInChildren<ScaleUp>().ReScale();
        gameControl.AllowMove();
        GameManager.i.canvasInGame.SetActive(true);
        Camera.main.backgroundColor = backColor;
        gameStarted = true;
        InvokeRepeating("SpawnBullet", 3f, 3f);
    }

    public void SpawnBullet()
    {
        spawner1.SpawnObj1();
        spawner2.SpawnObj2();
    }
}
