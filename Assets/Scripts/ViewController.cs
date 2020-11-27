using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {
	
	public float translateSpeed = 25;
	public float scaleSpeed = 500;

	void Update () 
	{
		float h = Input.GetAxis("Horizontal") * translateSpeed;
		float v = Input.GetAxis("Vertical") * translateSpeed;

		float mouse = Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;

		transform.Translate(new Vector3(h, mouse, v) * Time.deltaTime, Space.World);
	}
}
