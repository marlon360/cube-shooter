using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 5f; //Geschwindigkeit des Spielers

	Rigidbody playerRigidbody;          // Rigidbody des Spielers
	Animator animator;					// Animator des Spielers

	Vector3 movement;                   // Bewegungsrichtung Vektor

	int floorMask;                      // Layer Mask um Raycast nur auf Boden

	public FloatVariable health;				//aktuelle Lebenspunkte

	public GameEvent DeathEvent;

	//Wird einmal am Anfang ausgeführt
	void Awake(){
		playerRigidbody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
		health.Reset();
	}

	void FixedUpdate(){
		Move ();
		Turn ();
	}
	void Update(){
		Aim ();
	}

	void Move(){

		//Eingaben aus der Tastatur
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		// Bewegungsvektor einstellen, keine Bewegung nach oben (y)
		movement.Set (h, 0f, v);

		//Bewegung an Rotation der Kamera anpassen
		Quaternion camRotaion = Camera.main.transform.rotation;
		movement = camRotaion * movement;
		movement.y = 0f;

		// Normalisieren und an Geschwindigkeit anpassen
		movement = movement.normalized * speed * Time.deltaTime;

		//Wenn sich der Spieler bewegt -> Animator aktivieren
		bool isMoving = h != 0 || v != 0;
		animator.SetBool ("isWalking", isMoving);

		// Rigidbody bewegen
		playerRigidbody.MovePosition (transform.position + movement);

	}

	void Turn(){


		Vector3 NextDir = new Vector3(Input.GetAxis("TurnHorizontal"), 0, Input.GetAxisRaw("TurnVertical"));
		if (NextDir != Vector3.zero) {
			playerRigidbody.MoveRotation(Quaternion.LookRotation (NextDir));
		}


//		// Treffer vom Raycast
//		RaycastHit hit;
//		//Ray von der Mausposition
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//
//		if(Physics.Raycast(ray,out hit,100,floorMask))
//		{
//			// Erstellung des Vektor, der von der Spieler Position bis zu dem Punkt zeigt, den die Maus auf der Maske getroffen hat
//			Vector3 playerToMouse = hit.point - transform.position;
//
//			// Sicherstellen, dass der Vektor parallel zum Boden ist.
//			playerToMouse.y = 0f;
//
//			// Rotation erstellen
//			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);
//
//			// neue Roataion übergeben
//			playerRigidbody.MoveRotation (newRotatation);
//
//		}
	}

	void Aim(){
//		if (Input.GetAxisRaw("Fire2")) { //Rechte Maustaste gedrückt
//			bool currentState = animator.GetBool ("isAiming"); //aktueller Zustand des Zielens
//			animator.SetBool ("isAiming", !currentState); //Animation fürs Zielen starten/beenden
//		}
		if (Input.GetAxisRaw ("Aim") == 1) {
			speed = 4f;
		} else {
			speed = 5f;
		}
		animator.SetBool ("isAiming", Input.GetAxisRaw ("Aim") == 1);

	}

	public void TakeDamage(float damage) {
		health.Value -= damage;
		if (health.Value <= 0) {
			Die();
		}
	}

	public float GetHealth(){
		return health.Value;
	}

	void Die(){
		DeathEvent.Raise();
		gameObject.SetActive (false);
	}

}
