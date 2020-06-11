using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public GameObject startPanel, endPanel, skinsPanel, pausedPanel, pauseButton, muteImage, reviveButton;
    public TextMeshProUGUI scoreText, nextSliceText, highScoreText, endScoreText, endHighScoreText;

    public Sprite[] skins;

    [HideInInspector]
    public bool gameIsOver = false;

	void Start () 
    {
        StartPanelActivation();
        HighScoreCheck();

        PlayerPrefs.SetInt("Skin1Unlocked", 1);
    }

    public void Initialize()
    {
        scoreText.enabled = false;
        nextSliceText.enabled = false;
        pauseButton.SetActive(false);
    }

    public void StartPanelActivation()
    {
        Initialize();
        startPanel.SetActive(true);
        endPanel.SetActive(false);
        skinsPanel.SetActive(false);
        pausedPanel.SetActive(false);
    }

    public void EndPanelActivation()
    {
        gameIsOver = true;
        FindObjectOfType<AudioManager>().ErrorSound();
        nextSliceText.enabled = false;
        startPanel.SetActive(false);
        endPanel.SetActive(true);
        skinsPanel.SetActive(false);
        pausedPanel.SetActive(false);
        scoreText.enabled = false;
        endScoreText.text = scoreText.text;
        pauseButton.SetActive(false);
        HighScoreCheck();
    }

    public void SkinsPanelActivation()
    {
        startPanel.SetActive(false);
        skinsPanel.SetActive(true);
        pausedPanel.SetActive(false);
    }

    public void PausedPanelActivation()
    {
        FindObjectOfType<AudioManager>().ButtonClickSound();
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        skinsPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

    public void HighScoreCheck()
    {
        if (FindObjectOfType<Score>().score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", FindObjectOfType<Score>().score);
        }
        highScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        endHighScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }


    public void StartButton()
    {
        gameIsOver = false;
        nextSliceText.enabled = true;
        pauseButton.SetActive(true);
        scoreText.enabled = true;
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        
        FindObjectOfType<MidPlate>().enabled = true;
        FindObjectOfType<AudioManager>().ButtonClickSound();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<AudioManager>().ButtonClickSound();
    }
    public void PauseButton()
    {
        pauseButton.SetActive(false);
        PausedPanelActivation();
        scoreText.enabled = false;
        nextSliceText.enabled = false;
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().ButtonClickSound();
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        scoreText.enabled = true;
        nextSliceText.enabled = true;
        pauseButton.SetActive(true);
        pausedPanel.SetActive(false);
        FindObjectOfType<AudioManager>().ButtonClickSound();
    }

    public void QuitButton()
    {
        FindObjectOfType<AudioManager>().ButtonClickSound();
        Application.Quit(); //quit for Windows users
    }

    public void HomeButton()
    {
        ResumeButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Revive()
    {
        endPanel.SetActive(false);
        reviveButton.SetActive(false);
        pauseButton.SetActive(true);
        scoreText.enabled = true;

        gameIsOver = false;

        for (int i = 0; i < 3; i++)
            GameObject.FindGameObjectsWithTag("Circle")[i].GetComponent<Plate>().ResetSlices(false);
    }
}
