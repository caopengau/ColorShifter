using UnityEngine;
using System.Collections;

public class ColorShooter : MonoBehaviour {

    public ProjectileController projectilePrefab;
    public GameObject destroyExplosionPrefab;
	public GameObject player;

	public float difficultyLevel = 1;
	private Color color;
	public bool firstShoot;

	AudioSource audioSource;

	Color[] colors = new Color[]{Color.red, Color.green, Color.blue};

    void Start ()
    {

		audioSource = GetComponent<AudioSource> ();
        // Get player reference if none attached already
        if (this.player == null)
        {
			player = GameObject.FindGameObjectWithTag ("PlayerTarget");
        }


    }

    // This should be hooked up to the health manager on this object
    public void ShootEffect()
    {
        // Create explosion effect
        GameObject explosion = Instantiate(this.destroyExplosionPrefab);
        explosion.transform.position = this.transform.position;
    }
    
    // Update is called once per frame
    void Update ()
    {
	
		firstShoot = transform.GetComponentInParent<ShooterManager> ().firstShoot;

		this.difficultyLevel = transform.root.GetComponent<ShooterManager> ().difficultyLevel;
        // HealthManager healthManager = this.gameObject.GetComponent<HealthManager>();
        // MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
		this.transform.LookAt (player.transform);

        // Randomly fire a projectile
		if (Random.value < (0.0005f + (0.004f * difficultyLevel)) && firstShoot)
        {
			color = colors[Random.Range(0, colors.Length)];
            ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
			p.transform.parent = GameObject.Find ("ProjectileContainer").transform;

			ShootEffect ();
			audioSource.Play ();

			if (GlobalOptions.TUTORIAL_MODE) {
				p.color = GameObject.FindWithTag ("Player").GetComponent<PlayerManager> ().transform.Find ("EthanBody").GetComponent<Renderer> ().material.color;			
			}
			else
				p.color = color;

			p.GetComponent<Renderer> ().material.color = color;
			p.selfName ();
			p.transform.position = new Vector3 (this.transform.position.x, 1f, this.transform.position.z);
            p.velocity = (player.transform.position - p.transform.position).normalized * 5.0f;

        }
	}
}
