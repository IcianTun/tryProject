using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    
    public Image healthbar;
    public Text hpText;

    public const int maxHealth = 100;
    public int currentHealth;

    [Header("GA things")]
    public GameInstanceManager gameInstanceMngr;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        if (hpText)
        hpText.text = currentHealth + "/" + maxHealth;
    }
    
    public void takeDamage(int damagePoint)
    {
        currentHealth -= damagePoint;
        if(hpText)
        hpText.text = currentHealth + "/" + maxHealth;
        if(healthbar)
        healthbar.fillAmount = (float)currentHealth / maxHealth;

        if(currentHealth <= 0)
        {
            gameInstanceMngr.RecordData();
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (healthbar)
            healthbar.fillAmount = (float)currentHealth / maxHealth;

    }
}
