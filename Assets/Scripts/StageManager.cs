using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    static private StageManager instance = null;
    static public StageManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private WavingMobSpawner[] spanwer;
    private int enemyCount;
    
    public void DecreaseEnemyCnt()
    {
        --enemyCount;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        enemyCount = 0;
        for (int i = 0; i < spanwer.Length; i++)
        {
            enemyCount += spanwer[i].SpawnCount;
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
