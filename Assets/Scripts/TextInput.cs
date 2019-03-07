using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

	public Text switcher;
	public Text input;
	public Text next;
	public Text prev;

	public Text scoreAnzeige;

	public HighscoreManager HSManager;
	public UI userInterface;

	int currentIndex = 0;

	string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

	private bool m_isAxisInUse = false;

	// Use this for initialization
	void Start () {

		switcher.text = ""+alphabet [currentIndex]+"";
		input.text = "";
		next.text = ""+alphabet [add (currentIndex, alphabet.Length)]+"";
		prev.text = ""+alphabet [sub (currentIndex, alphabet.Length)]+"";

	}

	void OnEnable(){
		scoreAnzeige.text = GameManager.score.ToString("D9");
	}

	int add(int index, int size){
		if (index + 1 < size) {
			return index+1;
		} else {
			return 0;
		}
	}

	int sub(int index, int size){
		if (index  - 1 < 0) {
			return size - 1;
		} else {
			return index -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetAxisRaw ("Vertical") != 0)
		{
			RestartAfterTime.resetTimer ();
			if(m_isAxisInUse == false)
			{
				if (Input.GetAxisRaw ("Vertical") > 0) {
					currentIndex = add (currentIndex, alphabet.Length);
					switcher.text = ""+alphabet [currentIndex]+"";
					next.text = ""+alphabet [add (currentIndex, alphabet.Length)]+"";
					prev.text = ""+alphabet [sub (currentIndex, alphabet.Length)]+"";
				}
				if (Input.GetAxisRaw ("Vertical") < 0) {
					currentIndex = sub(currentIndex,alphabet.Length);
					switcher.text = ""+alphabet [currentIndex]+"";
					next.text = ""+alphabet [add (currentIndex, alphabet.Length)]+"";
					prev.text = ""+alphabet [sub (currentIndex, alphabet.Length)]+"";
				}
				m_isAxisInUse = true;
			}
		}
		if( Input.GetAxisRaw ("Vertical") == 0)
		{
			m_isAxisInUse = false;
		}  

		if (Input.GetButtonDown ("Select")) {
			if (input.text.Length < 10) {
				input.text += "" + alphabet [currentIndex] + "";
			}
		}
		if (Input.GetButtonDown ("Delete")) {
			if (input.text.Length > 0) {
				input.text = input.text.Substring (0, input.text.Length - 1);
			}
		}
		if (Input.GetButtonDown ("Submit")) {
			if (input.text.Length > 0) {
				HighscoreEntry entry = new HighscoreEntry (input.text, GameManager.score);
				HSManager.addHighscore (entry);
				userInterface.showHighscores ();
			}
		}
	}
}
