using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private float maxHp;
    public float hp = 150; 
    public Slider hpSlider; 
    public float speed = 1f;
    public int price = 50; 
    private Transform[] positions; 
    private int index = 0; 


    void Start() {
        maxHp = hp;
        positions = Waypoint.positions;
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    void move(){
        if (index > positions.Length - 1)
            return;
        //needed?

        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2)
            index++;

        if (index > positions.Length - 1)
        {
            // 当前敌人已经到达目的地
            GameManager.instance.playerHealth--;
            Destroy(this.gameObject);
            EnemySpawner.CountEnemyAlive--;
        }
    }

    public void ChangeHealth(float damage)
    {
        if (hp <= 0)
        {
            return;
        }
        // 血量减少，更新UI
        hp -= damage;
        hpSlider.value = hp / maxHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    // 敌人死亡
    void Die()
    {
        TowerManager.instance.ChangeMoney(price);
        Destroy(gameObject);
        EnemySpawner.CountEnemyAlive--;
    }
}
