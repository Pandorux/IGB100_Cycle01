using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Tooltip("Movement speed of the player")]
    public float moveSpeed = 50;

    [Header("Attack")]
    public GameObject projectile;

    [Tooltip("Where the projectile will spawn")]
    public GameObject projectileSpawnPoint;

    [Tooltip("How fast the player will attack")]
    public float fireRate = 0.15f;
    private float fireTime;

    public GameObject deathEffect;

    private Vector3 pos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
        Movement();
        Boundary ();
        transform.position = pos;
        Shoot();
	}


    /// <summary>
    /// Player Input Controls
    /// </summary>
    private void Movement()
    {
        // Move Right
        if (Input.GetKey(KeyCode.D))      
            pos.x += moveSpeed * Time.deltaTime;

        // Move Left
        if (Input.GetKey(KeyCode.A))
            pos.x -= moveSpeed * Time.deltaTime;

        // Move Up
        if (Input.GetKey(KeyCode.W))
            pos.z += moveSpeed * Time.deltaTime;

        // Move Down
        if (Input.GetKey(KeyCode.S))
            pos.z -= moveSpeed * Time.deltaTime;
    }

	private void Boundary() {

		if (pos.x > GameManager.instance.xBoundary) {
            pos.x = GameManager.instance.xBoundary;
		}
        else if (pos.x < -GameManager.instance.xBoundary)
        {
            pos.x = -GameManager.instance.xBoundary;
		}

        if (pos.z > GameManager.instance.zBoundary)
        {
            pos.z = GameManager.instance.zBoundary;
		}
        else if (pos.z < -GameManager.instance.zBoundary)
        {
            pos.z = -GameManager.instance.zBoundary;
		}
	}

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > fireTime)
        {
            Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            fireTime = Time.time + fireRate;
        }
    }

    public void DestroySelf()
    {
        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
