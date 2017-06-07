using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    //private List<GameObject> enemies = new List<GameObject>();
    //public TowerType type;
    //public TowerData towerData;

    private List<GameObject> enemies = new List<GameObject>();
    public float attackRateTime = 1.0f; // 攻击间隔
    private float timer = 0; // 计时器
    public GameObject bulletPrefab; // 子弹预制体

    // Use this for initialization
    void Start()
    {
        // 炮塔刚实例化就能开始攻击，让 timer >= attackRateTime 成立  maybe need a cor?
        timer = attackRateTime;
    }

    void Update()
    {
        if (enemies.Count > 0 && enemies[0] != null)//?
        {
            Vector3 targetPosition = enemies[0].transform.position;
            targetPosition.y = this.gameObject.transform.position.y;
        }

        timer += Time.deltaTime;
        // 有存在的地方，并且计时器大于攻击间隔就重置，并调用攻击方法
        if (enemies.Count > 0 && timer >= attackRateTime)
        {
            // 定时器清空
            timer = 0;
            Attack();
        }
    }
    void Attack()
    {
        //Debug.Log("attack!");
        // 如果目标已经被杀死或者已经到达终点，则移除集合
        if (enemies[0] == null)
        {
            UpdateEnemys();
        }

        // 清理了空元素后，再次判断是否还有敌人能够被攻击
        if (enemies.Count > 0)
        {
            // 实例化子弹，子弹位置和方向于炮塔枪口一致
            GameObject bullet = GameObject.Instantiate(bulletPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y,-2), this.gameObject.transform.rotation);
            // 给子弹设置攻击目标
            bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
        }
        else
        {
            // 可以攻击状态
            timer = attackRateTime;
        }

    }

    void UpdateEnemys()
    {
        // 存储所有为空的元素
        List<int> emptyIndexList = new List<int>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                emptyIndexList.Add(i);
            }
        }
        // 移除空元素
        for (int i = 0; i < emptyIndexList.Count; i++)
        {
            enemies.RemoveAt(emptyIndexList[i] - i);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("add enemy");
            enemies.Add(other.gameObject);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }
}
