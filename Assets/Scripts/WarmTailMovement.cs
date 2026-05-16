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

    [SerializeField] private float gulpingTermPerElements = 0.2f;
    [SerializeField] private float gulpingFrequency = 8;
    [SerializeField] private float gulpingAmplitude = 0.15f;



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
    void RecoverTail(int index)
    {
        if (index != tailCount)
        {
            timeAfterRecover += Time.deltaTime;
            if (timeAfterRecover > recoverFrequency)
            {
                tailObjs[index] = Instantiate(tailPrefab, transform);
                tailObjs[index].transform.localScale = Vector3.one * ((float)(tailCount - index + 2) / (tailCount + 2));
                timeAfterRecover = 0;
            }
        }
    }

    void WaveTail()
    {
        float nowR = 0;

        for (int i = 0; i < tailCount; i++)
        {
            if (tailObjs[i] == null) return;
            nowR += localScales[i];
            float accumulateTime = Time.time - startTime;
            float theta = (fluctuatingDegree * Mathf.Deg2Rad) * Mathf.Sin(accumulateTime * fluctuatingFrequency + (((float)tailCount - i) / tailCount) * (Mathf.PI));
            float x = transform.position.x + nowR * Mathf.Cos(theta + fluctuatingStartAngle * Mathf.Deg2Rad);
            float y = transform.position.y + nowR * Mathf.Sin(theta + fluctuatingStartAngle * Mathf.Deg2Rad);
            tailObjs[i].transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    int CheckAndRemoveCuttedTail()
    {
        int i;
        for (i = 0; i < tailCount; i++) 
        {
            if (tailObjs[i] == null)
            {
                int j = i;
                while (++j < tailCount)
                {
                    if (tailObjs[j] != null)
                    {
                        Destroy(tailObjs[j].gameObject);
                    }
                }
                break;
            }
        }
        return i;
    }
    void GulpingTail()
    {
        for (int i = 0; i < tailCount; i++)
        {
            if (tailObjs[i] == null) return;
            float accumulateTime = Time.time - startTime;
            float theta = (-accumulateTime +gulpingTermPerElements*i) * gulpingFrequency; 
            tailObjs[i].transform.localScale = Vector3.one * (localScales[i] + localScales[i]* gulpingAmplitude* Mathf.Sin(theta));
        }
    }

    void Update()
    {
        int last = CheckAndRemoveCuttedTail();
        RecoverTail(last);
        WaveTail();
        GulpingTail();
    }
}
