using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {

	public float speed = 5f; //Geschwindigkeit des Spielers

	[HideInInspector]
	public Rigidbody playerRigidbody; // Rigidbody des Spielers
	[HideInInspector]
	public Animator animator; // Animator des Spielers

	[HideInInspector]
	public Vector3 movement; // Bewegungsrichtung Vektor

	int floorMask; // Layer Mask um Raycast nur auf Boden

	public FloatVariable health; //aktuelle Lebenspunkte

	public GameEvent DeathEvent;
	public GameEvent PlayerHitEvent;

	public AudioClip damageSound;
	private AudioSource audioSource;

	public bool lockInput = false;

	//Wird einmal am Anfang ausgeführt
	void Awake () {
		playerRigidbody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
		health.Reset ();
	}

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate () {
		if (!lockInput) {
			Move ();
			Turn ();
		}
	}
	void Update () {
		if (!lockInput) {
			Aim ();
		}
	}

	void Move () {

		//Eingaben aus der Tastatur
		float h = InputManager.GetHorizontal ();
		float v = InputManager.GetVertical ();

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

	void Turn () {

		if (InputManager.inputType == InputType.Controller) {

			Vector3 NextDir = new Vector3 (ControllerInputManager.GetRightStickHorizontal (), 0, ControllerInputManager.GetRightStickVertical ());
			if (NextDir != Vector3.zero) {
				Quaternion newRotation = Quaternion.LookRotation (NextDir);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 1400f * Time.deltaTime);
			}
		}

		if (InputManager.inputType == InputType.KeyboardMouse) {

			// Treffer vom Raycast
			RaycastHit hit;
			//Ray von der Mausposition
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100, floorMask)) {
				// Erstellung des Vektor, der von der Spieler Position bis zu dem Punkt zeigt, den die Maus auf der Maske getroffen hat
				Vector3 playerToMouse = hit.point - transform.position;

				// Sicherstellen, dass der Vektor parallel zum Boden ist.
				playerToMouse.y = 0f;

				// Rotation erstellen
				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

				// neue Roataion übergeben
				transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 1200f * Time.deltaTime);

			}
		}
	}

	void Aim () {

		if (InputManager.GetAim () == 1) {
			speed = 4f;
		} else {
			speed = 5f;
		}
		animator.SetBool ("isAiming", InputManager.GetAim () == 1);

	}

	public void TakeDamage (float damage) {
		health.Value -= damage;
		PlayerHitEvent.Raise();
		audioSource.PlayOneShot(damageSound, 0.4f);
		if (health.Value <= 0) {
			Die ();
		}
	}

	public float GetHealth () {
		return health.Value;
	}

	void Die () {
		DeathEvent.Raise ();
		//gameObject.SetActive (false);
	}

	public void Reset() {
		health.Value = 30;
	}

}