using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
	
	public Text totalBonusText;
	public Text healthText;
	public Text difficultyText;
	public Text speedText;
	public Text timeText;
	public Text totalScore;
	public Text buffText;
	public Text modeText;
	public Image healthBar;

    public int startingHealth = 100;
	public int difficultLevel = 1;

	public GameObject orc = null;
	public GameObject shooters = null;
	public GameObject teleporters = null;
	public GameObject orbs = null;


	public float INIT_UPGRADE_TIME = 10.0f;
	public float upgradeTime;
	public float upgradeFactor = 0.1f;	// how much to upgrade

    private int currentHealth;
	private int totalBonus = 0;
	private int currentScore = 0;
	private float gameTime = 0;
	private float INIT_BUFFTEXT_TIME = 2f;
	private float buffTextTime = 0;
	private Color[] colors = new Color[]{Color.red, Color.green, Color.blue};
	private int colorNumber = 1;
	private string mode = "Human Mode: Evade Dangers (Scroll/Press 1,2,3 to change)";

	public AudioClip[] audioClip;
	AudioSource[] audioSources;
	AudioSource audio1;
	AudioSource audio2;

	// Use this for initialization
	void Start () {


		audioSources = GetComponents<AudioSource> ();
		audio1 = audioSources [0];
		audio2 = audioSources [1];

		popText ("Don't get killed");
		upgradeTime = INIT_UPGRADE_TIME;
        this.ResetToStart();

	}

	void Update(){

		GetComponentInChildren<ParticleSystem> ().startColor = transform.Find ("EthanBody").GetComponent<Renderer> ().material.color;


		buffTextTime -= Time.deltaTime;
		gameTime += Time.deltaTime;

		// scrolling change color
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) { 
			colorNumber += Mathf.CeilToInt (Input.GetAxis ("Mouse ScrollWheel"));
			shooters.GetComponent<ShooterManager> ().firstShoot = true;
			transform.Find ("EthanBody").GetComponent<Renderer> ().material.color = colors [Mathf.Abs(colorNumber) % 3];
		} else if(Input.GetAxis ("Mouse ScrollWheel") < 0){
			colorNumber += Mathf.FloorToInt (Input.GetAxis ("Mouse ScrollWheel"));
			shooters.GetComponent<ShooterManager> ().firstShoot = true;
			transform.Find ("EthanBody").GetComponent<Renderer> ().material.color = colors [Mathf.Abs(colorNumber) % 3];
		}

		// 1,2,3 change color
		if (Input.GetKeyDown ("1")) {
			shooters.GetComponent<ShooterManager> ().firstShoot = true;
			transform.Find ("EthanBody").GetComponent<Renderer> ().material.color = Color.red;
		} else if (Input.GetKeyDown ("2")){
			shooters.GetComponent<ShooterManager> ().firstShoot = true;
			transform.Find ("EthanBody").GetComponent<Renderer> ().material.color = Color.green;
		} else if (Input.GetKeyDown ("3")){
			shooters.GetComponent<ShooterManager> ().firstShoot = true;
			transform.Find ("EthanBody").GetComponent<Renderer> ().material.color = Color.blue;
		}



		Color color = transform.Find ("EthanBody").GetComponent<Renderer> ().material.color;
		if (color == Color.red)
			mode = "Red: Gain-Health Mode";
		if (color == Color.blue)
			mode = "Blue: Stun-Orc Mode";
		if (color == Color.green)
			mode = "Green: Gain-Speed Mode";


		upgradeTime -= Time.deltaTime;	// difficulty increases as time passes
		if (upgradeTime <= 0) {
			upgrade (upgradeFactor);
			upgradeTime = INIT_UPGRADE_TIME;
		}

		if ((float)currentHealth / startingHealth <= 0.5) {	// player low health heart beat
			if (!audio2.isPlaying) {
				audio2.Play ();
			}
		} else {
			audio2.Stop ();
		}
	}

    // Reset health to original starting health
    public void ResetToStart()
    {
		upgradeTime = INIT_UPGRADE_TIME;
        currentHealth = startingHealth;
    }

    // Reduce the health of the object by a certain amount
    // If health lte zero, destroy the object
    public void ApplyDamage(int damage)
    {


		audio1.clip = audioClip [1];
		audio1.Play ();
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
			GlobalOptions.gameTime = (int)Mathf.Round (gameTime);
			GlobalOptions.gameScore = currentScore;
			GlobalOptions.gameBalls = totalBonus;
			GlobalOptions.levelReached = difficultLevel;
			SceneManager.LoadScene ("GameEnded");
        }
    }

	public void GainHealth(int amount){
		currentHealth += amount;
		if (currentHealth > 100) {
			currentHealth = 100;
			popText ("Max Health");
		} else {
			popText ("Gain Health");
		}
	}

	public void ApplyBoost(int bonus)
	{
		GetComponentInChildren<ParticleSystem> ().Play ();

		audio1.clip = audioClip [0];
		audio1.Play ();
		Color color = transform.Find ("EthanBody").GetComponent<Renderer> ().material.color;

		if (color == Color.blue) {
			OrcChaser oc = orc.GetComponent<OrcChaser> ();
			oc.wait (3 * bonus);
			popText ("Orc Stunned");
		} else if (color == Color.green) {
			ThirdPersonCharacter tpc = GetComponent<ThirdPersonCharacter> ();

			if (tpc.boostSpeed (upgradeFactor * bonus))
				popText ("Gain Speed");
			else
				popText ("Max Speed");
		} else if(color == Color.red){
			GainHealth (5 * bonus);

		}

		totalBonus += bonus;
	}

    // Get the current health of the object
    public int GetHealth()
    {
        return this.currentHealth;
    }

	void FixedUpdate(){
		SetText ();
	}

	void SetText(){
		totalBonusText.text = "Total Bonus: " + this.totalBonus;
		healthText.text = "Current Health: " + this.currentHealth;

		currentScore = (int)(totalBonus + Mathf.Round (gameTime)) * difficultLevel;
		totalScore.text = "Total Score: " + currentScore;

		difficultyText.text = "Difficulty Level: " + this.difficultLevel;
		if (GlobalOptions.TUTORIAL_MODE)
			difficultyText.text = "Practice Mode";
		
		speedText.text = "Current Speed: " + GetComponent<ThirdPersonCharacter> ().m_MoveSpeedMultiplier * 10;
		timeText.text = "Time: " + Mathf.Round(gameTime);

		modeText.text = mode;

		if (buffTextTime <= 0)
			buffText.text = "";
		
		healthBar.transform.localScale = new Vector3 ((float)currentHealth/startingHealth, 1f, 1f);
	}


	void upgrade(float f){
		difficultLevel++;

		if (difficultLevel == 5 && !GlobalOptions.TUTORIAL_MODE) {
			popText ("Alert! Cannons are now moving!");
		}


		if (teleporters != null) {
			TeleporterManager tm = teleporters.GetComponent<TeleporterManager> ();
			tm.generateTeleporters (difficultLevel);
		}

		if (orbs != null) {
			OrbManager om = orbs.GetComponent<OrbManager> ();
			om.generateOrbs (1);
		}

		if (GlobalOptions.TUTORIAL_MODE)
			return;
		
		if (orc != null) {
			OrcChaser oc = orc.GetComponent<OrcChaser> ();
			oc.upgrade (f);
		}

		if (shooters != null) {
			ShooterManager sm = shooters.GetComponent<ShooterManager> ();
			sm.upgrade (f);
		}



	}

	public void popText(string text){
		buffTextTime = INIT_BUFFTEXT_TIME;
		buffText.text = text;
	}

	public void reduceSpeed(){
		ThirdPersonCharacter tpc = GetComponent<ThirdPersonCharacter> ();
		if (tpc.reduceSpeed (upgradeFactor)) {
			popText ("Ouch! Lose Speed");
		} else {
			popText ("Ouch! At Minimum Speed");
		}
	}
}
