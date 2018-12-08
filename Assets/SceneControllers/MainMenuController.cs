using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    
	void Update(){
		Cursor.visible = true;
	}

    public void StartGame()
    {
		GlobalOptions.TUTORIAL_MODE = false;
		if (GlobalOptions.firstTimeGame) {
			SceneManager.LoadScene ("Instructions");
			GlobalOptions.firstTimeGame = false;
		} else {
			SceneManager.LoadScene ("ColorShifter");
		}
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OpenOptions()
    {
		GlobalOptions.TUTORIAL_MODE = true;
        SceneManager.LoadScene("Options");
    }

	public void clickExit()
	{
		Application.Quit ();
	}

}
