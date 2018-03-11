using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float moveSpeed = 15;

    public GameObject projectile;

    [Range(0, 100)]
    public float chanceOfEightBulletSpawn = 15;

    [HideInInspector]
    public int numberOfBulletsSpawnedOnDeath = 4;
    public int points;
    public GameObject deathEffect;

    void Start()
    {
       numberOfBulletsSpawnedOnDeath = DetermineAmountOfBulletsSpawnedUponDeath();
    }

	// Update is called once per frame
	void Update ()
    {
        Movement();
	}

    void Movement()
    {
        if (GameManager.instance.player)
        {
            transform.LookAt(GameManager.instance.player.transform.position);
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().DestroySelf();
        }
    }

    public void AwardPoints()
    {
        ScoreManager.instance.ScoreIncrease(points);
    }

    private int DetermineAmountOfBulletsSpawnedUponDeath()
    {
        int rand = Random.Range(0, 100);

        if (rand <= chanceOfEightBulletSpawn)
        {
            return 8;
        }
        else
        {
            return 4;
        }
    }
}
