using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBBullet : MonoBehaviour
{
    Rigidbody rb;
    float lifeTime = 0;
    public float bulletForce = 50f;

    private int damage;
    private GameObject Tower;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletForce;
    }

    // Update is called once per frame
    void Update()
    {
        DestoryBullet();
        FindTower();
        UpdateDamage();
    }
    private void OnTriggerEnter(Collider collision)
    {
        // 檢查碰到的物體是否是敵人
        if (collision.gameObject.tag == "Enemy")
        {
            // 在這裡處理碰撞時對敵人的相應處理邏輯
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();

            if (enemy != null)
            {
                // 傳遞傷害值
                enemy.GetShooted(damage);
            }

            Destroy(gameObject); // 銷毀子彈
        }
    }

    void DestoryBullet()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > 3)
        {
            Destroy(gameObject);
        }
    }

    void FindTower()
    {
        GameObject[] l_Tower = GameObject.FindGameObjectsWithTag("TowerB");
        foreach (GameObject target in l_Tower)
        {
            Tower = target;
        }
    }

    void UpdateDamage()
    {
        damage = Tower.GetComponent<TowerAAttack>().BulletDamage;
    }
}
