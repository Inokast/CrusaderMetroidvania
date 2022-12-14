using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] GameObject cam;
    [SerializeField] private float parallaxIntensity;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxIntensity));
        float dist = (cam.transform.position.x * parallaxIntensity);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z); 

        if(temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
