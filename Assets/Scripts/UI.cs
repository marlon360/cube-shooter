using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	[Header("UIs")]
	public GameObject menu;
	public GameObject game;
	public GameObject gameover;
	public GameObject pause;
	public GameObject newHighscore;
	public GameObject highscores;

	[Header("Punkte")]
	public Text score;
	[Header("Lebensanzeige")]
	public Image health;
	public Color startColor;
	public Color endColor;

	[Header("Nachricht")]
	public GameObject msgPanel;
	public Text msgText;
	public Image process;
	private bool timerRunning = false;
	private float startTime = 2;
	private float curTime = 2;

	[Header("Nachricht2")]
	public GameObject msgPanel2;
	public Text msgText2;
	public Image process2;
	private bool timerRunning2 = false;
	private float startTime2 = 2;
	private float curTime2 = 2;


	public void setScore(int scoreValue){
		if (score != null) {
			score.text = scoreValue.ToString ("D5");
		}
	}

	void Start(){
		showMenu ();
	}

	void Update(){
		if (timerRunning) {
			curTime -= Time.deltaTime;
			if (curTime > 0) {
				process.fillAmount = (curTime / startTime);
			} else {
				process.fillAmount = 0;
				hideMessage ();
				timerRunning = false;
			}
		}
		if (timerRunning2) {
			curTime2 -= Time.deltaTime;
			if (curTime2 > 0) {
				process2.fillAmount = (curTime2 / startTime2);
			} else {
				process2.fillAmount = 0;
				hideMessage2 ();
				timerRunning2 = false;
			}
		}
	}

	private void showMessage(string msg){
		msgText.text = msg;
		msgPanel.GetComponent<Animator> ().SetBool ("visible", true);
	}
	private void hideMessage(){
		msgPanel.GetComponent<Animator> ().SetBool ("visible", false);
	}
	private void showMessage2(string msg){
		msgText2.text = msg;
		msgPanel2.GetComponent<Animator> ().SetBool ("visible", true);
	}
	private void hideMessage2(){
		msgPanel2.GetComponent<Animator> ().SetBool ("visible", false);
		ItemManager.itemActive = false;
	}

	public void showMessageForSeconds(string msg, float sec){
		showMessage (msg);
		timerRunning = true;
		startTime = sec;
		curTime = sec;
	}
	public void showMessageForSeconds2(string msg, float sec){
		showMessage2 (msg);
		timerRunning2 = true;
		startTime2 = sec;
		curTime2 = sec;
	}

	public void setHealth(float start, float current){
		float ratio = current / start;
		health.fillAmount = Mathf.Lerp(health.fillAmount,ratio,Time.deltaTime*10f);
		health.color = Color.Lerp(endColor, startColor, ratio);
	}

	private void hideAll(){
		menu.SetActive (false);
		game.SetActive (false);
		gameover.SetActive (false);
		pause.SetActive (false);
		newHighscore.SetActive (false);
		highscores.SetActive (false);
	}

	public void showMenu(){
		hideAll ();
		menu.SetActive (true);
	}

	public void showGame(){
		hideAll ();
		game.SetActive (true);
	}

	public void showGameover(){
		hideAll ();
		gameover.SetActive (true);
	}

	public void showPause(){
		hideAll ();
		pause.SetActive (true);
	}

	public void showNewHighscore(){
		hideAll ();
		newHighscore.SetActive (true);
	}

	public void showHighscores(){
		hideAll ();
		highscores.SetActive (true);
	}

	public void restartScene(){
		SceneManager.LoadScene("main");
	}

	public void quit(){
		Application.Quit();
	}

	public bool inGame(){
		return game.activeSelf || pause.activeSelf;
	}
		

}
