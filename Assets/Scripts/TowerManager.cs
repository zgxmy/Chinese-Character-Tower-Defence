using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour {
    //public static TowerManager towerManager;
    public GameObject upgradeCanvas;
    public Button upgradeButton;
    public Text moneyText;

    public TowerData[] earthTowerData;
    public TowerData[] woodTowerData;
    public TowerData[] waterTowerData;
    public TowerData[] fireTowerData;
    private TowerData curTowerData;
    private Grid curGrid;
    private TowerType curType;
    public  Animator moneyAnimator;
    public static TowerManager instance;

    private int money = 1000;

    void Awake()
    {
        instance = this;
    }

    public void ChangeMoney(int data) {
        money += data;
        moneyText.text = "¥ " + money;
        Debug.Log("curMoney: " + money);
    }

    // Use this for initialization
    void Start () {
        upgradeCanvas.SetActive(false);
        moneyText.text = "¥ " + money;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero , Mathf.Infinity , LayerMask.GetMask("MapGrid"));
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.point, Color.red, 2);
            if (hit.collider != null)
            {
                Grid grid = hit.collider.gameObject.GetComponent<Grid>();
                if (curTowerData != null && grid.tower == null)
                {
                    upgradeCanvas.SetActive(false);
                    if (money >= curTowerData.cost)
                    {
                        ChangeMoney(-curTowerData.cost);
                        grid.BuildTower(curTowerData);
                        grid.towerType = curType;
                    }
                    else
                    {
                        // TODO钱不够，给一个提示
                        moneyAnimator.SetTrigger("Blink");
                    }
                }
                else if (grid.tower != null)
                {
                    if (curGrid == grid && upgradeCanvas.activeInHierarchy)
                    {
                        Debug.Log("double");
                        upgradeCanvas.SetActive(false);
                    }
                    else
                    {
                        ShowUpgradeUI(new Vector3(grid.transform.position.x, grid.transform.position.y, grid.transform.position.z - 5), !grid.isUpgradable);
                    }
                    curGrid = grid;
                }
            }
            else {
                upgradeCanvas.SetActive(false);
            }
            
        }
    }

    public void OnUpgradeButtonDown()
    {
        TowerData upgradeData;
        switch (curGrid.towerType)
        {
            case TowerType.earth:
                upgradeData = earthTowerData[curGrid.towerData.level];
                break;
            case TowerType.wood:
                upgradeData = woodTowerData[curGrid.towerData.level];
                break;
            case TowerType.water:
                upgradeData = waterTowerData[curGrid.towerData.level];
                break;
            case TowerType.fire:
                upgradeData = fireTowerData[curGrid.towerData.level];
                break;
            default:
                upgradeData = null;
                break;
        }
        if (money >= upgradeData.cost)
        { 
            ChangeMoney(-upgradeData.cost);
            curGrid.UpgradeTower(upgradeData);
            if (upgradeData.level == upgradeData.maxlevel) {
                curGrid.isUpgradable = false;
            }
            upgradeCanvas.SetActive(false);
        }
        else
        {
            // TODO钱不够，给一个提示
            moneyAnimator.SetTrigger("Blink");
        }
    }

    public void OnDestroyButtonDown(){
        TowerData upgradeData;
        switch (curGrid.towerType)
        {
            case TowerType.earth:
                upgradeData = earthTowerData[curGrid.towerData.level-1];
                break;
            case TowerType.wood:
                upgradeData = woodTowerData[curGrid.towerData.level-1];
                break;
            case TowerType.water:
                upgradeData = waterTowerData[curGrid.towerData.level-1];
                break;
            case TowerType.fire:
                upgradeData = fireTowerData[curGrid.towerData.level-1];
                break;
            default:
                upgradeData = null;
                break;
        }
        ChangeMoney(  (int) (upgradeData.cost * 0.5f));
        curGrid.DestroyTower();
        upgradeCanvas.SetActive(false);
    }

    public void OnEarthTowerSelected(){
        CursorManager.Instance.SetCursorEarth();
        curTowerData = earthTowerData[0];
        curType = TowerType.earth;
    }

    public void OnWoodTowerSelected(){
        CursorManager.Instance.SetCursorWood();
        curTowerData = woodTowerData[0];
        curType = TowerType.wood;
    }

    public void OnWaterTowerSelected(){
        CursorManager.Instance.SetCursorWater();
        curTowerData = waterTowerData[0];
        curType = TowerType.water;
    }

    public void OnFireTowerSelected(){
        CursorManager.Instance.SetCursorFire();
        curTowerData = fireTowerData[0];
        curType = TowerType.fire;
    }

    void ShowUpgradeUI(Vector3 position, bool isDisableUpgrade){
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = position;
        upgradeButton.interactable = !isDisableUpgrade;
    }
}
