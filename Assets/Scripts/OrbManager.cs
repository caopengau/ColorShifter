using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour {

	public GameObject orbTemplate;

	// Use this for initialization
	void Start () {
		generateOrbs (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void generateOrbs(int n){
		for (int i = 0; i < n; i++) {
			GameObject obj = Instantiate (orbTemplate);
			obj.transform.parent = this.transform;
		}
	}
}
