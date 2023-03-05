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

    [SerializeField] Text healthContainer;
    public int hpCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLifeScore();
    }

    public void UpdateLifeScore(int hpDamage = 0)
    {
        healthContainer.text = "";
        hpCount -= hpDamage;

        if (hpCount <= 0) GameOver();

        for (int i = 0; i < hpCount; i++)
        {
            healthContainer.text += "❤";
        }
    }

    public virtual void GameOver()
    {
        Menu.instance.ShowPanel(false);
        Time.timeScale = 0;
    }
}
