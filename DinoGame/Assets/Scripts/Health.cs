using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public float initialHealth;
    public float health;

	// Use this for initialization
	void Start ()
    {
        health = initialHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void takeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Debug.Log("Death");
            if (this.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
