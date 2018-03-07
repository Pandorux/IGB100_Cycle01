using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float moveSpeed = 15;
    public float health = 100;

    public GameObject projectile;
    public int numberOfBulletsSpawnedOnDeath = 4;

    public float damage = 25;
    private float damageRate = 0.2f;
    private float damageTime;

    public GameObject deathEffect;

	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        if (GameManager.instance.player)
        {
            transform.LookAt(GameManager.instance.player.transform.position);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && Time.time > damageTime)
        {
            other.GetComponent<Player>().DestroySelf();
        }
    }
}
