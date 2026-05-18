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
    [SerializeField] private int killTailCount = 3;
    [SerializeField] private float DeathBurstSpeed;

    private Vector3 center;
    private float startTime;
    private float accumulateTime;
    private int initialTailCount;

    private  Vector3[] burstDir;
    void Start()
    {
        startTime = Time.time;
        center = transform.position;
        initialTailCount = transform.childCount;
        burstDir = new Vector3[initialTailCount];
        for (int i = 0; i < initialTailCount; i++)
        {
            burstDir[i] = Random.insideUnitCircle;
            burstDir[i].z = 0;
        }
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(2.0f);

    }
    // Update is called once per frame

    
    void Update()
    {
        if (transform.childCount <= initialTailCount - killTailCount)
        {
            StartCoroutine(DelayedDestroy());

            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 burstVec = burstDir[i] * DeathBurstSpeed * Time.deltaTime;
                transform.GetChild(i).position += burstVec;
            }
            return;
        }
        float passingTime = Time.time - startTime;
        float x = center.x + xFluctuatingAmplitude * Mathf.Sin(passingTime *xFluctuatingFrequency);
        float y = center.y + yFluctuatingAmplitude * Mathf.Sin(passingTime *yFluctuatingFrequency);
        transform.position = new Vector3(x, y, transform.position.z);

    }
}
