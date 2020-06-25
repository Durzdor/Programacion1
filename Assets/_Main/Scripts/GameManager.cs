using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool dontDestroyOnLoad;
    public static GameManager Instance;
    public int maxLives;
    public float maxHitPoints;
    public int currentLives;
    public float currentHitPoints;
    public int maxJumps;
    public int maxAmmo;
    [SerializeField] private PlayerController myPlayerController;
    public TextMeshProUGUI livesCounter;
    public TextMeshProUGUI jumpsCounter;
    [SerializeField] private Image healthBarFill;
    public TextMeshProUGUI ammoCounter;
    [SerializeField] private Image reloadBarFill;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource hitSound;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        LoadStats();
    }

    private void LoadStats()
    {
        currentLives = maxLives;
        currentHitPoints = maxHitPoints;
        myPlayerController.Stats(maxAmmo, maxJumps);
        livesCounter.text = $"Lives Remaining: {currentLives}";
        jumpsCounter.text = $"Max Jumps: {maxJumps}";
        AmmoForceUpdate();

    }
    
    public void TakeDamage(int damageTaken)
    {
        currentHitPoints -= damageTaken;
        myPlayerController.GetComponent<Animator>().SetBool("isHit", true);
        hitSound.Play();
        Invoke(nameof(HitAnimationDuration),0.2f);
        HealthBarFillMath();
        if (currentHitPoints > 0) return;
        currentLives--;
        myPlayerController.canMove = false;
        livesCounter.text = $"Lives Remaining: {currentLives}";
        currentHitPoints = maxHitPoints;
        myPlayerController.GetComponent<Animator>().SetBool("isDead", true);
        deathSound.Play();
        Invoke(nameof(Respawn),deathSound.clip.length);
        if (currentLives > 0) return;
        GameOver();
    }

    private void HitAnimationDuration()
    {
        myPlayerController.GetComponent<Animator>().SetBool("isHit", false);
    }

    private void Respawn()
    {
        myPlayerController.transform.position = new Vector3(-21,-3,0);
        HealthBarFillMath();
        myPlayerController.GetComponent<Animator>().SetBool("isDead", false);
        myPlayerController.canMove = true;
    }

    public void GainHitPoints(int healthGained)
    {
        if (currentHitPoints < maxHitPoints)
        {
            currentHitPoints += healthGained;
            if (currentHitPoints < maxHitPoints) return;
            currentHitPoints = maxHitPoints;
        }  
    }

    public void GainAmmo(int ammoGained)
    {
        if (myPlayerController.currentAmmo < maxAmmo)
        {
            myPlayerController.currentAmmo += ammoGained;
            if (myPlayerController.currentAmmo < maxAmmo) return;
            myPlayerController.currentAmmo = maxAmmo;
        }
    }
    
    private void GameOver()
    {
        if (Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        }
        // Debug.Log("Game Over");
        // Time.timeScale = 0.0f;
    }

    public void WinGame()
    {
        if (Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        }
        // Debug.Log("You Win");
        // Time.timeScale = 0.0f;
    }

    public void HealthBarFillMath()
    {
        healthBarFill.fillAmount = currentHitPoints / maxHitPoints;
    }

    public void ReloadBarFillMath()
    {
        reloadBarFill.fillAmount = myPlayerController.ammoRecharging / myPlayerController.ammoRechargeTimer;
    }

    public void AmmoForceUpdate()
    {
        ammoCounter.text = $"Ammo {myPlayerController.currentAmmo}/{maxAmmo}";
    }
}
