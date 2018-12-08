using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcChaser : MonoBehaviour {

	public Transform chaseTarget;
	public GameObject stunEffect;

	public float MoveSpeed = 2f;

	public float MinDist = 2f;
	public float MaxDistance = 200f;
	public float terrorMin = 2f;
	public float terrorMax = 10f;

	public int damage = 10;
	public float stunTime = 0;

	private float waitAttack = 0;

	Animation ani;
	public AudioClip[] audioClips;
	public AudioSource audioSource;
	void Start()
	{
		ani = GetComponent<Animation> ();
		audioSource = GetComponent<AudioSource> ();
		ani.Play("creature1Spawn");
		audioSource.clip = audioClips [4];
		audioSource.Play ();

	}

	void Update()
	{
		waitAttack -= Time.deltaTime;
		stunTime -= Time.deltaTime;

		if (stunTime >= 0) {
			return;
		}

		float distance = Vector3.Distance (transform.position, chaseTarget.position);
		Vector3 position = new Vector3 (chaseTarget.position.x, 0, chaseTarget.position.z);

		if ( distance > MinDist && distance < MaxDistance)	// in aggro range
		{
			
			transform.LookAt(position);
			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
			if (!ani.isPlaying)
				ani.Play ("creature1run");
		} else if (distance <= MinDist) 	// in attack range
		{
			transform.LookAt(position);

			if (waitAttack <= 0) {
				switch (Random.Range (0, 3)) {
				case 0:
					ani.Play ("creature1Attack1");
					break;
				case 1:
					ani.Play ("creature1Attack2");
					break;
				case 2:
					ani.Play ("creature1Attack3");
					break;
				}
				audioSource.clip = audioClips [0];
				audioSource.Play ();

				GameObject.Find("Player").GetComponent<PlayerManager> ().ApplyDamage (damage);
				waitAttack = ani.clip.length;
			}

			// Vector3 dir = (chaseTarget.position - transform.position).normalized;
			// GameObject.Find ("Player").GetComponent<Rigidbody> ().AddForce (dir * 8000 * Time.deltaTime);

		} else if (distance >= MaxDistance) 	// out of aggro range
		{
			
		}

		if (distance <= terrorMax && distance >= terrorMin) {
			if (!audioSource.isPlaying) {
				audioSource.clip = audioClips [Random.Range (1, 4)];
				audioSource.Play ();
			}
		}

	}

	public void upgrade(float f){
		MoveSpeed += 7 * f;
		ani ["creature1run"].speed += 0.5f * f;

	}

	public void wait(float f){
		stunTime = f;
		GameObject obj = Instantiate (stunEffect);
		obj.transform.position = this.transform.position;
		audioSource.Stop ();
		audioSource.clip = audioClips [4];
		audioSource.Play ();
	}
}
