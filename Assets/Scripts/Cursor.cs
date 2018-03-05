using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {

    public Image cursorImage;
    public Image chargeImage;

    [Tooltip("How fast the player can self-destruct an object after hovering the cursor over it")]
    [Range(0, 5)]
    public float chargeRate = 0.5f;

    [Tooltip("How much will the bar will charge")]
    [Range(0, 10)]
    public float chargeAmount = 5;

    [Tooltip("How fast the bounds of the cursor turn")]
    [Range(-180, 180)]
    public float turnRate = 5;

    private float chargeTime;

	// Use this for initialization
	void Start () {
        chargeImage.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        RotateCursor();
	}

    private void ChargeSelfDestruct()
    {
        if(Time.time > chargeTime)
        {
            chargeImage.fillAmount += chargeAmount * Time.deltaTime;
            chargeTime = Time.time + chargeRate;
        }
    }

    private void RotateCursor()
    {

        //Quaternion rot = 
        cursorImage.rectTransform.Rotate(0, 0, turnRate*Time.deltaTime);
        //rot.z += turnRate * Time.deltaTime;
        //cursorImage.rectTransform.rotation = rot;

    }
}
