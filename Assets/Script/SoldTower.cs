using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldTower : MonoBehaviour
{
    public GameObject EnconomicSystem;

    [Header("bal")]
    public int TowerABal = 90;
    public int TowerBBal = 80;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ShopController Shop = EnconomicSystem.GetComponent<ShopController>();
        if (other.gameObject.tag == "TowerA")
        {
            print("Sold.");
            Shop.SoldTower(TowerABal);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "TowerB")
        {
            print("Sold.");
            Shop.SoldTower(TowerBBal);
            Destroy(other.gameObject);
        }
    }
}
