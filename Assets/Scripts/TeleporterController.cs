using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour {
	public GameObject player;
	public GameObject port1;
	public GameObject port2;
	public float lifeTime = 10f;

	public float size = 100f;

	public Color color;

	private Color[] colors = new Color[]{Color.red, Color.green, Color.blue};


	// Use this for initialization
	void Start () {
		if(!GetComponentInParent<TeleporterManager>().chosen3Color)
			color = colors[Random.Range(0, colors.Length)];

		if(player == null)
			player = GameObject.FindWithTag ("Player");

		lifeTime = player.GetComponent<PlayerManager> ().INIT_UPGRADE_TIME;

		port1.transform.position = new Vector3 (Random.Range (-size / 2 + 1, size / 2 - 1), 0, Random.Range (-size / 2 + 1, size / 2 - 1));
		port2.transform.position = new Vector3 (Random.Range (-size / 2 + 1, size / 2 - 1), 0, Random.Range (-size / 2 + 1, size / 2 - 1));
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0) {
			Destroy (this.gameObject);
		}
	}
}
