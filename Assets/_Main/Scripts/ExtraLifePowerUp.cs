using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.currentLives++;
            GameManager.Instance.livesCounter.text = $"Lives Remaining: {GameManager.Instance.currentLives}";
            Destroy(gameObject);
        }
    }
}
