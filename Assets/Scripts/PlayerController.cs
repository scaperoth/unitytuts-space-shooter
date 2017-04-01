using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

	// ship motion and boundary 
	public float speed;
	public float zTilt, xTilt;
	public Boundary boundary;

	// shot variables 
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	// private
	private Rigidbody rb;
	private AudioSource boltSound;
	private float nextFire;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		boltSound = GetComponent<AudioSource> ();
	}

	void Update ()
	{

		// push button and time is ready? 
		if ((Input.GetButton ("Fire1")) && Time.time > nextFire) {

			// add to next fire the current time and 
			// the time it takes for the next shop 
			nextFire = Time.time + fireRate;

			// lock the x rotation to keep the shots from 
			// shooting up or down if the ship changes its orientation
			shotSpawn.rotation = Quaternion.Euler (0.0f, 0.0f, shotSpawn.rotation.eulerAngles.z);

			// drop a new shot onto the scene and it will fire itself
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

			boltSound.Play ();
		}
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
