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


    private float startTime;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount == 0)
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
        mobPrefab.fallSpeed = fallSpeed;
        Instantiate(mobPrefab);
        lastSpawnTime = Time.time;
        spawnCount--;
    }
}
