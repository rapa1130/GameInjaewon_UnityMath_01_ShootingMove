using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavingMob : MonoBehaviour
{
    public float fluctuatingFrequency;
    public float fluctuatingAmplitude;
    public float fluctuatingStartAngle;
    public float fallSpeed;

    private float accumulateTime = 0.0f;
    private Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        accumulateTime += Time.deltaTime;
        float x = initialPosition.x - accumulateTime * fallSpeed;
        float y = initialPosition.y + fluctuatingAmplitude * Mathf.Sin(accumulateTime * fluctuatingFrequency + fluctuatingStartAngle * Mathf.Deg2Rad); 
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
