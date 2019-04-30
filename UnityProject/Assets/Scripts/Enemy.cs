using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {

	NavMeshAgent pathfinder;
	Transform target;

	public float startingHealth;
	protected float health;
	protected bool dead;

	public ParticleSystem deathEffect;
	public AudioClip deathSound;
	public ParticleSystem hitEffect;
	public AudioClip hitSound;
	public AudioClip awakeSound;
	float nextAttackTime;

	Player player;
	public float damage = 10;

	public GameEvent DeathEvent;

	public TransformVariable ItemSpawningPoint;

	static bool isChasing;

	private AudioSource audioSource;

	void Start () {
		pathfinder = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		player = target.GetComponent<Player> ();

		health = startingHealth;
		isChasing = true;

		audioSource = GetComponent<AudioSource>();

		audioSource.PlayOneShot(awakeSound, 0.3f);

	}

	void Update () {

		if (isChasing) {
			checkForAttack ();
			updateDestination ();
		} else {
			pathfinder.enabled = false;
		}

	}

	void checkForAttack () {
		if (Time.time > nextAttackTime) {
			if (target) {
				float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
				if (sqrDstToTarget < 5) {
					StartCoroutine (Attack ());
					nextAttackTime = Time.time + 1f;
				}
			}
		}
	}

	void updateDestination () {
		if (pathfinder.enabled) {
			Vector3 targetPosition = new Vector3 (target.position.x, 0, target.position.z);
			pathfinder.SetDestination (targetPosition);
		}
	}

	public void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {

		health -= damage;

		if (!dead) {
			if (health <= 0) {
				ItemSpawningPoint.SetValue (transform);
				DeathEvent.Raise ();
				Die ();
				PlaySound(hitSound, 0.4f, hitPoint);
				PlaySound(deathSound, 0.1f, hitPoint, UnityEngine.Random.Range(0.3f, 1f));
				Destroy (Instantiate (deathEffect.gameObject, hitPoint, Quaternion.FromToRotation (Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
			} else {
				PlaySound(hitSound, 0.4f, hitPoint);
				Destroy (Instantiate (hitEffect.gameObject, new Vector3 (transform.position.x, hitPoint.y, transform.position.z), Quaternion.FromToRotation (Vector3.forward, hitDirection)) as GameObject, hitEffect.startLifetime);
			}
		}

	}

	private void PlaySound(AudioClip audio, float volume, Vector3 pos, float pitch = 1f) {
		var go = Instantiate(new GameObject(), pos, Quaternion.identity);
		AudioSource audioSource = go.AddComponent(typeof(AudioSource)) as AudioSource;
		audioSource.spatialBlend = 1f;
		audioSource.pitch = pitch;
		audioSource.PlayOneShot(audio, volume);
		Destroy(go, audio.length);
	}

	protected void Die () {
		dead = true;
		GameObject.Destroy (gameObject);
	}
	IEnumerator Attack () {
		//pathfinder.enabled = false;

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		Vector3 attackPosition = target.position - dirToTarget * (0.5f);

		float attackSpeed = 2;
		float percent = 0;

		bool hasAppliedDamage = false;

		while (percent <= 1) {

			if (percent >= .5f && !hasAppliedDamage) {
				hasAppliedDamage = true;
				player.TakeDamage (damage);
			}

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow (percent, 2) + percent) * 4;
			transform.position = Vector3.Lerp (originalPosition, attackPosition, interpolation);

			yield return null;
		}

		pathfinder.enabled = true;
	}

	public static void stopChasing () {
		isChasing = false;
	}

}