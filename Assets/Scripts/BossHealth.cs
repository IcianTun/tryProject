using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public Image healthbar;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage(int damagePoint)
    {
        currentHealth -= damagePoint;
        healthbar.fillAmount = (float) currentHealth / maxHealth;
    }
}
