using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    #region Singleton
    public static Menu instance;
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
    [SerializeField] Text countEniemies;
    [SerializeField] GameObject gameRestartPanel;
    [SerializeField] Text titleText;

    private void Start()
    {
        countEniemies.text += EnemiesController.instance.enemyCountToSpawn.ToString();
    }

    public void ShowPanel(bool state)
    {
        gameRestartPanel.SetActive(true);

        if (state)
            titleText.text = "You Win!";
        else
            titleText.text = "Game Over!";
    }

    public void SetTower()
    {
        TowersManager.instance.ShowWhereCanBeTower();
    }

    public void UpgradeTower()
    {
        TowersManager.instance.ShowWhereUpgradeTower();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
