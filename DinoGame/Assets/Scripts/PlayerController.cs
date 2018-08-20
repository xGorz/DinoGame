using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Animator animator;
	// Use this for initialization
	void Start ()
    {
        animator = this.GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Horizontal"))
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Idle", true);
	}
}
