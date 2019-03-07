using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartAfterTime : MonoBehaviour {

	private static float maxTime = 10f;
	public static float timer;
	string text = "";

	// Use this for initialization
	void Start () {
		timer = maxTime;
	}

	public static void resetTimer(){
		timer = maxTime;
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if (Input.anyKey) {
			resetTimer ();
		}
		if (timer < 5) {
			text = string.Format("Restart in {0:0.} seconds! Press any key!", timer);
		} else {
			text = "";
		}
		if (timer < 0) {
			SceneManager.LoadScene("main");
		}

	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (1f, 1f, 1f, 1.0f);
		GUI.Label(rect, text, style);
	}

}
