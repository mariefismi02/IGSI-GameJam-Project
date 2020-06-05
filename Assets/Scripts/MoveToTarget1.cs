using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget1 : MonoBehaviour
{
    public Sprite sprite;

    Transform rotationCenter;

    private Rigidbody2D rb;

    [SerializeField]
    float rotationRadius = 2f;

    float posX, posY, angle = 0f;

    BackgroundColorBlink cameraBackgroundBlink;
    AudioSource audio;
    public AudioSource Audio
    {
        get { return audio; }
    }

    public float angularSpeed = 2f;

    float scale = 0;
    public bool touch = false;
    public bool destroy = false;
    public int colorIndex = 0;
    private void Awake()
    {
        rotationCenter = GameObject.FindGameObjectWithTag("RotationPoint").transform;
        rb = GetComponent<Rigidbody2D>();
        Vector2 p1 = rotationCenter.position;
        Vector2 p2 = transform.position;
        angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;
        audio = GetComponent<AudioSource>();
        cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();
    }

    private void Start()
    {
        GameManager.i.p1Click = false;
        GameManager.i.P1Text.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        if (sprite != null)
            GetComponent<SpriteRenderer>().sprite = sprite;

        transform.localScale = Vector3.one * scale;

        scale = Mathf.Lerp(scale, destroy ? 0 : 1, .25f);
    }

    private void FixedUpdate()
    {
        if (GameManager.i.p1Click)
        {
            if (GameManager.i.p2Click)
            {
                GameObject other = GameObject.FindGameObjectWithTag("ButtonP2");
                
                GameManager.AddScore(20);
                GameManager.ShowFloatingText("GOOD", transform.position, transform.parent, Color.black);
                GameManager.i.p1Click = false;
                GameManager.i.p2Click = false;
                GameManager.i.coffinAnimator.SetInteger("dance", Random.Range(1, 5));
                audio.clip = GameManager.i.audioCorrect;
                audio.Play();

                Color[] colors = cameraBackgroundBlink.color;
                colors[1] = GameManager.i.buttonsColor[colorIndex];
                colors[2] = GameManager.i.buttonsColor[other.GetComponent<MoveToTarget2>().colorIndex];

                cameraBackgroundBlink.SetColors(colors);

                Destroy(gameObject);
                Destroy(other);
            }
            return;
        }

        if (destroy)
        {
            if (scale <= 0.1f)
            {
                GameManager.i.coffinAnimator.SetInteger("dance", 0);

                GameManager.ShowFloatingText("MISS", transform.position, transform.parent, Color.white);
                Destroy(gameObject);
                GameManager.i.p1Click = false;
                audio.clip = GameManager.i.audioWrong;
                audio.Play();

                BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

                cameraBackgroundBlink.SetDefaultColors();
            }
            return;
        }

        


        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

        angle = angle + Time.fixedDeltaTime * angularSpeed;
        rb.MovePosition(new Vector2(posX, posY));
        if (angle >= 360f)
            angle = 0f;


        GameManager.i.P1Text.transform.position = transform.position;
        GameManager.i.P1Text.transform.rotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (touch) return;

        if (collision.CompareTag("ButtonP2"))
        {
            touch = true;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ButtonP2"))
        {
            touch = false;
            destroy = true;
            GameManager.i.missedPoint++;
            GameManager.i.P1Text.SetActive(false);


        }
    }
}
