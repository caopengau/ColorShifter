using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour {

    public Vector3 velocity;

    public int damageAmount = 50;
	private int scorePoint = 1;

	public float projSpeed = 5f;
    public string tagToDamage;
	public GameObject createOnDestroy;
	public GameObject player;
	public Color color;

	void Start(){
		player = GameObject.FindWithTag ("Player");
		GetComponent<Renderer> ().material.color = color;
		GetComponentInChildren<ParticleSystem> ().startColor = color;
	}

    // Update is called once per frame
    void Update () {
		this.transform.Translate(velocity * Time.deltaTime * 2);
	}

    // Handle collisions
    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == tagToDamage && player.transform.Find ("EthanBody").GetComponent<Renderer> ().material.color != this.color) {
			// Damage object with relevant tag
			PlayerManager pManager = col.gameObject.GetComponent<PlayerManager> ();
			pManager.reduceSpeed ();
			pManager.ApplyDamage (damageAmount);

			GameObject obj = Instantiate (this.createOnDestroy);
			obj.transform.position = this.transform.position;
			Destroy (this.gameObject);
		} else if(col.gameObject.tag == tagToDamage && player.transform.Find ("EthanBody").GetComponent<Renderer> ().material.color == this.color){
			PlayerManager pManager = col.gameObject.GetComponent<PlayerManager> ();
			pManager.ApplyBoost (scorePoint);
			Destroy (this.gameObject);

		} else if (col.gameObject.tag == "Wall") {
			Destroy (this.gameObject);
		}
    }

	public void selfName(){
		if (color == Color.blue) {
			tag = "Blue";
		} else if (color == Color.green) {
			tag = "Green";
		} else {
			tag = "Red";
		}
	}
}
