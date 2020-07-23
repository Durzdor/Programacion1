using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Pathfinding;
using UnityEngine;

public class BatBehaviour : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private int maxHitpoints;
    [SerializeField] private int currentHitpoints;
    private Rigidbody2D rb;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHitpoints = maxHitpoints;
    }

    void Start() {
        MovementOff();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHitpoints -= damageTaken;
        animator.SetTrigger("Hit");
        if (currentHitpoints > 0) return;
        animator.SetTrigger("Death");
        MovementOff();
        Invoke(nameof(DestroyGameObject), 1f);
    }

    public void MovementOff() {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    public void MovementOn() {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void DestroyGameObject() {
        Destroy(this.gameObject);
    }
}
