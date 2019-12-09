using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public UI userinterface;
	public Weapon weapon;
	public AudioClip spawnsound;

	public string msg;

	public bool triShoot;
	public int bulletIndex;
	public int schaden;

	private ItemManager _itemManager;

	// Use this for initialization
	void Start () {
		_itemManager = GetComponentInParent<ItemManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			userinterface.showMessageForSeconds2 (msg, 10f);
			weapon.setWeapon (triShoot, schaden, bulletIndex);

			AudioSource audio = _itemManager.GetComponent<AudioSource>();
			audio.clip = spawnsound;
			audio.Play();

			gameObject.SetActive(false);
		}
	}
}
