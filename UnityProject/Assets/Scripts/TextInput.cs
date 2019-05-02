using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextInput : MonoBehaviour {

	public Text switcher;
	public Text next;
	public Text prev;

	public Text scoreAnzeige;

	public HighscoreManager HSManager;
	public UI userInterface;

	int currentIndex = 0;

	string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

	private bool m_isAxisInUse = false;

	private InputField inputField;

	// Use this for initialization
	void Start () {
		inputField = GetComponentInChildren<InputField>();

		switcher.text = ""+alphabet [currentIndex]+"";
		inputField.text = "";
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
		if(!inputField.isFocused) {
			inputField.ActivateInputField();

		}
		if( ControllerInputManager.GetLeftStickVertical() != 0)
		{
			if(m_isAxisInUse == false)
			{
				if (ControllerInputManager.GetLeftStickVertical() > 0) {
					currentIndex = add (currentIndex, alphabet.Length);
					switcher.text = ""+alphabet [currentIndex]+"";
					next.text = ""+alphabet [add (currentIndex, alphabet.Length)]+"";
					prev.text = ""+alphabet [sub (currentIndex, alphabet.Length)]+"";
				}
				if (ControllerInputManager.GetLeftStickVertical() < 0) {
					currentIndex = sub(currentIndex,alphabet.Length);
					switcher.text = ""+alphabet [currentIndex]+"";
					next.text = ""+alphabet [add (currentIndex, alphabet.Length)]+"";
					prev.text = ""+alphabet [sub (currentIndex, alphabet.Length)]+"";
				}
				m_isAxisInUse = true;
			}
		}
		if(ControllerInputManager.GetLeftStickVertical() == 0)
		{
			m_isAxisInUse = false;
		}  

		if (ControllerInputManager.GetAButton()) {
			if (inputField.text.Length < 10) {
				inputField.text += "" + alphabet [currentIndex] + "";
			}
		}
		if (ControllerInputManager.GetBButton()) {
			if (inputField.text.Length > 0) {
				inputField.text = inputField.text.Substring (0, inputField.text.Length - 1);
			}
		}
		if (InputManager.GetSubmitName()) {
			if (inputField.text.Length > 0) {
				HighscoreEntry entry = new HighscoreEntry (inputField.text, GameManager.score);
				HSManager.addHighscore (entry);
				userInterface.showHighscores ();
			}
		}
	}
}
