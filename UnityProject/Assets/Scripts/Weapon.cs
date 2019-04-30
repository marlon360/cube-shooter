using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


	public Transform BulletEmitter; //hier werden die Projektile erscheinen
	public GameObject[] Bullets; //Prefab für das Projektil
	public int currentBullet = 0;

	public int schaden = 1; //Schaden von einem Schuss
	public float timeBetweenBullets;//Zeit zwischen den Schüssen

	float timer; //Zeit zählen, um den Abstand der Schüsse zu kontrollieren
	public bool triShoot = false;

	Animator playerAnimator;

	public AudioClip shotSound;
	private AudioSource audioSource;

	void Start(){
		playerAnimator = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		timer += Time.deltaTime;

		if (InputManager.GetShoot() == 1 && timer >= timeBetweenBullets && Time.timeScale != 0 && playerAnimator.GetBool("isAiming"))
		{
			if (currentBullet == 1) {
				audioSource.pitch = 1.2f;
			}
			if (currentBullet == 0) {
				audioSource.pitch = 2.2f;
			}
			audioSource.PlayOneShot(shotSound, 0.2f);
			createBullet ();
			if (triShoot) {
				createBullet (20);
				createBullet (-20);
			}
			timer = 0;
		}
		if (InputManager.GetShoot() != 1) {
			timer = timeBetweenBullets;
		}
		
	}

	void createBullet(float angle = 0){
		Quaternion rotation = BulletEmitter.transform.rotation;
		GameObject bullet = Instantiate (Bullets[currentBullet], BulletEmitter.position, BulletEmitter.rotation) as GameObject;
		bullet.GetComponent<Bullet>().SetDamage(schaden);
		bullet.transform.Rotate(Vector3.up, angle, Space.World);
	}

	public void setWeapon(bool trishoot, int schaden, int bullet){
		this.triShoot = trishoot;
		this.schaden = schaden;
		this.currentBullet = bullet;
		StartCoroutine(setNormal (10f));
	}
	IEnumerator setNormal(float waitTime){
		float counter = 0f;
		while (counter < waitTime){
			counter += Time.deltaTime;
			yield return null; //Don't freeze Unity
		}
		triShoot = false;
		schaden = 1;
		currentBullet = 0;
	}


}



