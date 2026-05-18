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

    [System.Serializable]
    public struct SineInfo
    {
        public float amplitude;
        public float frequency;
        public float startAngle;
    }

    public SineInfo[] moveSineArr;

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

        float y = initialPosition.y;
        float x = transform.position.x;
        float deltaY = 0.0f;
        float velocityY = 0.0f;

        if (moveSineArr != null)
            for (int i = 0; i < moveSineArr.Length; i++) 
            {
                float amplitude = moveSineArr[i].amplitude;
                float frequency = moveSineArr[i].frequency;
                float angle = accumulateTime * frequency + moveSineArr[i].startAngle * Mathf.Deg2Rad;

                deltaY += amplitude * Mathf.Sin(angle);
                velocityY += amplitude * Mathf.Cos(angle);
            }

        y += deltaY;


        float remainVelocitySquared = moveSpeed * moveSpeed - velocityY * velocityY;
        float xVelocity;
        if(remainVelocitySquared > 0.0f )
        {
            xVelocity = Mathf.Sqrt(remainVelocitySquared);
        }
        else
        {
            xVelocity = 0.0f;
        }
        x = transform.position.x + xVelocity * Time.deltaTime * dir * moveSpeed * 0.1f;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
