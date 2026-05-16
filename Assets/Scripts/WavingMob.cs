using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavingMob : MonoBehaviour
{
    public float fluctuatingFrequency;
    public float fluctuatingAmplitude;
    public float fluctuatingStartAngle;
    public float moveSpeed;

    private float accumulateTime = 0.0f;
    private Vector3 initialPosition;
    int dir; 

    public Transform[] points;
    private int nowIndex;


    void Start()
    {
        initialPosition = transform.position;
        nowIndex = 0;
        dir = -1;
    }

    // Update is called once per frame
    void Update()
    {
        float distX = points[nowIndex].position.x - transform.position.x;
        if(distX * dir < 0)
        {
            nowIndex = (nowIndex + 1) % points.Length;
            dir *= -1;
        }

        accumulateTime += Time.deltaTime;
        //float x = initialPosition.x - accumulateTime * moveSpeed;
        float x = transform.position.x + dir * moveSpeed * Time.deltaTime;
        float y = initialPosition.y + fluctuatingAmplitude * Mathf.Sin(accumulateTime * fluctuatingFrequency + fluctuatingStartAngle * Mathf.Deg2Rad); 
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
