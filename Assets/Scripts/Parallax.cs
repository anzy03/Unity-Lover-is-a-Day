using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Parallax : MonoBehaviour
{

    float length, startPos;
    public float parallex;
    public GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (Cam.transform.position.x * (1 - parallex));
        float dist = (Cam.transform.position.x * parallex);

        transform.position = new Vector2(startPos + dist, transform.position.y);

        if(temp > startPos + length)
        {
            startPos += length;
        }
        else if(temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
