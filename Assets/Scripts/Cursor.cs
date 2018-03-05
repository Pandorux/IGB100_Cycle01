using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {


    [Tooltip("How fast the player can self-destruct an object after hovering the cursor over it")]
    [Range(0, 1)]
    public float chargeRate = 0.5;

    [Tooltip("How fast the bounds of the cursor turn")]
    [Range(-180, 180)]
    public float turnRate = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ChargeSelfDestruct()
    {

    }

    private void RotateCursor()
    {

    }
}
