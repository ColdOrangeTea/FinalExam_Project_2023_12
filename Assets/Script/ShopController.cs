using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    #region 宣告
    [Header("MoneyTMP")]
    public int PlayersMoney = 100;
    public TMP_Text ShowMoney;
    public TextMeshProUGUI NoMoney;

    [Header("販售塔價錢")]
    public int TowerAPrice = 80;
    public int TowerBPrice = 40;

    [Header("生成塔")]
    public GameObject TowerSpawnPoint;
    public GameObject TowerAPrefab;
    public GameObject TowerBPrefab;

    [Header("購物音效")]
    public AudioClip BuyTower;
    AudioSource audiosource;
    #endregion 

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        NoMoney.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMonryShow();
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(ShowAndHideUI());
        }
    }

    #region 更新在UI上顯示的金錢
    void UpdateMonryShow()
    {
        ShowMoney.text = PlayersMoney.ToString();
    }
    #endregion
    #region 買A塔
    public void BuyTowerA()
    {
        if(PlayersMoney >= TowerAPrice)
        {
            PlayersMoney = PlayersMoney - TowerAPrice;
            audiosource.PlayOneShot(BuyTower);
            SpawnTowerA();
        }
        else
        {
            //print("no money");
            StartCoroutine(ShowAndHideUI());
        }
    }

    void SpawnTowerA()
    {
        Instantiate(TowerAPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    }
    #endregion
    #region 買B塔
    public void BuyTowerB()
    {
        if (PlayersMoney >= TowerBPrice)
        {
            PlayersMoney = PlayersMoney - TowerBPrice;
            SpawnTowerB();
            audiosource.PlayOneShot(BuyTower);
        }
        else
        {
            StartCoroutine(ShowAndHideUI());
        }
    }
    void SpawnTowerB()
    {
        Instantiate(TowerBPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    }
    #endregion
    #region 生成在隨機位置
    private Vector3 GetRandomSpawnPosition()
    {
        // 在 spawnPoint 的附近隨機生成位置
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        return TowerSpawnPoint.transform.position + randomOffset;
    }
    #endregion
    #region 賣塔
    public void SoldTower(int bal)
    {
        //print("get money");
        PlayersMoney = PlayersMoney + bal;
    }
    #endregion
    #region 殺掉敵人後取得金錢
    public void KilledEnemyPaid(int bal)
    {
        PlayersMoney = PlayersMoney + bal;
    }
    #endregion
    #region 在顯示UI三秒後隱藏
    IEnumerator ShowAndHideUI()
    {
        NoMoney.alpha = 1f;
        // 顯示UI
        NoMoney.gameObject.SetActive(true);

        while (NoMoney.alpha > 0f)
        {
            NoMoney.alpha -= Time.deltaTime / 3f;  // 在三秒内逐渐减小透明度
            yield return null;
        }

        // 等待三秒
        yield return new WaitForSeconds(3f);

        // 隱藏UI
        NoMoney.gameObject.SetActive(false);
        
    }
    #endregion
}
