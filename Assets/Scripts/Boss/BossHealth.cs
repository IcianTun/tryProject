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

    private BossAttackController bossAttackController;
    private BossMovementController bossMovementController;

    public float startTime;

    [SerializeField]
    List<float> timeToBeatList;

    void Start()
    {
        bossAttackController = GetComponent<BossAttackController>();
        bossMovementController = GetComponent<BossMovementController>();
        currentHealth = maxHealth;
        startTime = Time.time;
        if (timeToBeatList == null)
        {
            timeToBeatList = new List<float>();
        }

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

        if (currentHealth <= 0)
        {
            RecordData();
            PlayerRuleBased playerAIScript = bossAttackController.gameInstanceManager.player.GetComponent<PlayerRuleBased>();
            if (playerAIScript)
            {
                playerAIScript.GetBattleData();
            }
            else
            {
                PlayerMovement tstScript = bossAttackController.gameInstanceManager.player.GetComponent<PlayerMovement>();

                if (!tstScript)
                    Debug.Log("ERROR script not found");
                else
                    tstScript.GetBattleData();
            }
        }
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        bossAttackController.MyReset();
        bossMovementController.Stop();
    }

    public void RecordData()
    {
        timeToBeatList.Add(startTime - Time.time);
    }
}
