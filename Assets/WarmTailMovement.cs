using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmTailMovement : MonoBehaviour
{
    [SerializeField] private float fluctuatingDegree;
    [SerializeField] private float fluctuatingFrequency;
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private int tailCount;
    [SerializeField] private float tailGap;

    private GameObject[] tailObjs;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        tailObjs = new GameObject[tailCount];
        for (int i = 0; i < tailCount; i++)
        {
            tailObjs[i] = Instantiate(tailPrefab,transform);
            tailObjs[i].transform.localScale = Vector3.one * ((float)(tailCount - i + 2) / (tailCount+2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tailCount; i++)
        {
            float r = (i + 1) * tailGap;
            float accumulateTime = Time.time - startTime;
            float theta = (fluctuatingDegree* Mathf.Deg2Rad) * Mathf.Sin(accumulateTime * fluctuatingFrequency + (((float)tailCount-i)/tailCount) * (Mathf.PI) + Mathf.PI*(3.0f/2.0f) );
            float x = transform.position.x + r * Mathf.Cos(theta);
            float y = transform.position.y + r * Mathf.Sin(theta);
            tailObjs[i].transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
