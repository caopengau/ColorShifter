using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public GameObject exit;
	public float disableTimer = 0;
	public float size = 100f;

	private Color color;
	ParticleSystem [] particlesyss;
	public GameObject teleportEffect;


	void Start () {
		color = GetComponentInParent<TeleporterController> ().color;
		particlesyss = GetComponentsInChildren<ParticleSystem> ();
		foreach(ParticleSystem s in particlesyss){
			s.startColor = color;
		}

		// GetComponent<Renderer> ().material.color = color;
		if(player == null)
			player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (disableTimer >= 0)
			disableTimer -= Time.deltaTime;
		

	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		
		if (player!=null && other.gameObject.CompareTag ("Player") && player.transform.Find ("EthanBody").GetComponent<Renderer> ().material.color == color && disableTimer <= 0)
		{
			exit.GetComponent<Portal> ().disableTimer = 1.0f;
			Vector3 position = exit.transform.position;
			position.y += 5;
			player.transform.position = position;
			createOnExit ();
			exit.GetComponent<Portal> ().createOnExit ();
		}
		if (other.gameObject.CompareTag ("Portals")) {
			transform.position = new Vector3 (Random.Range (-size / 2 + 1, size / 2 - 1), 0, Random.Range (-size / 2 + 1, size / 2 - 1));
		}
		if (other.gameObject.CompareTag ("Cannons")) {
			transform.position = new Vector3 (Random.Range (-size / 2 + 1, size / 2 - 1), 0, Random.Range (-size / 2 + 1, size / 2 - 1));
		}
	}

	public void createOnExit(){
		GameObject obj = Instantiate (teleportEffect);
		obj.GetComponent<ParticleSystem> ().startColor = color;
		obj.transform.Find ("Smoke").GetComponent<ParticleSystem> ().startColor = color;
		obj.transform.Find ("Light").GetComponent<ParticleSystem> ().startColor = color;

		Vector3 pos = transform.position;
		pos.y += 1;
		obj.transform.position = pos;
	}
}
