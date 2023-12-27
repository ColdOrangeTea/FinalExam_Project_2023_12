using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    #region 宣告
    [Header("敵人生成點")]
    public Transform spawnPoint;
    [Header("敵人生成音效")]
    public AudioClip EnemyApear;
    AudioSource audiosource;
    [Header("敵人每波數量")]
    public int numberOfEnemiesPerWave = 5;
    public int totalWaves = 3;
    public TMP_Text WaveCount;
    [Header("生成等待時間")]
    public float timeBetweenEnemies = 1f; // 每個敵人之間的等待時間
    public float timeBetweenWaves = 3f; // 每波之間的等待時間
    [Header("現在波數")]
    public int currentWave = 1;
    public List<GameObject> enemies;
    [Header("勝利判斷")]
    public bool isWin = false;
    #endregion

    void Start()
    {
        isWin = false;
        audiosource = GetComponent<AudioSource>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyWaveShowed();
        WinCheck();
    }

    #region 每波生成敵人
    private IEnumerator SpawnEnemies()
    {
        print("go in spawnenemies");
        while (enemies.Count > 0)
        {
            currentWave++;
            print("Wave:" + currentWave);

            // 生成每波敵人
            for (int i = 0; i < numberOfEnemiesPerWave; i++)
            {
                if (enemies.Count > 0)
                {
                    // 生成敵人
                    Instantiate(enemies[0], spawnPoint.position, Quaternion.identity);
                    audiosource.PlayOneShot(EnemyApear);
                    enemies.RemoveAt(0);

                    // 暫停一定時間
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            // 暫停五秒再生成下一波
            if (currentWave < totalWaves)
            {
                Debug.Log("Pause between waves");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }
    #endregion
    #region 指定位置附近隨機位置生成
    private Vector3 GetRandomSpawnPosition()
    {
        // 在 spawnPoint 的附近隨機生成位置
        Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        return spawnPoint.position + randomOffset;
    }
    #endregion
    #region TMP上顯示第幾波
    void UpdateEnemyWaveShowed()
    {
        WaveCount.text = currentWave.ToString() + "/" + totalWaves.ToString();
    }
    #endregion
    #region 勝利判定
    void WinCheck()
    {
        if (enemies.Count == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            isWin = true;
        }
        else
        {
            isWin = false;
        }
    }
    #endregion
}
