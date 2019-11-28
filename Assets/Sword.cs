using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PerformAttack();
        }
    }
    public void PerformAttack()
    {
        animator.SetTrigger("SwingSword");
    }
}
