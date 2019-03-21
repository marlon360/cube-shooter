using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static int score { get; private set; }
	public UI userinterface;

	public HighscoreManager HSManager;
	public Level level;

	Player player;
	Weapon weapon;

	bool itemActive = false;

	GameObject[] items;

	void Start() {
		score = 0;
		player = FindObjectOfType<Player> ();
		weapon = player.GetComponentsInChildren<Weapon> ()[0];
		Cursor.visible = false;
	}


	void Update(){
		userinterface.setHealth (player.health.InitialValue, player.GetHealth());
		if (userinterface.inGame ()) {
			if (Input.GetButtonDown ("Submit")) {
				if (Time.timeScale == 0) {
					Time.timeScale = 1;
					userinterface.showGame ();
				} else {
					Time.timeScale = 0;
					userinterface.showPause ();
				}
			}

		}
		if (userinterface.menu.activeSelf) {
			if (Input.GetButtonDown ("Submit")) {
				level.startSpawning ();
				Camera.main.GetComponent<CameraFollow> ().switchToPlayerCam ();
			}
			if (Input.GetButtonDown ("BackButton")) {
				userinterface.showHighscores ();
			}
		}

		if (userinterface.highscores.activeSelf) {
			if (Input.GetButtonDown ("Submit")) {
				userinterface.restartScene ();
			}
		}

		if (userinterface.gameover.activeSelf) {
			if (Input.GetButtonDown ("Submit")) {
				userinterface.restartScene ();
			}
		}
		if (userinterface.pause.activeSelf) {
			if (Input.GetButtonDown ("BackButton")) {
				Time.timeScale = 1;
				userinterface.restartScene ();
			}
		}
	}

	public void OnEnemyKilled() {
		score += 5;
		userinterface.setScore (score);
		Debug.Log ("Enemy Killed");
	}

	public void OnPlayerDeath() {
		userinterface.showGameover();
		if (HSManager.isNewHighscore (score)) {
			userinterface.showNewHighscore();
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}
	}

}
