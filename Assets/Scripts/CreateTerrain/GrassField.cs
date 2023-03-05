using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassField : MonoBehaviour
{
    public int id;
    [SerializeField] public bool haveTower = false;
    [SerializeField] public bool hasUpgrade = false;
    [SerializeField] public GameObject showWhereCanBeTower;
    [SerializeField] public GameObject showUpgradeInfo;
    [SerializeField] public Transform parentTower;

    public void SetTower()
    {
        haveTower = true;
        GameObject tower = Instantiate(TowersManager.instance.prefabTower, new Vector3(parentTower.position.x, 0.92f, parentTower.position.z), Quaternion.identity);
        tower.transform.parent = parentTower;
        showWhereCanBeTower.SetActive(false);
    }

    public void UpgradeCurrentTower()
    {
        hasUpgrade = true;
        Transform tower = parentTower.GetChild(0);
        tower.GetComponent<Tower>().UpgradeTower();
        showUpgradeInfo.SetActive(false);
    }
}
