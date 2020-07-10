using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameInstanceManager : MonoBehaviour {

    public GA ga;
    public GameObject player;
    public GameObject boss;

    public TimerText timerText;
    public Text currentBossNumber;
    public Text bossHPText;

    public float startTime;

    public void StartFight()
    {
        boss.SetActive(true);
        player.SetActive(true);
        startTime = Time.time;
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B) && boss != null)
        //{
        //    AssignBossToThisInstance(boss);
        //}
    }

    public void DestroyAllAoe()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Aoe" || child.tag == "PlayerShot")
            Destroy(child.gameObject);
        }
    }

    public void RecordData()
    {
        boss.SetActive(false);
        DestroyAllAoe();
        if (timerText)
            timerText.isStop = true;
        if (player.GetComponent<PlayerRuleBased>())
        {
            float timeUsed = Time.time - startTime;
            BossStatisticData bossStatsData = new BossStatisticData
            {
                timeUsed = Time.time - startTime,
                playerHPLeft = Mathf.Max(0, player.GetComponent<PlayerHealth>().currentHealth),
                attackUptimePercentages = player.GetComponent<PlayerRuleBased>().bossAttackUptime / timeUsed
            };
            BossStatistic bossStatistic = boss.GetComponent<BossStatistic>();
            bossStatistic.data.Clear();
            bossStatistic.data.Add(bossStatsData);
            player.SetActive(false);
            ga.AnInstanceEnd();

        }

    }

    public void AssignBossToThisInstance(GameObject newBoss)
    {
        boss = newBoss;
        newBoss.GetComponent<BossAttackController>().gameInstanceManager = GetComponent<GameInstanceManager>();
        newBoss.GetComponent<BossMovementController>().gameInstanceTransform = transform;
        newBoss.transform.parent = this.transform;
        if (player.GetComponent<PlayerRuleBased>())
            player.GetComponent<PlayerRuleBased>().boss = newBoss;
        player.GetComponent<PlayerHealth>().gameInstanceMngr = this;
    }

    public void SaveBoss(int generationNumber)
    {
        // System.DateTime.Now.ToString("_HH:mm:ss_dd-MM-yyyy") + 
        string localPath = "Assets/Boss/Generation[" + generationNumber + "] " + boss.name + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.CreatePrefab(localPath, boss);
    }

    public void Resetkub()
    {
        DestroyAllAoe();
        player.GetComponent<PlayerHealth>().ResetHealth();
        if(player.GetComponent<PlayerRuleBased>())
        player.GetComponent<PlayerRuleBased>().bossAttackUptime = 0;
        boss.GetComponent<BossHealth>().ResetHealthAndAttacks();

        if (timerText)
        {
            timerText.time = 0;
            timerText.isStop = false;

        }

        player.transform.rotation = Quaternion.identity;
        player.transform.position = new Vector3(0, 1, -5)
            + this.transform.position;
        boss.transform.position = new Vector3(0, 2, 0) + this.transform.position;
    }

}
