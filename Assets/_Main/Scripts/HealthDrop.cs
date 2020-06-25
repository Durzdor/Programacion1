using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [SerializeField] private int healthGained;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GainHitPoints(healthGained);
            GameManager.Instance.HealthBarFillMath();
            Destroy(gameObject);
        }
    }
}
