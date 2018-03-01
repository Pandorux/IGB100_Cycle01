using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public float spawnRate = 2.0f;
    private float spawnTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SpawnEnemy();
	}

    private void SpawnEnemy()
    {
        if (Time.time > spawnTimer) {
            Instantiate(enemy, transform.position, transform.rotation);
            spawnTimer = Time.time + spawnRate;
        }
    }
}
