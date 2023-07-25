using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthBar_UI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverText;
    private Transform healthText;
    private TextMeshProUGUI text;
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthText = transform.Find("Text").transform;
        text = healthText.GetComponent<TextMeshProUGUI>();
        Hide();
    }

    private void Start()
    {
        healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
        healthSystem.OnDie += HealthSystem_OnDie;
        ApplyHeathChange();
    }

    private void HealthSystem_OnDie(object sender, EventArgs e)
    {
        Show();
    }

    private void HealthSystem_OnHealthChange(object sender, EventArgs e)
    {
        ApplyHeathChange();
    }


    private void ApplyHeathChange()
    {
        text.text = healthSystem.GetCurrentHealth().ToString();
        
    }
    public void SetUp(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
    }


    private void Hide()
    {
        Time.timeScale = 1;
        gameOverText.SetActive(false);
    }
    private void Show()
    {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
    }

    

    
}
