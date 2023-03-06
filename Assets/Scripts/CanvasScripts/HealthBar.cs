using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Singleton
    public static HealthBar instance;
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

    [SerializeField] List<GameObject> hearts = new List<GameObject>();
    private int hpCount = 3;
    void Start()
    {
        UpdateLifeScore();
    }

    public void UpdateLifeScore(int hpDamage = 0)
    {
        foreach (var heart in hearts)
            heart.SetActive(false);

        hpCount -= hpDamage;

        if (hpCount <= 0) GameOver();

        for (int i = 0; i < hpCount; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public virtual void GameOver()
    {
        Menu.instance.ShowPanel(false);
        Time.timeScale = 0;
    }
}
