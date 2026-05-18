using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static WavingMob;

public class WavingMobSpawner : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private WavingMob mobPrefab;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnTerm;
    [SerializeField] private float spawnStart;



    [Header("Mob Setting")]
    //public float fluctuatingFrequency;
    //public float fluctuatingAmplitude;
    //public float fluctuatingStartAngle;
    public float fallSpeed;
    public Transform[] points;

    [SerializeField] private SineInfo[] moveSineArr;
    public int SpawnCount
    {
        get => spawnCount;
    }

    private float startTime;
    private float lastSpawnTime;
    private int nowSpawnCount;

    // Start is called before the first frame update
    void Awake()
    {
        lastSpawnTime = startTime = Time.time;
        nowSpawnCount = spawnCount;
        //mobPrefab.moveSineArr = moveSineArr;
        //mobPrefab.moveSpeed = fallSpeed;
        //mobPrefab.points = points;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowSpawnCount == 0)
        {
            Destroy(this);
        }
        float timeFromStart = Time.time - startTime;
        float timeFromSpawn = Time.time - lastSpawnTime;

        if (timeFromStart < spawnStart) return;
        if (timeFromSpawn < spawnTerm) return;

        //mobPrefab.fluctuatingFrequency = fluctuatingFrequency;
        //mobPrefab.fluctuatingAmplitude = fluctuatingAmplitude;
        //mobPrefab.fluctuatingStartAngle = fluctuatingStartAngle;

        WavingMob wavMob = Instantiate(mobPrefab);
        wavMob.transform.position = transform.position;
        wavMob.transform.localScale = Vector3.one * (((float)nowSpawnCount / spawnCount) + 0.25f);
        wavMob.moveSineArr = moveSineArr;
        wavMob.moveSpeed = fallSpeed;
        wavMob.points = points;
        lastSpawnTime = Time.time;
        nowSpawnCount--;
    }
}
