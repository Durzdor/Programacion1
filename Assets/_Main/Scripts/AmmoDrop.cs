using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    [SerializeField] private int ammoGained;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GainAmmo(ammoGained);
            GameManager.Instance.AmmoForceUpdate();
            Destroy(gameObject);
        }
    }
}
