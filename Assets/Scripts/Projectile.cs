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
            GameObject ex = Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            ex.transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
            OnDeath(45, other.GetComponent<Enemy>().projectile);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void OnDeath(int startRot, GameObject projectile/*, int numOfBullets = 4*/)
    {
        float rot = startRot;
        GameObject[] bullet = new GameObject[] { projectile, projectile, projectile, projectile };
        //int count = 0;

        for (int i = 0; i < 4; i++)
        {
            bullet[i] = (GameObject)Instantiate(projectile, gameObject.transform.position, new Quaternion(0, rot, 0, Quaternion.identity.w));

            //    //count++;
            //    //Debug.Log("Rot of projectile: " + rot);
            //    Quaternion qRot = new Quaternion(0, rot, 0, Quaternion.identity.w);
            //    Instantiate(projectile, new Vector3(0, 0, 0), qRot);
            //    Debug.Log(qRot);
            //    rot += 90;
            //}

            //Debug.Log(count + " projectiles were spawned");
        }

        bullet[0].GetComponent<Projectile>().direction = Direction.Northwest;
        bullet[1].GetComponent<Projectile>().direction = Direction.Northeast;
        bullet[2].GetComponent<Projectile>().direction = Direction.Southwest;
        bullet[3].GetComponent<Projectile>().direction = Direction.Southeast;
    }
}
