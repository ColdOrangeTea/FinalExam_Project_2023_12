using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region 宣告
    [Header("乘載程式的物件")]
    public GameObject Crystal;
    public GameObject EnemySpawnSystem;
    [Header("UI")]
    public GameObject GameOverUI;
    public AudioClip audio_GameOver;
    public GameObject GameSuccessedUI;
    public AudioClip audio_GameSuccessed;
    private bool hasAudioPlayed;
    [Header("判斷")]
    public bool isGameOver = false;

    AudioSource audiosource;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        hasAudioPlayed = false;
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCrystalHP();
        GameOverCheck();
        GameSuccessedCheck();
    }

    #region 更新水晶生命值並判斷遊戲失敗
    void CheckCrystalHP()
    {
        EnemysTarget s_Crystal = Crystal.GetComponent<EnemysTarget>();
        int s_CrystalHP = s_Crystal.EnemysTargetHP;

        if (s_CrystalHP > 0)
        {
            isGameOver = false;
        }
        else
        {
            //print("Gameover");
            isGameOver = true;
        }
    }
    #endregion
    #region 遊戲失敗
    void GameOverCheck()
    {
        if (isGameOver && hasAudioPlayed == false)
        {
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
            audiosource.PlayOneShot(audio_GameOver);
            hasAudioPlayed = true;
            print("Played");
        }
    }
    #endregion
    #region 遊戲成功
    void GameSuccessedCheck()
    {
        EnemySpawn s_Enemy = EnemySpawnSystem.GetComponent<EnemySpawn>();
        bool s_EnemyisWin = s_Enemy.isWin;

        if (s_EnemyisWin && hasAudioPlayed == false)
        {
            GameSuccessedUI.SetActive(true);
            audiosource.PlayOneShot(audio_GameSuccessed);
            hasAudioPlayed = true;
        }

    }
    #endregion

}
