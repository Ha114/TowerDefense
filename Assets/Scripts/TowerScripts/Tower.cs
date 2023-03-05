using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject magicMissle;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject spawnPosition;
    public Transform target;
    public float range = 1.5f;
    public float fireRate = 3f;

    List<GameObject> enemies = new List<GameObject>();
    private bool isRecharge = true;
    private GameObject typeOfSpell;
    private Vector3 center = Vector3.zero;

    private void Start()
    {
        typeOfSpell = magicMissle;
        center = new Vector3(transform.position.x, -0.01f, transform.position.z);
        StartCoroutine(UpdateTarget());
    }

    private IEnumerator UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject niearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(center, enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    niearestEnemy = enemy;
                }
            }
        }

        if (niearestEnemy != null && shortestDistance <= range)
            target = niearestEnemy.transform;
        else
            target = null;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdateTarget());
    }

    private void Update()
    {
        enemies = EnemiesController.instance.enemiesList;

        if (target == null)
            return;
        else if(isRecharge)
        {
            CastSpell();
        }
        
    }

    void CastSpell()
    {
        isRecharge = false;
        GameObject spell = Instantiate(typeOfSpell, spawnPosition.transform.position, Quaternion.identity);
        SpellBehavior spellBehavior = spell.GetComponent<SpellBehavior>();


        if(target != null && spell != null)
        {
           spellBehavior.ChaseTarget(target);

        }        
        StartCoroutine(Recharge());
    }


    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(fireRate);
        isRecharge = true;
    }

    public void UpgradeTower()
    {
        typeOfSpell = fireBall;
        transform.GetComponent<MeshRenderer>().material.color = Color.white;
        range = 2f;
        fireRate = 1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, range);
    }
}
