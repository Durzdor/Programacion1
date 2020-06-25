using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAmmoPowerUp : MonoBehaviour
{
    [SerializeField] private int extraAmmo;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.maxAmmo += extraAmmo;
            GameManager.Instance.AmmoForceUpdate();
            Destroy(gameObject);
        }
    }
}
