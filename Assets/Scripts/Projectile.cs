using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float lifeTime = 3;
    public float moveSpeed = 5;
    public float damage = 100.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement()
    {
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            OnDestroy(45, other.GetComponent<Enemy>().projectile);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void OnDestroy(int startRot, GameObject projectile, int numOfBullets = 4)
    {
        float rot = startRot;

        for (int i = 0; i < numOfBullets; i++)
        {
            rot += 90;
            Instantiate(projectile, gameObject.transform.position, new Quaternion(0, rot, 0, Quaternion.identity.w));
        }
    }
}
