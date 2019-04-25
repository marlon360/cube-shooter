using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public LayerMask collisionMask; //Womit kann das Projektil kollidieren
	public float speed = 30; //Geschwindigkeit des Projektils
	float damage;

	float lifetime = 3;//Nach soviele Sekunden wird das Proejektil zerstört, wenn es keine Kollision 

	void Start() {
		Destroy (gameObject, lifetime); //Zerstört sich selbst nach bestimmter Zeit

		//Wenn Kugel in einem Gegner initialisiert wird
		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0], transform.position);
		}
	}

	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance); //Auf Kollision prüfen
		transform.Translate (Vector3.forward * moveDistance); //Kugel nach vorne bewegen
	}


	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + 0.3f, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point);
		}
	}

	void OnHitObject(Collider c, Vector3 hitPoint) {
		Enemy enemy = c.GetComponent<Enemy> ();
		if (enemy != null) {
			enemy.TakeHit(damage, hitPoint, transform.forward);
		}
		GameObject.Destroy (gameObject);
	}

	public void SetDamage(float damage){
		this.damage = damage;
	}

}
