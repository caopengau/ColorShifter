// camera collision is based on code written by Filmstorm
// https://www.youtube.com/watch?v=LbDQHv9z-F0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollision : MonoBehaviour {

	// collision
	public float minDistance = 0.5f;
	public float maxDistance = 5.0f;
	private float smooth = 10.0f;
	Vector3 dollyDir;
	private Vector3 dollyDirAdjusted;
	private float distance;

	// Use this for initialization
	void Awake () {
		dollyDir = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;
	}


	void Update(){
		// Q E change color
		if (Input.GetKeyDown (KeyCode.E)) {
			maxDistance += 1f;
		} else if (Input.GetKeyDown (KeyCode.Q)){
			maxDistance -= 1f;
		} 
	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 desiredCameraPosition = transform.parent.TransformPoint (dollyDir * maxDistance);
		RaycastHit hit;
		if (Physics.Linecast (transform.parent.position, desiredCameraPosition, out hit)) {
			distance = Mathf.Clamp ((hit.distance * 0.9f), minDistance, maxDistance);
		} else {
			distance = maxDistance;
		}
		transform.localPosition = Vector3.Lerp (transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
