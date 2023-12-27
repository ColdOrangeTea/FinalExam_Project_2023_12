using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAAttack : MonoBehaviour
{
    #region 宣告
    [Header("子彈預製物和開火點")]
    public GameObject bulletPrefab;
    public GameObject firePoint;
    public AudioClip fireSound;
    public int BulletDamage;
    [Header("射擊速度")]
    public float shootingInterval = 0.5f; // 射擊間隔（秒）
    [Header("敵人的標籤")]
    public string enemyTag = "Enemy";

    [Header("偵測範圍")]
    public float detectionRadius = 10f;
    private Transform currentTarget;

    [Header("敵人列表")]
    public GameObject[] enemys;

    AudioSource audiosource;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine());
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    #region 啟用程式與否
    public void EnableScript()
    {
        // 啟用你的腳本
        enabled = true;
    }
    public void UnenableScript()
    {
        // 關閉你的腳本
        enabled = false;
    }
    #endregion
    #region 自動射擊
    //射擊
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            // 檢測範圍內的所有敵人
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

            // 找到離攻擊者最近的敵人
            Transform closestEnemy = GetClosestEnemy(colliders);

            // 更新當前目標
            currentTarget = closestEnemy;

            // 如果找到敵人，執行射擊
            if (currentTarget != null)
            {
                Shoot(currentTarget);
                audiosource.PlayOneShot(fireSound);
            }

            yield return new WaitForSeconds(shootingInterval);
        }
    }
    //鎖定敵人
    private Transform GetClosestEnemy(Collider[] colliders)
    {
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                // 如果這個敵人更近，更新最近的敵人和距離
                if (distance < closestDistance)
                {
                    closestEnemy = collider.transform;
                    closestDistance = distance;
                }
            }
        }

        return closestEnemy;
    }
    //射擊方向
    private void Shoot(Transform target)
    {
        // 在這裡實例化子彈，設定位置和方向等
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 設定子彈的方向
        Vector3 shootDirection = (target.position - transform.position).normalized;
        bullet.transform.forward = shootDirection;
    }
    #endregion

}
