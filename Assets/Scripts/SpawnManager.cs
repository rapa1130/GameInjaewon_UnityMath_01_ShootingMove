using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    static private SpawnManager instance = null;
    static public SpawnManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private WavingMobSpawner[] spanwer;
    private int enemyCount;
    string nowScene;
    public void DecreaseEnemyCnt()
    {
        --enemyCount;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        enemyCount = 0;
        for (int i = 0; i < spanwer.Length; i++)
        {
            if (spanwer[i] != null)
            {
                enemyCount += spanwer[i].SpawnCount;
            }
        }
    }
    private void Update()
    {
        if(enemyCount == 0)
        {
            StartCoroutine(DelayTime());
        }
    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("2_WarmEnemy");
    }

}
