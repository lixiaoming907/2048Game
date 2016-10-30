using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    //Application.targetFrameRate = 30;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
