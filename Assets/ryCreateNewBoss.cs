using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryCreateNewBoss : MonoBehaviour {

    public GameObject bossPrefab1;
    public GameObject bossPrefab2;
    public float x;
    public float z;
	
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Creation();
        }

    }

    void Creation() {
        GameObject bossObject = Instantiate(bossPrefab1,new Vector3(this.transform.position.x+x,2,this.transform.position.z+z),this.transform.rotation);
        BossAttackController bossScript1 = bossObject.GetComponent<BossAttackController>();
        bossScript1.myAwake();
        BossAttackController bossScript2 = bossPrefab2.GetComponent<BossAttackController>();
        bossScript2.myAwake();
        Attack attack2 = bossScript2.getAttackList()[0];
        List<Attack> attackList = bossScript1.getAttackList();
        attackList.Add(attack2);
        bossScript1.setAttackList(attackList);
        Debug.Log(bossObject.GetComponent<BossAttackController>().getAttackList().Count);
    }
}
