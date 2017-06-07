using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerHealth;
    public GameObject endUI; // 结束UI
    public Text endMessage; // 游戏结束提示信息
    public static GameManager instance;
    private EnemySpawner enemySpawner; // 敌人孵化器

    void Awake()
    {
        instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    void Update()
    {
        if (playerHealth == 0) {
            Failed();
        }
    }

    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "胜 利";
        CursorManager.Instance.SetCursorNormal();
    }

    public void Failed()
    {
        endUI.SetActive(true);
        endMessage.text = "失 败";
        enemySpawner.Stop();
        CursorManager.Instance.SetCursorNormal();
    }

    public void OnButtonRetryDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnButtonMenuDown()
    {
        SceneManager.LoadScene("menu");
    }

}