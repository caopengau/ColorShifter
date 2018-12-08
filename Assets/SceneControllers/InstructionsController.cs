using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour {

	public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNextButtonPressed()
    {
        SceneManager.LoadScene("Instructions2");
    }

    public void OnPlayBottonPressed()
    {
        SceneManager.LoadScene("ColorShifter");
    }

    public void OnBack2BottonPressed()
    {
        SceneManager.LoadScene("Instructions");
    }
}
