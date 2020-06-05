using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject coffin;
    [SerializeField] GameObject hospital;
    [SerializeField] AudioSource audio;

    public float scrollSpeed;

    [SerializeField] GameObject transitionImage;
    [SerializeField] GameObject gameMain;
    [SerializeField] Animator coffinAnimator;

    public bool started = false;
    //bool preparing = true;
    float transitionScale = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartMove", 2);
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (!audio.isPlaying)
        {
            if (!gameMain.activeSelf)
            {
                gameMain.SetActive(true);
                gameMain.GetComponent<AudioSource>().Play();
            }

            transitionScale = Mathf.Lerp(transitionScale, 1, .15f);

            transitionImage.transform.localScale = Vector3.one * 8 * transitionScale;

            if (transitionScale > 0.9f)
            {
                gameMain.GetComponent<GameMain>().StartGame();
                gameObject.SetActive(false);
            }

            return;
        }

        if (started)
        {
            coffinAnimator.SetBool("isWalk", true);
            if (coffin.transform.position.x < 0)
            {
                coffin.transform.Translate(Vector2.right * scrollSpeed/5 * Time.deltaTime);
            }
            
            hospital.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        } /*else
        {
            if (!preparing)
            {
                if (coffin.transform.position.x < 0)
                {
                    coffin.transform.Translate(Vector2.right  * scrollSpeed * Time.deltaTime);
                } else
                {
                    started = true;
                }
            }
        }*/
    }

    void StartMove()
    {
        started = true;
    }

   
}
