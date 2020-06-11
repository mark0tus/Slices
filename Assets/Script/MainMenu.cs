using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 
    public void PlayGame () 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Current Level + 1 (goes to the next level)
        FindObjectOfType<AudioManager>().ButtonClickSound();
    }

    public void QuitGame ()
    {
        Application.Quit(); // Closes the game
    }

}
