using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    private int maxHealth = 1000;
    private int currentHealth;
    public Image healthbar;
    public Text hpText;

    // Use this for initialization
    void Start()
    {
        if (healthbar == null)
        {
            healthbar = GameObject.Find("BossGreen").GetComponent<Image>();
        }
        if (hpText == null)
        {
            hpText = GameObject.Find("BossHpText").GetComponent<Text>();
        }
        currentHealth = maxHealth;
        hpText.text = currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage(int damagePoint)
    {
        currentHealth -= damagePoint;
        hpText.text = currentHealth + "/" + maxHealth;
        healthbar.fillAmount = (float) currentHealth / maxHealth;
    }
}
