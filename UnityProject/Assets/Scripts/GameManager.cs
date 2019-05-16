using System.Collections;
using UnityEngine;
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

	void Start () {
		score = 0;
		player = FindObjectOfType<Player> ();
		weapon = player.GetComponentsInChildren<Weapon> () [0];

	}

	void Update () {

		if (!(InputManager.inputType == InputType.KeyboardMouse)) {
			Cursor.visible = false;
		} else {
			Cursor.visible = true;
		}

		userinterface.setHealth (player.health.InitialValue, player.GetHealth ());
		if (userinterface.inGame ()) {
			if (InputManager.GetPause ()) {
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
			if (InputManager.GetStart ()) {
				StartGame ();
			}
			if (InputManager.GetBack ()) {
				userinterface.showHighscores ();
			}
		}

		if (userinterface.highscores.activeSelf) {
			if (InputManager.GetStart ()) {
				userinterface.restartScene ();
			}
		}

		if (userinterface.gameover.activeSelf) {
			if (InputManager.GetStart ()) {
				userinterface.restartScene ();
			}
		}
		if (userinterface.pause.activeSelf) {
			if (InputManager.GetExit ()) {
				Time.timeScale = 1;
				userinterface.restartScene ();
			}
		}
	}

	public void StartGame () {
		level.StartGame ();
		Camera.main.GetComponent<CameraFollow> ().switchToPlayerCam ();
	}

	public void OnEnemyKilled () {
		score += 5;
		userinterface.setScore (score);
	}

	public void OnPlayerDeath () {
		userinterface.showGameover ();
		if (HSManager.isNewHighscore (score)) {
			userinterface.showNewHighscore ();
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}
	}

}