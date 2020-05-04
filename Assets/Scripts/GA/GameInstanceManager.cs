using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameInstanceManager : MonoBehaviour {

    public GA ga;
    public GameObject player;
    public GameObject boss;

    public float startTime;

    public void StartFight()
    {
        boss.SetActive(true);
        player.SetActive(true);
        startTime = Time.time;
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
        DestroyAllAoe();
        float timeUsed = Time.time - startTime;
        BossStatisticData bossStatsData = new BossStatisticData
        {
            timeUsed = Time.time - startTime,
            playerHPLeft = Mathf.Max(0,player.GetComponent<PlayerHealth>().currentHealth),
            attackUptimePercentages = player.GetComponent<PlayerRuleBased>().bossAttackUptime / timeUsed
        };
        BossStatistic bossStatistic = boss.GetComponent<BossStatistic>();
        bossStatistic.data.Clear();
        bossStatistic.data.Add(bossStatsData);
        player.SetActive(false);
        boss.SetActive(false);
        ga.AnInstanceEnd();
    }

    public void AssignBossToThisInstance(GameObject newBoss)
    {
        boss = newBoss;
        newBoss.GetComponent<BossAttackController>().gameInstanceManager = GetComponent<GameInstanceManager>();
        newBoss.GetComponent<BossMovementController>().gameInstanceTransform = transform;
        newBoss.transform.parent = this.transform;
        player.GetComponent<PlayerRuleBased>().boss = newBoss;
        player.GetComponent<PlayerHealth>().gameInstanceMngr = this;
    }

    public void SaveBoss()
    {
        // System.DateTime.Now.ToString("_HH:mm:ss_dd-MM-yyyy") + 
        string localPath = "Assets/Boss/" + boss.name + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.CreatePrefab(localPath, boss);
    }

    public void Resetkub()
    {
        player.GetComponent<PlayerHealth>().ResetHealth();
        player.GetComponent<PlayerRuleBased>().bossAttackUptime = 0;
        boss.GetComponent<BossHealth>().ResetHealthAndAttacks();

        player.transform.rotation = Quaternion.identity;
        player.transform.position = new Vector3(0, 1, -5)
            + this.transform.position;
        boss.transform.position = new Vector3(0, 2, 0) + this.transform.position;
    }

}
