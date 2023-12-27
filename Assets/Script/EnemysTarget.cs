using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysTarget : MonoBehaviour
{
    public int EnemysTargetHP = 100;
    public int EnemyDamage = 10;
    public int DamageBetweenDamage = 3;
    public AudioClip CrystalHit;
    AudioSource audiosource;

    public HealthBarController HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetMaxHealth(EnemysTargetHP);
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarUpdate();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // 檢查碰到的物體是否是敵人
        if (collision.gameObject.tag == "Enemy")
        {
            // 造成傷害
            StartCoroutine(ApplyDamageOverTime(DamageBetweenDamage));
            //TakeDamage(EnemyDamage);
        }
    }

    #region 扣血
    private IEnumerator ApplyDamageOverTime(int s)
    {
        while (true)
        {
            // 每隔s秒扣一次血
            yield return new WaitForSeconds(s);
            TakeDamage(EnemyDamage);
        }
    }
    private void TakeDamage(int amount)
    {
        EnemysTargetHP -= amount;
        audiosource.PlayOneShot(CrystalHit);

        if (EnemysTargetHP <= 0)
        {
            Debug.Log("EnemysTarget: Failed!");
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    #endregion
    #region 水晶血量更新
    void HealthBarUpdate()
    {
        HealthBar.SetHealth(EnemysTargetHP);
    }
    #endregion
}
