using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Transform target;
    public int damage = 50;
    public float speed = 10;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        Vector3 direction = target.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, (target.position - transform.position).normalized);
        // need to be fixed

        transform.Translate(direction * Time.deltaTime * speed);
    }

    // 子弹碰撞检测
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        // 如果攻击到的是敌人
        if (other.tag == "Enemy")
        {
            Debug.Log("hit");
            // 让敌人掉血
            other.GetComponent<Enemy>().ChangeHealth(damage);
            // 销毁子弹
            Destroy(this.gameObject);
            Debug.Log("bullet destroyed");
        }
    }

    void Die()
    {
       // GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
       // Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
