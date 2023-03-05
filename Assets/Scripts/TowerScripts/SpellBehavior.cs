using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehavior : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public void ChaseTarget(Transform currentTarget) => target = currentTarget;


    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            target.GetComponent<EnemyStats>().ChangeHP();
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir * distanceThisFrame, Space.World);
    }

}
