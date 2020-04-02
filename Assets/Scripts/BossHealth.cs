using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    private int maxHealth = 100;
    private int currentHealth;
    public Image healthbar;
    public Text hpText;

    // Use this for initialization
    void Start()
    {
        //if (healthbar == null)
        //{
        //    healthbar = GameObject.Find("BossGreen").GetComponent<Image>();
        //}
        //if (hpText == null)
        //{
        //    hpText = GameObject.Find("BossHpText").GetComponent<Text>();
        //}
        currentHealth = maxHealth;
        if (hpText)
        hpText.text = currentHealth + "/" + maxHealth;
    }
    
    public void takeDamage(int damagePoint)
    {
        currentHealth -= damagePoint;
        if (healthbar)
            healthbar.fillAmount = (float)currentHealth / maxHealth;
        if (hpText)
            hpText.text = currentHealth + "/" + maxHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void resetHealth()
    {
        currentHealth = maxHealth;
    }
}
