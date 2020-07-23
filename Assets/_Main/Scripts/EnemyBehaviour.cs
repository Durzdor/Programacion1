using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private int maxHitpoints;
    [SerializeField] private int currentHitpoints;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private BlobBehaviour blob;
    

    private void Awake()
    {
        currentHitpoints = maxHitpoints;
        animator = GetComponent<Animator>();
        blob = GetComponent<BlobBehaviour>();
    }

    private void Start()
    {
        blob.canMove = true;
        animator.SetBool("isDead", false);
    }


    public void TakeDamage(int damageTaken)
    {
        currentHitpoints -= damageTaken;
        animator.SetTrigger("isHit");
        if (currentHitpoints > 0) return;
        animator.SetBool("isDead", true);
        blob.canMove = false;
        Invoke(nameof(Death), 1f);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
