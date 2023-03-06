using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    #region Singleton
    public static EnemiesController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of EnemiesController found!");
            return;
        }
        instance = this;
    }
    #endregion

    public List<Transform> targetsPositions = new List<Transform>();
    public List<GameObject> enemiesList = new List<GameObject>();

    [SerializeField] GameObject prefabEnemy;
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject enemiesParent;

    public int enemyCountToSpawn = 10;
    public float timeStep = 1f; //time period beetween spawning enemies;

    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            GameObject enemy = Instantiate(prefabEnemy, spawnPosition.position, Quaternion.identity);
            enemy.transform.parent = enemiesParent.transform;
            enemy.GetComponent<EnemyProperties>().SetEnemyData(targetsPositions);
            enemy.transform.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            enemiesList.Add(enemy);
            yield return new WaitForSeconds(timeStep);
        }
    }

    public void CheckSurroundings()
    {
        for (int i = enemiesList.Count - 1; i >= 0; i--)
        {
            if (enemiesList[i] == null)
            {
                enemiesList[i] = enemiesList[enemiesList.Count - 1];
                enemiesList.RemoveAt(enemiesList.Count - 1);
            }
        }
    }

}
