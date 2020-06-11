using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioSource buttonClickSound, scoreSound, errorSound;
    public bool soundIsOn = true;

    /*public void Update()
    {
        if(Input.GetMouseButtonDown(0))
            if (soundIsOn)
                buttonClickSound.Play();    //maybe too much noise 
    }*/
    public void ScoreSound()
    {
        if (soundIsOn)
            scoreSound.Play();
    }
    public void ButtonClickSound()
    {
        if (soundIsOn)
            buttonClickSound.Play();
    }
    public void ErrorSound()
    {
        if (soundIsOn)
            errorSound.Play();
    }
}
