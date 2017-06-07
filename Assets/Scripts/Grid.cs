using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerData towerData;
    public bool isUpgradable = true;
    public TowerType towerType;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildTower(TowerData towerData) {
        this.towerData = towerData;
        tower = Instantiate(towerData.towerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        Debug.Log("new " + towerType + "tower");
    }

    public void UpgradeTower(TowerData towerData) {
        this.towerData = towerData;
        Destroy(tower);
        tower = Instantiate(towerData.towerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        Debug.Log(towerType + "tower level up to level" + towerData.level);
    }

    public void DestroyTower() {
        towerData = null;
        Destroy(tower);
        Debug.Log("tower Destroyed");
    }
}
