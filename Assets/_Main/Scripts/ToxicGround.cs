using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGround : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    [SerializeField] private float attackCountdown;
    [SerializeField] private int damage;
    private bool recentlyHit;

    private void Awake()
    {
        recentlyHit = false;
        attackInterval = attackCountdown;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamageRefresh();
            if (recentlyHit == false)
            {
                recentlyHit = true;
                GameManager.Instance.TakeDamage(damage);
            }
        }
    }

    private void DamageRefresh()
    {
        attackInterval -= Time.deltaTime;
        if (attackInterval > 0) return;
        attackInterval = attackCountdown;
        recentlyHit = false;
    }
}
