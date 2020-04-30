using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceManager : MonoBehaviour {

    //public GameObject bossPrefab1;
    //public GameObject bossPrefab2;
    //public GameObject[] bossPrefabs;

    //public GameObject bossTemplate;

    public GameObject player;
    public GameObject boss;
	
	//// Update is called once per frame
	//void Update () {
 //       if(Input.GetKeyDown(KeyCode.G))
 //       {
 //           GenerateBoss();
 //       }

 //   }

    //void tryMixBoss() {
    //    GameObject bossObject = Instantiate(bossPrefab1,new Vector3(transform.position.x, 2, transform.position.z),transform.rotation);
    //    BossAttackController bossScript1 = bossObject.GetComponent<BossAttackController>();
    //    bossScript1.MyAwake();
    //    BossAttackController bossScript2 = bossPrefab2.GetComponent<BossAttackController>();
    //    bossScript2.MyAwake();
    //    Attack attack2 = bossScript2.getAttackList()[0];
    //    List<Attack> attackList = bossScript1.getAttackList();
    //    attackList.Add(attack2);
    //    bossScript1.setAttackList(attackList);
    //    Debug.Log(bossObject.GetComponent<BossAttackController>().getAttackList().Count);
    //}

    //public GameObject GenerateBoss()
    //{
    //    GameObject randomedBoss = bossPrefabs[Random.Range(0,bossPrefabs.Length)];
    //    GameObject newBoss = Instantiate(randomedBoss, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    //    newBoss.transform.parent = gameObject.transform;
    //    newBoss.GetComponent<BossAttackController>().setGameInstanceManager(this);
    //    return newBoss;
    //}

    public void DestroyAllAoe()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Aoe" || child.tag == "PlayerShot")
            Destroy(child.gameObject);
        }
    }
}
