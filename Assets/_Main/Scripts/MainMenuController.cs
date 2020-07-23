using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    private Animator helpAnim;
    private AudioSource audioSource;
    

    void Awake() {
        
        helpAnim = GameObject.Find("ControlsHolder").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        audioSource.Play();
    }

    public void PlayGame() {
        SceneManager.LoadScene("Gameplay");

    }

    public void ShowControls() {
        mainMenu.SetActive(false);
        helpAnim.Play("FadeIn");
    }

    public void BackToMenu() {
        mainMenu.SetActive(true);
        helpAnim.Play("FadeOut");
    }

    public void QuitGame() {

        Application.Quit();
    }
}
