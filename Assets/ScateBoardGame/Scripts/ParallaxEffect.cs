using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    float length, startPos;
    Transform camTrans;
    public float parallaxEffect;
    public float offset;

    private void Start()
    {
        camTrans = Camera.main.transform;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Update()
    {
        float tmp = camTrans.position.x * (1 - parallaxEffect);
        float dist = camTrans.position.x * parallaxEffect;

        Vector3 pos = transform.position;
        pos.x = startPos + dist;
        transform.position = pos;

        if (tmp > startPos + (length - offset)) startPos += length;
        else if (tmp < startPos - (length - offset)) startPos -= length;
    }
}
