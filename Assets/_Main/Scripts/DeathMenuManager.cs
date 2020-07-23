using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour {
    
    private AudioSource audioSource;
    public GameObject buttonsHolder;
    
    // Start is called before the first frame update
    void Start() {

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

      
       
    }

    private void Update() {
        if (!audioSource.isPlaying) {
            ShowButtons();
        }
    }


    public void PlayGame() {
        SceneManager.LoadScene("Gameplay");
    }
    
    public void QuitGame() {

        Application.Quit();
    }

    private void ShowButtons() {
        buttonsHolder.SetActive(true);
    }
    
    
}
