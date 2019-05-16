using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterAgent : Agent {

    private Player player;
    private RayPerception3D rayPer;
    private Weapon weapon;
    private GameManager gm;
    private Level level;
    private ItemManager itemManager;

    private bool isAiming = false;


    public override void InitializeAgent() {
        base.InitializeAgent();
        rayPer = GetComponent<RayPerception3D>();
        player = GetComponent<Player> ();
        player.lockInput = true;
        weapon = GetComponentInChildren<Weapon>();
        weapon.lockInput = true;
        isAiming = false;
        gm = FindObjectOfType<GameManager>();
        gm.StartGame();
        level = FindObjectOfType<Level>();
        itemManager = FindObjectOfType<ItemManager>();
        itemManager.Reset();
    }

    public override void CollectObservations() {

            float rayDistance = 50f;
            float[] rayAngles = { 30f, 60f, 90f, 120f, 150f, 180f, 210f, 240f, 270f, 300f, 330f };
            string[] detectableObjects = { "Wall", "Enemy", "Item" };
            AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1f, 0f));

            AddVectorObs(isAiming);

            AddVectorObs(weapon.canShoot());

            AddVectorObs(weapon.currentBullet);

            AddVectorObs(weapon.triShoot);

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        float movementVertical = Mathf.Clamp(vectorAction[0], -1f, 1f);
        float movementHorizontal = Mathf.Clamp(vectorAction[1], -1f, 1f);

        Move(movementHorizontal, movementVertical);

        // float turnVertical = Mathf.Clamp(vectorAction[2], -1f, 1f);
        // float turnHorizontal = Mathf.Clamp(vectorAction[3], -1f, 1f);
        Vector3 rotateDir = transform.up * Mathf.Clamp(vectorAction[2], -1f, 1f);
        //Turn(turnHorizontal, turnVertical);
        Turn(rotateDir);

        bool aimCommand = Mathf.Clamp(vectorAction[3], -1f, 1f) > -0.5f;

        Aim(aimCommand);

        bool shootCommand = Mathf.Clamp(vectorAction[4], -1f, 1f) > 0.3f;

        Shoot(shootCommand);
    }

    public override void AgentReset() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}
        level.Reset();
        gm.StartGame();
        player.Reset();
        weapon.Reset();
        transform.position = new Vector3(-1.6f, 0, 2f);
    }


    void Move (float h, float v) {

        // Bewegungsvektor einstellen, keine Bewegung nach oben (y)
        player.movement.Set (h, 0f, v);

        //Bewegung an Rotation der Kamera anpassen
        Quaternion camRotaion = Camera.main.transform.rotation;
        player.movement = camRotaion * player.movement;
        player.movement.y = 0f;

        // Normalisieren und an Geschwindigkeit anpassen
        player.movement = player.movement.normalized * player.speed * Time.deltaTime;

        //Wenn sich der Spieler bewegt -> Animator aktivieren
        bool isMoving = h != 0 || v != 0;
        player.animator.SetBool ("isWalking", isMoving);

        // Rigidbody bewegen
        player.playerRigidbody.MovePosition (transform.position + player.movement);
    }

    void Turn (Vector3 rotateDir) {

        // Vector3 NextDir = new Vector3 (h, 0, v);
        // if (NextDir != Vector3.zero) {
        //     Quaternion newRotation = Quaternion.LookRotation (NextDir);
        //     transform.rotation = Quaternion.RotateTowards (transform.rotation, newRotation, 1400f * Time.deltaTime);
        // }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 500f);
    }

    void Aim (bool aiming) {
        isAiming = aiming;
		if (aiming) {
			player.speed = 4f;
		} else {
			player.speed = 5f;
		}
		player.animator.SetBool ("isAiming", aiming);

	}

    void Shoot(bool shoot) {

        if (shoot && weapon.canShoot()) {
            AddReward(-0.001f);
            weapon.Shoot();
            weapon.timer = 0;
        }
        if (!shoot) {
            weapon.ResetTime();
        }

    }

    public void GotHit() {
        AddReward(-0.2f);
    }

    public void Death() {
        AddReward(-1);
        Done();
    }

    public void HitEnemy() {
        AddReward(0.1f);
    }

    public void KilledEnemy() {
        AddReward(0.2f);
    }

}