using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public GameObject goal;
    public GameObject[] goals;
    public int EnemyHP = 3;
    public HealthBarController HealthBar;
    [Header("死掉賺錢")]
    public GameObject EnconomicSystem;
    public int EnemyDeadMoney;
    [Header("變色")]
    public Material Original;
    public Material Hurt;
    public float switchTime;
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        #region 朝著水晶移動
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //goals = GameObject.FindGameObjectsWithTag("EnemysTarget");
        // 找到所有帶有 "EnemysTarget" 標籤的物件
        GameObject[] targets = GameObject.FindGameObjectsWithTag("EnemysTarget");

        // 對找到的每個目標進行處理
        foreach (GameObject target in targets)
        {
            agent.destination = target.transform.position;
        }
        #endregion
        HealthBar.SetMaxHealth(EnemyHP);
        EnconomicSystem = GameObject.Find("EnconomicSystem");
        objectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
        HealthBarUpdate();
    }


    #region 被攻擊扣血
    public void GetShooted(int i)
    {
        EnemyHP = EnemyHP - i;
        StartCoroutine(SwitchMaterial(switchTime, Hurt));
        StartCoroutine(SwitchBackMaterial(switchTime, Original));
    }

    private IEnumerator SwitchMaterial(float delay, Material targetMaterial)
    {
        objectRenderer.material = targetMaterial;

        yield return new WaitForSeconds(delay);
    }

    private IEnumerator SwitchBackMaterial(float delay, Material originalMaterial)
    {
        yield return new WaitForSeconds(delay);
        objectRenderer.material = originalMaterial;
    }
    #endregion
    #region 死亡
    void Dead()
    {
        ShopController Shop = EnconomicSystem.GetComponent<ShopController>();
        if (EnemyHP <= 0)
        {
            Shop.KilledEnemyPaid(EnemyDeadMoney);
            Destroy(gameObject);
        }
    }
    #endregion

    #region 更新生命血條
    void HealthBarUpdate()
    {
        HealthBar.SetHealth(EnemyHP);
    }
    #endregion
}
