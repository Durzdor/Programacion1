using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenuManager : MonoBehaviour
{
    
    private AudioSource audioSource;
    void Start() {

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    
    public void PlayGame() {
        SceneManager.LoadScene("Gameplay");
    }
    
    public void QuitGame() {
        
        Application.Quit();
    }
}
