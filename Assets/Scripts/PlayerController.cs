using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float zTilt, xTilt;
	public Boundary boundary;

	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal * speed, 0.0f, moveVertical * speed);
		rb.velocity = movement;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (rb.velocity.z * zTilt, 0.0f, rb.velocity.x * -xTilt);
	}
}
