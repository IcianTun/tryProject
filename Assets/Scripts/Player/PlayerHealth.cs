using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    
    public Image healthbar;
    public Text hpText;

    public int maxHealth = 100;
    public int currentHealth;
    public PlayerAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<PlayerAgent>();
        currentHealth = maxHealth;
        if (hpText)
        hpText.text = currentHealth + "/" + maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void takeDamage(int damagePoint)
    {
        currentHealth -= damagePoint;
        if(hpText)
        hpText.text = currentHealth + "/" + maxHealth;
        if(healthbar)
        healthbar.fillAmount = (float)currentHealth / maxHealth;
        if (agent)
            agent.AddReward(-damagePoint / 100f);
    }

    public void resetHealth()
    {
        currentHealth = maxHealth;
        if (healthbar)
            healthbar.fillAmount = (float)currentHealth / maxHealth;
    }
}
