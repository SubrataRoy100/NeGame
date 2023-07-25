using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private HealthSystem healthSystem;
    [SerializeField] private HealthBar_UI healthBar_UI;
    public int currentHealth;

    private PlaneController planeController;
    void Awake()
    {
        planeController = GetComponent<PlaneController>(); // this is plane controller 
        healthSystem = new HealthSystem(planeController.GetPlayerDataSO().CurrentHealtMax); // SET MAXHEALTH FROM PLAYERDATA
        healthBar_UI.SetUp(healthSystem);
        healthSystem.OnDie += HealthSystem_OnDie;
        healthSystem.OnHealthChange += healthSystem_OnHealthChange;
        if (PlayerPrefs.HasKey("Health"))
        {
            int h = PlayerPrefs.GetInt("Health");
            healthSystem.SetHealth(h);
            Debug.Log(h);
        };
    }
    private void Start()
    {

    }
    private void healthSystem_OnHealthChange(object sender, EventArgs e)
    {
    }

    private void HealthSystem_OnDie(object sender, EventArgs e) // ON DEAD EVENT ARGS
    {
        Destroy(gameObject);

    }

    public HealthSystem GetHealthSystem() => healthSystem; // Geting Health System
    public int GetCurrentHealth()
    {
        return healthSystem.GetCurrentHealth();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Health", healthSystem.GetCurrentHealth());
        Debug.Log(healthSystem.GetCurrentHealth());
    }

}


