using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHitPoints;
    [SerializeField] private int currentHitPoints;
    

    private void Awake()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
         
    }

    private void ShootProjectile()
    {
           
    }

    public void TakeDamage(int damageTaken)
    {
        currentHitPoints -= damageTaken;
        if (currentHitPoints > 0) return;
        Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
