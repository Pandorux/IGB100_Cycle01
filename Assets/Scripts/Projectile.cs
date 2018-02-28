using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float lifeTime = 3;
    public float moveSpeed = 5;

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
}
