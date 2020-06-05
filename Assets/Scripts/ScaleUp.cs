using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    [SerializeField] float fullScale = 1;
    float scale = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scale = EasingFunction.EaseInOutBounce(scale, 1, .25f);
        
        transform.localScale = Vector3.one * fullScale * scale;
    }

    public void ReScale()
    {
        scale = 0;
    }
}
