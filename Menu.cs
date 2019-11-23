using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject playButton;
    public GameObject quitButton;

    public bool standard;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void MenuChange()
    {

        if (playButton.transform.GetChild(0).GetComponent<Text>().text == "PLAY")
        {
            playButton.transform.GetChild(0).GetComponent<Text>().text = "STANDARD";
            quitButton.transform.GetChild(0).GetComponent<Text>().text = "PREMIUM";
        }
        else
        {
            standard = true;
            SceneManager.LoadScene("Game");
        }
    }

    public void Quit()
    {
        if(quitButton.transform.GetChild(0).GetComponent<Text>().text == "QUIT")
        {
            Application.Quit();
        }
        else
        {
            standard = false;
            SceneManager.LoadScene("Game");
        }
    }

}
