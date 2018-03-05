using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
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

    // The next possible time the chargeBar will charge
    private float chargeTime;

	// Use this for initialization
	void Start () {
        chargeImage.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
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
        cursorImage.rectTransform.Rotate(0, 0, turnRate*Time.deltaTime);
    }

    private void Movement()
    {
        Vector3 pos = Input.mousePosition;

        // BUG: Need to reassign z otherwise won't work
        pos.z = 10;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(pos);
        UpdateUIPosition(pos);
    }

    private void UpdateUIPosition(Vector2 pos)
    {
        chargeImage.rectTransform.position = pos;
        cursorImage.rectTransform.position = pos;
    }

    private void UpdateUIPosition(Vector3 pos)
    {
        chargeImage.rectTransform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        cursorImage.rectTransform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }
}
