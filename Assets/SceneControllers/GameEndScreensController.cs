using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndScreensController : MonoBehaviour {

    public Text resultText;
	public Text highestScoreText;

	private string result = "";
	

    void Start()
    {
		if (GlobalOptions.gameScore >= GlobalOptions.highestScore) {
			GlobalOptions.highestScore = GlobalOptions.gameScore;
			highestScoreText.text = "New Highest Record: " + GlobalOptions.highestScore;
		} else {
			highestScoreText.text = "Highest Score: " + GlobalOptions.highestScore;
		}
		
		result += "Time Survived: " + GlobalOptions.gameTime + "\n";
		result += "Total Bonus  : " + GlobalOptions.gameBalls + "\n";
		result += "Total Score  : " + GlobalOptions.gameScore + "\n";
		result += "Press ESC to continue";
		resultText.text = result;

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    /*
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");

    }*/


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
            Cursor.visible = true;
        }
    }

}
