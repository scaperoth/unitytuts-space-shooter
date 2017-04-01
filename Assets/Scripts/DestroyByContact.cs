﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

	public GameObject asteroidExplosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Boundary")) {
			return;
		}

		Instantiate (asteroidExplosion, transform.position, transform.rotation);

		if (other.CompareTag ("Player")) {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		if (gameObject.CompareTag ("Asteroid") && other.CompareTag ("Bolt")) {
			gameController.AddScore (scoreValue);
		}

		Destroy (other.gameObject);
		Destroy (gameObject);

	}
}
