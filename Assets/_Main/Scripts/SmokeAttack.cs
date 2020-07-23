using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAttack : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    [SerializeField] private float attackCountdown;
    [SerializeField] private int damage;
    [SerializeField] private float duration;
    private bool recentlyHit;
    

    private void Awake()
    {
        Invoke(nameof(Death), duration);
        recentlyHit = false;
        attackInterval = attackCountdown;
    }

    private void Death()
    {
        Destroy(gameObject);
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
