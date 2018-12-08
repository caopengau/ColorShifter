using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterManager : MonoBehaviour {

	public GameObject teleporterTemplate;
	public bool chosen3Color = false;
	public GameObject player;

	void Start () {
		if(player == null)
			player = GameObject.FindWithTag ("Player");

		if (!chosen3Color)
			generateTeleporters (1);
		else
			generate3 ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generateTeleporters(int n){
		if (chosen3Color) {
			generate3 ();
			return;
		}

		for (int i = 0; i < n; i++) {
			GameObject obj = Instantiate (teleporterTemplate);
			obj.transform.parent = this.transform;
		}
	}

	private void generate3(){
		GameObject r = Instantiate (teleporterTemplate);
		r.GetComponent<TeleporterController> ().color = Color.red;
		r.transform.parent = this.transform;
		GameObject b = Instantiate (teleporterTemplate);
		b.GetComponent<TeleporterController> ().color = Color.blue;
		b.transform.parent = this.transform;
		GameObject g = Instantiate (teleporterTemplate);
		g.GetComponent<TeleporterController> ().color = Color.green;
		g.transform.parent = this.transform;
	}
}
