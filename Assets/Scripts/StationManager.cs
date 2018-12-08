using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour {

	public GameObject station_template;
	public int nStations = 30;
	public float size = 100f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < nStations; i++) {
			GenerateStation();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateStation(){
		GameObject station = GameObject.Instantiate<GameObject>(station_template);
		station.transform.parent = this.transform;
		station.transform.localPosition = new Vector3 (Random.Range (-size / 2, size / 2), 0, Random.Range (-size / 2, size / 2));
	}
}
