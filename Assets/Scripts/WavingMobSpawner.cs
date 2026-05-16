using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WavingMobSpawner : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private WavingMob mobPrefab;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnTerm;
    [SerializeField] private float spawnStart;



    [Header("Mob Setting")]
    public float fluctuatingFrequency;
    public float fluctuatingAmplitude;
    public float fluctuatingStartAngle;
    public float fallSpeed;
    public Transform[] points;


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

        mobPrefab.fluctuatingFrequency = fluctuatingFrequency;
        mobPrefab.fluctuatingAmplitude = fluctuatingAmplitude;
        mobPrefab.fluctuatingStartAngle = fluctuatingStartAngle;
        mobPrefab.moveSpeed = fallSpeed;
        mobPrefab.points = points;
        mobPrefab.transform.localScale = Vector3.one * (((float)nowSpawnCount / spawnCount)  + 0.25f);
        WavingMob wavMob = Instantiate(mobPrefab);
        wavMob.transform.position = transform.position;
        lastSpawnTime = Time.time;
        nowSpawnCount--;
    }
}
