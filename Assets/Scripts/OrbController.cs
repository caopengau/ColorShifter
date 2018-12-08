using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController: MonoBehaviour {

	// Use this for initialization
	public float size = 100f;


	Vector3 pos;
	Color[] colors = new Color[] {Color.red, Color.green, Color.blue};
	Color color;
	ParticleSystem [] particlesyss;

	GameObject [] greens;
	GameObject [] reds;
	GameObject [] blues;

	void Start () {
		transform.position = new Vector3 (Random.Range (-size / 2 + 1, size / 2 - 1), 0.7f, Random.Range (-size / 2 + 1, size / 2 - 1));

		color = colors[Random.Range(0, colors.Length)];

		particlesyss = GetComponentsInChildren<ParticleSystem> ();
		foreach(ParticleSystem s in particlesyss){
			s.startColor = color;
		}

	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && GameObject.FindWithTag("Player").transform.Find("EthanBody").GetComponent<Renderer>().material.color == color) {

			reds = GameObject.FindGameObjectsWithTag ("Red");
			blues = GameObject.FindGameObjectsWithTag ("Blue");
			greens = GameObject.FindGameObjectsWithTag ("Green");

			if (color == Color.red)
				clearOthers (blues, greens);
			else if (color == Color.green)
				clearOthers (blues, reds);
			else if (color == Color.blue)
				clearOthers (reds, greens);

			GameObject.FindWithTag ("Player").GetComponent<PlayerManager> ().ApplyBoost (3);
			Destroy (gameObject);
			/*
			 * follow player
			transform.parent = col.gameObject.transform;
			pos = col.gameObject.transform.position;
			pos.y += 0.7f;
			transform.position = pos;
			*/
		}

	}
	// Update is called once per frame
	void Update () {
		
	}

	void clearOthers(GameObject [] c1, GameObject [] c2){
		for (int i = 0; i < c1.Length; i++) {
			Destroy (c1 [i].gameObject);
		}
		for (int i = 0; i < c2.Length; i++) {
			Destroy (c2 [i].gameObject);
		}
	}

}
