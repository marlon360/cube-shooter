using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smooth= 5.0f;

	Vector3 playPos = new Vector3(0,10,10);
	Quaternion playRot = Quaternion.Euler (new Vector3(45, 180, 0));

	Quaternion newRotation;
	Vector3 newPosition;

	bool playerCam = false;

	Vector3 offset;

	void Start(){
		offset = new Vector3 (transform.position.x- target.transform.position.x,
							  transform.position.y- target.transform.position.y,
							  transform.position.z- target.transform.position.z);
		
		
		newRotation = transform.rotation;
	}
	[ContextMenu("Switch CAM")]
	public void switchToPlayerCam(){
		newRotation = playRot;
		offset = playPos;
	}

	void LateUpdate() {
		if (target != null) {

			newPosition = new Vector3(target.transform.position.x + offset.x,
				target.transform.position.y + offset.y,
				target.transform.position.z + offset.z);

			transform.position = Vector3.Lerp (transform.position, newPosition,Time.deltaTime * smooth);
			transform.rotation = Quaternion.Lerp (transform.rotation, newRotation,Time.deltaTime * smooth);
		}
	}
}
