using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour {

	public Text score;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		score.text = GameManager.score.ToString ("D9");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
