using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour {

	public GameObject shooterTemplate;
	public GameObject target;
	public float difficultyLevel = 1;
	public float test;
	public float nShooters = 10;
	public float size = 100f;
	public float moveSpeed = 0f;
	public bool firstShoot;

	// Use this for initialization
	void Start () {
		firstShoot = !GlobalOptions.TUTORIAL_MODE;
		for (int i = 0; i < nShooters; i++) {
			GenerateShooters();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateShooters(){
		GameObject shooter = GameObject.Instantiate<GameObject>(shooterTemplate);
		shooter.transform.parent = this.transform;


		ColorShooter cs = shooter.transform.Find ("cannonTube").GetComponent<ColorShooter> ();
		cs.difficultyLevel = difficultyLevel;
		cs.player = target;
		shooter.transform.localPosition = new Vector3 (Random.Range (-size / 2 + 3, size / 2 - 3), 0, Random.Range (-size / 2 + 3, size / 2 - 3));
	}

	public void upgrade(float f){
		int level = GameObject.FindWithTag ("Player").GetComponent<PlayerManager> ().difficultLevel;
		if (level >= 5 && level <= 10) {
			moveSpeed++;
		}

		if (level >= 10) {
			GenerateShooters ();
		}

		difficultyLevel += f * 5f;
	}
}
