using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public static bool itemActive { get; set; }

	public Item[] items;

	public Weapon weapon;
	public UI userinterface;

	Item currentItem;

	// Use this for initialization
	void Start () {

		itemActive = false;
		currentItem = items [Random.Range (0, items.Length)];
	}

	
	// Update is called once per frame
	void Update () {


	}

	public void Reset() {
		if (currentItem != null) {
			currentItem.gameObject.SetActive(false);
		}
	}
		
	public void OnEnemyDeath(TransformVariable transform){
		if (!itemActive) {
			if (GameManager.score % 20 == 0 && GameManager.score != 0) {
				if (transform != null) {
					if (currentItem != null) {
						//aktuelles Item ausblenden
						currentItem.gameObject.SetActive (false);
						//neues Item festlegen
						currentItem = items [Random.Range (0, items.Length)];
						//Position für Spawn bestimmen
						Vector3 newPos = new Vector3 (transform.Value.position.x, currentItem.gameObject.transform.position.y, transform.Value.position.z);
						//Position setzten
						currentItem.gameObject.transform.position = newPos;
						//neues Item einblenden
						currentItem.gameObject.SetActive (true);
						itemActive = true;
					} else {
						currentItem = items [Random.Range (0, items.Length)];
					}
				}
			}
		}
	}
}
