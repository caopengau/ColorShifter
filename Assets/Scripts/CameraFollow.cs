using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	public float cameraMoveSpeed = 120.0f;
	public GameObject cameraFollowObj;
	Vector3 followPOS;
	private const float Y_ANGLE_MIN = 0;
	private const float Y_ANGLE_MAX = 80.0f;
	public float inputSensitivity = 150.0f;

	private float mouseX;
	private float mouseY;

	private float smoothX;
	private float smoothY;
	private float rotX = 0.0f;
	private float rotY = 0.0f;


	void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");

		rotY += mouseX * inputSensitivity * Time.deltaTime;
		rotX -= mouseY * inputSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp (rotX, Y_ANGLE_MIN, Y_ANGLE_MAX);
		Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
		transform.rotation = localRotation;
	}



	void LateUpdate(){
		CameraUpdater ();

	}

	void CameraUpdater(){
		// set target obj to follow
		Transform target = cameraFollowObj.transform;

		float step = cameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
