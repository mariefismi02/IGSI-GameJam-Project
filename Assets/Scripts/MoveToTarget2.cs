using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget2 : MonoBehaviour
{
    public Sprite sprite;

    Transform rotationCenter;

    private Rigidbody2D rb;

    [SerializeField]
    float rotationRadius = 2f;

    float posX, posY, angle = 0f;
    AudioSource audio;

    public AudioSource Audio
    {
        get { return audio; }
    }

    public float angularSpeed = 2f;

    float scale = 0;
    public bool destroy = false;
    public bool touch = false;
    public int colorIndex = 0;

    private void Awake()
    {
        rotationCenter = GameObject.FindGameObjectWithTag("RotationPoint").transform;
        rb = GetComponent<Rigidbody2D>();
        Vector2 p1 = rotationCenter.position;
        Vector2 p2 = transform.position;
        angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;
        audio = GetComponent<AudioSource>();

    }

    private void Start()
    {
        GameManager.i.p2Click = false;
        GameManager.i.P2Text.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(sprite!=null)
            GetComponent<SpriteRenderer>().sprite = sprite;

        transform.localScale = Vector3.one * scale;

        scale = Mathf.Lerp(scale, destroy ? 0 : 1, .25f);
        
    }

    private void FixedUpdate()
    {
        if (GameManager.i.p2Click)
        {
            return;
        }

        if (destroy)
        {
            if (scale <= 0.1f)
            {
                GameManager.ShowFloatingText("MISS", transform.position, transform.parent, Color.white);
                Destroy(gameObject);
                GameManager.i.p2Click = false;
                audio.clip = GameManager.i.audioWrong;
                audio.Play();

                BackgroundColorBlink cameraBackgroundBlink = Camera.main.GetComponent<BackgroundColorBlink>();

                cameraBackgroundBlink.SetDefaultColors();
            }
            return;
        }

        

        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

        angle = angle - Time.fixedDeltaTime * angularSpeed;
        rb.MovePosition(new Vector2(posX, posY));
        if (angle >= 360f)
            angle = 0f;

        GameManager.i.P2Text.transform.rotation = transform.rotation;
        GameManager.i.P2Text.transform.position = transform.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (touch) return;

        if (collision.CompareTag("ButtonP1"))
        {
            touch = true;
        }

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ButtonP1"))
        {
            touch = false;
            destroy = true;
            GameManager.i.missedPoint++;
            GameManager.i.P2Text.SetActive(false);

        }
    }
}
