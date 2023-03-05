using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : EnemyStats
{
    [SerializeField] List<Transform> targets = new List<Transform>();

    private Transform currentTarget;
    private int currentTargetID = -1;
    private EnemiesController enemiesController;
    private HealthBar healthBar;
    public void SetEnemyData(List<Transform> targetsList)
    {
        targets = targetsList;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemiesController = EnemiesController.instance;
        healthBar = HealthBar.instance;
        SetNewTarget();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentTargetID < targets.Count)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);

            if (Vector3.Distance(transform.position, currentTarget.position) < 0.3f)
            {
                SetNewTarget();
            }
        }
        else
        {
            enemiesController.CheckSurroundings();
            healthBar.UpdateLifeScore(damage);
            Destroy(gameObject);
            return;
        }
    }


    void SetNewTarget()
    {
        currentTargetID++;
        if (currentTargetID < targets.Count)
        {
            currentTarget = targets[currentTargetID];
        }
    }
}
