using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    private int maxHealth = 100;
    public int currentHealth;
    public Image healthbar;
    public Text hpText;

    public BossAttackController bossAttackController;
    public BossMovementController bossMovementController;

    void Awake()
    {
        bossAttackController = GetComponent<BossAttackController>();
        bossMovementController = GetComponent<BossMovementController>();
        currentHealth = maxHealth;

        if (hpText)
        hpText.text = "Boss HP: " + currentHealth;
    }
    
    public void takeDamage(int damagePoint)
    {
        currentHealth -= damagePoint;
        if (healthbar)
            healthbar.fillAmount = (float)currentHealth / maxHealth;
        if (hpText)
            hpText.text = "Boss HP: " + currentHealth;

        if (currentHealth <= 0)
        {
            bossAttackController.gameInstanceManager.RecordData();

        }
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void ResetHealthAndAttacks()
    {
        //    bossAttackController = GetComponent<BossAttackController>();
        //    bossMovementController = GetComponent<BossMovementController>();
        currentHealth = maxHealth;
        if (healthbar)
            healthbar.fillAmount = (float)currentHealth / maxHealth;
        if (hpText)
            hpText.text = "Boss HP: " + currentHealth;
        bossAttackController.MyReset();
        bossMovementController.Stop();
    }
}
