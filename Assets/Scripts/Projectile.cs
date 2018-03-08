using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public enum Direction { North, Northeast, East, Southeast, South, Southwest, West, Northwest };
    Dictionary<string, Vector3> vectors;

    public Direction direction;

    public float lifeTime = 3;
    public float moveSpeed = 5;
    public float damage = 100.0f;
    public float explosionSize;
    public GameObject deathEffect;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeTime);
        vectors = new Dictionary<string, Vector3>
        {         
            { "North", new Vector3(0, 0, moveSpeed) },
            { "Northeast", new Vector3(moveSpeed, 0, moveSpeed) },
            { "East", new Vector3(moveSpeed, 0, 0) },
            { "Southeast", new Vector3(moveSpeed, 0, -moveSpeed) },
            { "South", new Vector3(0, 0, -moveSpeed) },
            { "Southwest", new Vector3(-moveSpeed, 0, -moveSpeed) },
            { "West", new Vector3(-moveSpeed, 0, 0) },
            { "Northwest", new Vector3(-moveSpeed, 0, moveSpeed) }
        };
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement()
    {
        transform.position += Time.deltaTime * vectors[direction.ToString()];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            OnDeath(other.GetComponent<Enemy>().projectile, other.GetComponent<Enemy>().numberOfBulletsSpawnedOnDeath);
            if(GameManager.instance.gameRunning)
                other.gameObject.GetComponent<Enemy>().AwardPoints();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().DestroySelf();
            Destroy(gameObject);
            GameManager.instance.GameOver();

        }
    }

    public void OnDeath(GameObject projectile, int numOfBullets = 4)
    {
        numOfBullets = numOfBullets == 8 || numOfBullets == 4 ? numOfBullets : 4;
        float rotAmount = 360 / numOfBullets;
        int pattern = Random.Range(0, 2);
        float rot = numOfBullets == 8 ? 0 : 45 * pattern;

        GameObject[] bullet = new GameObject[numOfBullets];

        for (int i = 0; i < numOfBullets; i++)
        {
            bullet[i] = Instantiate(projectile, gameObject.transform.position, new Quaternion(0, rot, 0, Quaternion.identity.w));
            Debug.Log("rot:" + rot);
            rot += rotAmount;
        }

        if (numOfBullets == 8)
        {
            bullet[0].GetComponent<Projectile>().direction = Direction.North;
            bullet[1].GetComponent<Projectile>().direction = Direction.Northeast;
            bullet[2].GetComponent<Projectile>().direction = Direction.East;
            bullet[3].GetComponent<Projectile>().direction = Direction.Southeast;
            bullet[4].GetComponent<Projectile>().direction = Direction.South;
            bullet[5].GetComponent<Projectile>().direction = Direction.Southwest;
            bullet[6].GetComponent<Projectile>().direction = Direction.West;
            bullet[7].GetComponent<Projectile>().direction = Direction.Northwest;
        }
        else
        {
            switch (pattern)
            {
                case 1:
                    bullet[0].GetComponent<Projectile>().direction = Direction.Northeast;
                    bullet[1].GetComponent<Projectile>().direction = Direction.Southeast;
                    bullet[2].GetComponent<Projectile>().direction = Direction.Southwest;
                    bullet[3].GetComponent<Projectile>().direction = Direction.Northwest;
                    break;

                case 0:
                    bullet[0].GetComponent<Projectile>().direction = Direction.North;
                    bullet[1].GetComponent<Projectile>().direction = Direction.East;
                    bullet[2].GetComponent<Projectile>().direction = Direction.South;
                    bullet[3].GetComponent<Projectile>().direction = Direction.West;
                    break;
            }
        }

    }
}
