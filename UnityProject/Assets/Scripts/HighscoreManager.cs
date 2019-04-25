using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour {

	private int maxEntries = 5;

	public Text[] nameTexts;
	public Text[] scoreTexts;

	void Awake(){
		setUI ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ContextMenu("Reset HighScore!")]
	void reset(){
		for (int i = 0; i < maxEntries; i++) {
			PlayerPrefs.SetString (i + "Name", "---------");
			PlayerPrefs.SetInt (i + "Score", 0);
		}
		setUI ();
	}

	public bool isNewHighscore(int score){
		for (int i = 0; i < maxEntries; i++) {
			HighscoreEntry entry = getEntryOfIndex (i);
			if (entry == null) {
				return true;
			}
			if (entry.score < score) {
				return true;
			}
		}

		return false;
	}

	public void addHighscore(HighscoreEntry newEntry){

		HighscoreEntry oldEntry;

		for (int i = 0; i < maxEntries; i++) {
			HighscoreEntry entry = getEntryOfIndex (i);
			if (entry == null) {
				reset ();
				entry = getEntryOfIndex (i);
			}
			if (entry.score < newEntry.score) {
				oldEntry = entry;
				addEntryAtIndex (newEntry, i);
				newEntry = oldEntry;
			}
		}
		setUI ();
	}

	void setUI(){
		for (int i = 0; i < maxEntries; i++) {
			HighscoreEntry entry =  getEntryOfIndex(i);
			nameTexts [i].text = entry.name;
			scoreTexts [i].text = entry.score.ToString("D8");
		}
	}

	HighscoreEntry getEntryOfIndex(int index){
		if (PlayerPrefs.HasKey (index + "Score")) {
			return new HighscoreEntry (PlayerPrefs.GetString (index + "Name"), PlayerPrefs.GetInt (index + "Score"));
		} else {
			reset ();
			return getEntryOfIndex(index);
		}
	}

	void addEntryAtIndex(HighscoreEntry entry, int index){
		if (PlayerPrefs.HasKey (index + "Score")) {
			PlayerPrefs.SetString (index + "Name",entry.name);
			PlayerPrefs.SetInt (index + "Score",entry.score);
		}
	}



}

public class HighscoreEntry{
	public string name;
	public int score;
	public HighscoreEntry(string name, int score){
		this.name = name;
		this.score = score;
	}
}
