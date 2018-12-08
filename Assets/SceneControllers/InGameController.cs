using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour {
    
    public Text scoreText;
    public Text healthText;

    //public SwarmManager swarmManager;
    //public ScoreManager scoreManager;
    public PlayerController player;

    // A very simple way to keep data persistent between scenes is via
    // a static attribute as below. There are other ways whereby statics
    // can be avoided, but involve a bit more complexity (use of persistent
    // objects between scenes).
    public static bool lastGameWon;
    
	void Start ()
    {
        //this.scoreManager.score = 0;

        // Create swarm with parameters adjusted for difficulty
        //swarmManager.stepTime = 2.1f - (GlobalOptions.difficulty * 2.0f);
        //swarmManager.enemyRows = 2 + (int)(5.0f * GlobalOptions.difficulty);
        //swarmManager.GenerateSwarm();
    }

    void Update ()
    {
        // Update score text field
        //this.scoreText.text = "Score: " + this.scoreManager.score;

        // Update health text field (possible extension exercise)
        //this.healthText.text = "Shields: " + player.GetComponent<HealthManager>().GetHealth() + "%";
    }

    // Called when the game should be ended
    // Changes the UI accordingly
    // Note: Currently hooked up to player health manager "zero event"
    public void GameOver()
    {
        InGameController.lastGameWon = false;
        SceneManager.LoadScene("GameEnded");
    }

    public void PlayerWon()
    {
        InGameController.lastGameWon = true;
        SceneManager.LoadScene("GameEnded");
    }
}
