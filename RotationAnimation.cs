using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour {

    private float rotationSpeed;
    public float rotationSpeedModifier;

	// Use this for initialization
	void Start ()
    {
        rotationSpeedModifier = 20;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rotationSpeed = Time.deltaTime;
        this.transform.Rotate(new Vector3(0, 0, -rotationSpeed*rotationSpeedModifier));
	}
}
