using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {



    public void Start()
    {
        // Important: Make sure slider corresponds to underlying value
        // if it has been changed with scene invocations in-between
		GlobalOptions.TUTORIAL_MODE = true;
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
