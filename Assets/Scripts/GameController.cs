using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start ()
	{
		gameOver = false;
		restart = false;
		score = 0;

		restartText.text = "";
		gameOverText.text = "";

		UpdateScoreText ();
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene ("Main");
			}
		}
	}

	IEnumerator SpawnWaves ()
	{	
		yield return new WaitForSeconds (startWait);

		while (!gameOver) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds (spawnWait);

			}
			yield return new WaitForSeconds (waveWait);
		}

		restartText.text = "Press 'R' for Restart";
		restart = true;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScoreText ();
	}

	void UpdateScoreText ()
	{
		scoreText.text = "Score: " + score.ToString ();
	}
}
