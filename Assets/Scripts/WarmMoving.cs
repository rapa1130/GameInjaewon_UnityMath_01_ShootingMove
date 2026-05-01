using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmMoving : MonoBehaviour
{
    [SerializeField] private float intialEnterSpeed;
    [SerializeField] private float enterSpeedDamping;
    [SerializeField] private float xFluctuatingAmplitude;
    [SerializeField] private float yFluctuatingAmplitude;
    [SerializeField] private float xFluctuatingFrequency;
    [SerializeField] private float yFluctuatingFrequency;

    private Vector3 center;
    private float startTime;
    private float accumulateTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float passingTime = Time.time - startTime;
        float x = center.x + xFluctuatingAmplitude * Mathf.Sin(passingTime *xFluctuatingFrequency);
        float y = center.y + yFluctuatingAmplitude * Mathf.Sin(passingTime *yFluctuatingFrequency);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
