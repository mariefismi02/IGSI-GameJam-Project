using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public int score = 0;

    public int missedPoint = 0;

    public GameObject canvasGameOver;
    public GameObject canvasInGame;
    public GameObject canvasGamePaused;
    public GameObject gameOverPanel;
    public Button[] p1Buttons;
    public Button[] p2Buttons;
    public Sprite[] buttonSprite;
    public Color[] buttonsColor;
    public GameObject floatingText;
    public Text scoreText;
    public GameObject P1Text;
    public GameObject P2Text;
    public Animator coffinAnimator;
    public AudioClip gameOverMusic;
    public AudioClip audioClick;
    public AudioClip audioCorrect;
    public AudioClip audioWrong;

    public bool p1Click, p2Click = false;
    private float callDelay = 0;//Delay for calling time
    private float timer = 0;//
    private bool gameOver;
    private AudioSource audio;

    private bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        if (i == null)
            i = this;
        else if (i != this)
            Destroy(gameObject);

        foreach(var button in p1Buttons)
        {
            button.onClick.AddListener(() => { P1ButtonListener(button); });
        }
        foreach (var button in p2Buttons)
        {
            button.onClick.AddListener(() => { P2ButtonListener(button); });
        }

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canvasInGame.activeSelf && !gameOver)
        {
            TogglePause();
        }

        if (gamePaused)
        {
            canvasGamePaused.SetActive(true);
        } else
        {
            canvasGamePaused.SetActive(false);
        }
    }

    public static bool IsGamePaused()
    {
        return i.gamePaused;
    }

    public void TogglePause()
    {
        gamePaused = !gamePaused;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void P1ButtonListener(Button button)
    {
        GameObject parent = GameObject.Find("P1");
        GameObject targetObject = GameObject.FindGameObjectWithTag("ButtonP1");
        GameObject targetObject2 = GameObject.FindGameObjectWithTag("ButtonP2");

        string txt = "";
        if ((targetObject != null))
        {
            Image image = button.gameObject.GetComponent<Image>();
            Debug.Log((image.sprite == targetObject.GetComponent<SpriteRenderer>().sprite));
            if (image.sprite == targetObject.GetComponent<SpriteRenderer>().sprite)
            {
                if (targetObject.GetComponent<MoveToTarget1>().touch)
                {
                    p1Click = true;
                }
                else
                {
                    txt = "TOO FAST";
                    missedPoint++;
                    coffinAnimator.SetInteger("dance", 0);
                    targetObject.GetComponent<AudioSource>().clip = audioWrong;
                    targetObject.GetComponent<AudioSource>().Play();

                    BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

                    cameraBackgroundBlink.SetDefaultColors();
                }
            }
            else
            {
                txt = "WRONG";
                missedPoint++;

                coffinAnimator.SetInteger("dance", 0);
                targetObject.GetComponent<AudioSource>().clip = audioWrong;
                targetObject.GetComponent<AudioSource>().Play();

                BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

                cameraBackgroundBlink.SetDefaultColors();
            }

        }
        else
        {
            txt = "STOP";
            missedPoint++;
            coffinAnimator.SetInteger("dance", 0);
            audio.clip = audioWrong;
            audio.Play();

            BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

            cameraBackgroundBlink.SetDefaultColors();
        }
        ShowFloatingText(txt, parent.gameObject.transform.position, parent.transform, Color.white);
    }

    public void P2ButtonListener(Button button)
    {
        GameObject parent = GameObject.Find("P2");
        GameObject targetObject = GameObject.FindGameObjectWithTag("ButtonP2");
        GameObject targetObject2 = GameObject.FindGameObjectWithTag("ButtonP1");
        string txt = "";
        if ((targetObject != null)) {
            Image image = button.gameObject.GetComponent<Image>();
            if(image.sprite == targetObject.GetComponent<SpriteRenderer>().sprite){
                if (targetObject.GetComponent<MoveToTarget2>().touch)
                {
                    p2Click = true;
                }
                else
                {
                    txt = "TOO FAST";
                    missedPoint++;
                    coffinAnimator.SetInteger("dance", 0);
                    targetObject.GetComponent<AudioSource>().clip = audioWrong;
                    targetObject.GetComponent<AudioSource>().Play();

                    BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

                    cameraBackgroundBlink.SetDefaultColors();
                }
            }
            else
            {
                txt = "WRONG";
                missedPoint++;

                coffinAnimator.SetInteger("dance", 0);
                targetObject.GetComponent<AudioSource>().clip = audioWrong;
                targetObject.GetComponent<AudioSource>().Play();

                BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

                cameraBackgroundBlink.SetDefaultColors();
            }

        } else
        {
            txt = "STOP";
            missedPoint++;
            coffinAnimator.SetInteger("dance", 0);
            audio.clip = audioWrong;
            audio.Play();

            BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

            cameraBackgroundBlink.SetDefaultColors();
        }
        ShowFloatingText(txt, parent.gameObject.transform.position, parent.transform, Color.white);
    }

    public static void ShowFloatingText(string txt, Vector3 position, Transform parent, Color color)
    {
        GameObject spawnObj = Instantiate(GameManager.i.floatingText, position, Quaternion.identity) as GameObject;
        spawnObj.transform.parent = parent;
        spawnObj.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        spawnObj.GetComponent<MeshRenderer>().sortingOrder = 5;
        spawnObj.GetComponent<TextMesh>().text = txt;
        spawnObj.GetComponent<TextMesh>().color = color;
    }
    public static void AddScore(int val)
    {
        i.score += val;
        i.scoreText.text = i.score.ToString();
        i.scoreText.GetComponent<ScaleUp>().ReScale();
    }

    public void ShowGameOverMessage()
    {
        i.canvasGameOver.SetActive(true);

        i.audio.Stop();
    }
    
    public static void GameOver()
    {
        i.gameOver = true;

        i.gameOverPanel.SetActive(true);

        i.audio.clip = i.gameOverMusic;
        i.audio.Play();

        i.coffinAnimator.SetInteger("dance", 0);
        i.coffinAnimator.SetBool("gameOver", true);

        i.Invoke("ShowGameOverMessage", 6f);
        
        int currentScore = PlayerPrefs.GetInt("score", 0);
        if (i.score > currentScore)
        {
            PlayerPrefs.SetInt("score", i.score);
        }

    }

    public void AudioClick()
    {
        audio.clip = audioClick;
        audio.Play();
    }

    public static bool IsGameOver()
    {
        return i.gameOver;
    }
}
