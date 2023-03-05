using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    #region Singleton
    public static TowersManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of HealthBar found!");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] GameObject grassParent;

    [SerializeField] List<Transform> whereCanBeTower = new List<Transform>();
    [SerializeField] List<Transform> whereCanUpgradeTower = new List<Transform>();
    [SerializeField] List<GameObject> grassFieldsList = new List<GameObject>();

    public GameObject prefabTower;


    private void Start()
    {
        GetChilds(grassParent, grassFieldsList);
        GetWhereChageTower();
    }

    public void ShowWhereUpgradeTower()
    {
        GetWhereChageTower();
        for (int i = 0; i < whereCanUpgradeTower.Count; i++)
        {
            GameObject go = whereCanUpgradeTower[i].gameObject;

            if (go.activeSelf)
                go.SetActive(false);
            else
            {
                if (grassFieldsList[i].GetComponent<GrassField>().haveTower && !grassFieldsList[i].GetComponent<GrassField>().hasUpgrade)
                {
                    go.SetActive(true);
                }
            }
        }
    }
    public void ShowWhereCanBeTower()
    {
        GetWhereChageTower();
        for (int i = 0; i < whereCanBeTower.Count; i++)
        {
            GameObject go = whereCanBeTower[i].gameObject;

            if (go.activeSelf)
                go.SetActive(false);
            else
            {
                if (!grassFieldsList[i].GetComponent<GrassField>().haveTower)
                    go.SetActive(true);
            }
        }
    }
    private void GetChilds(GameObject obj, List<GameObject> list)
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;

            list.Add(child.gameObject);
        }
    }
    private void GetWhereChageTower()
    {
        whereCanBeTower.Clear();
        whereCanUpgradeTower.Clear();
        foreach (GameObject gameObject in grassFieldsList)
        {
            whereCanBeTower.Add(gameObject.transform.GetChild(1));
            whereCanUpgradeTower.Add(gameObject.transform.GetChild(2));
        }
    }
}
