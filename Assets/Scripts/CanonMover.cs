using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMover : MonoBehaviour {

	public float moveSpeed = 1.0f;
	Vector3 targetPosition;
	public static float size = 100f;

	static Vector3 C1 = new Vector3(-size/2 + 3, 0, -size/2 + 3);
	static Vector3 C2 = new Vector3(size/2 - 3, 0, -size/2 + 3);
	static Vector3 C3 = new Vector3(-size/2 + 3, 0, size/2 - 3);
	static Vector3 C4 = new Vector3(size/2 - 3, 0, size/2 - 3);
	Vector3[] targetPostionList = new Vector3[]{C1, C2, C3, C4};

	void Start () {
		getNextTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
		moveSpeed = GetComponentInParent<ShooterManager> ().moveSpeed;

		if (GameObject.FindWithTag ("Player").GetComponent<PlayerManager> ().difficultLevel < 5) {
			return;
		}

		if (transform.position == targetPosition) {
			getNextTarget ();
		}
		transform.LookAt(targetPosition);
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

	}

	void OnCollisionEnter(Collision collision)
	{
		getNextTarget ();
		if (collision.gameObject.tag == "Player") {
			GameObject.FindWithTag ("Player").GetComponent<PlayerManager> ().ApplyDamage (10);
		}

	}

	void getNextTarget(){
		targetPosition = targetPostionList [Random.Range (0, targetPostionList.Length)];
	}

}
