using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int hp = 3;
    public float speed = 1;
    public int damage = 1;
    private Color previousColor;

    public void ChangeHP()
    {
        if (--hp <= 0)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            previousColor = gameObject.transform.GetComponent<MeshRenderer>().material.color;
            gameObject.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            StartCoroutine(ReturnPrevoiusColor());
        }
    }

    private IEnumerator ReturnPrevoiusColor()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetComponent<MeshRenderer>().material.color = previousColor;
    }
    
}
