using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[DisallowMultipleComponent]
public class Deflector : MonoBehaviour {

    [Tooltip("The radius of the deflector")]
    public float size;

	// Use this for initialization
	void Start () {
        GetComponent<SphereCollider>().radius = size;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Projectile")
        {
            
        }
    }
}
