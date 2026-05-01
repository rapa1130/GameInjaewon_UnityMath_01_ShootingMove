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
    [SerializeField] private float recoverFrequency;

    private GameObject[] tailObjs;
    private float[] localScales;
    private float startTime;
    private float timeAfterRecover;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        tailObjs = new GameObject[tailCount];
        localScales = new float[tailCount];
        timeAfterRecover = 0;
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
        int i;
        for (i = 0; i < tailCount; i++)
        {
            if (tailObjs[i] == null)
            {
                int j = i;
                while(++j < tailCount)
                {
                    if (tailObjs[j] != null)
                    {
                        Destroy(tailObjs[j].gameObject);
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

        if(i != tailCount)
        {
            timeAfterRecover += Time.deltaTime;
            if(timeAfterRecover > recoverFrequency)
            {
                Debug.Log("Recover!");
                tailObjs[i] = Instantiate(tailPrefab, transform);
                tailObjs[i].transform.localScale = Vector3.one * ((float)(tailCount - i + 2) / (tailCount + 2));
                timeAfterRecover = 0;
            }
        }

    }
}
