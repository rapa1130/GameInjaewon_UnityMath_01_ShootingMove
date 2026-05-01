using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmTailMovement : MonoBehaviour
{
    [SerializeField] private float fluctuatingDegree;
    [SerializeField] private float fluctuatingFrequency;
    [SerializeField] private float fluctuatingStartAngle;
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private int tailCount;
    [SerializeField] private float tailGap;

    private GameObject[] tailObjs;
    private float[] localScales;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        tailObjs = new GameObject[tailCount];
        localScales = new float[tailCount];
        for (int i = 0; i < tailCount; i++)
        {
            tailObjs[i] = Instantiate(tailPrefab,transform);
            tailObjs[i].transform.localScale = Vector3.one * ((float)(tailCount - i + 2) / (tailCount+2));
            localScales[i] = tailObjs[i].transform.localScale.x;
        }
    }
    void Update()
    {
        float nowR = 0;
        for (int i = 0; i < tailCount; i++)
        {
            if (tailObjs[i] == null)
            {
                while(++i < tailCount)
                {
                    if (tailObjs[i] != null)
                    {
                        Destroy(tailObjs[i].gameObject);
                    }
                }
                break;
            }
            nowR += localScales[i];
            float accumulateTime = Time.time - startTime;
            float theta = (fluctuatingDegree * Mathf.Deg2Rad) * Mathf.Sin(accumulateTime * fluctuatingFrequency + (((float)tailCount - i) / tailCount) * (Mathf.PI));
            float x = transform.position.x + nowR * Mathf.Cos(theta + fluctuatingStartAngle * Mathf.Deg2Rad);
            float y = transform.position.y + nowR * Mathf.Sin(theta + fluctuatingStartAngle * Mathf.Deg2Rad);
            tailObjs[i].transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
