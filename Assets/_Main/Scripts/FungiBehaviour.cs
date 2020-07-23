using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungiBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
   [SerializeField] private Transform groundCheck;
   [SerializeField] private Transform wallCheck;
   [SerializeField] private Transform smokeCheck;
   [SerializeField] private float checkDistance;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private int maxHitpoints;
   [SerializeField] private int currentHitpoints;
   [SerializeField] private Animator animator;
   [SerializeField] private int damage;
   [SerializeField] private PlayerController player;
   [SerializeField] private float targetDirection;
   [SerializeField] private SmokeAttack smokePrefab;
   [SerializeField] private float attackInterval;
   [SerializeField] private float attackCountdown;
   private bool isGrounded;
   private bool goingRight = true;
   private Rigidbody2D rb2D;
   public bool canMove;

   private void Awake()
   {
      rb2D = GetComponent<Rigidbody2D>();
      currentHitpoints = maxHitpoints;
      attackInterval = attackCountdown;
      animator = GetComponent<Animator>();
   }
   
   private void Start()
   {
      canMove = true;
      animator.SetBool("isDead", false);
   }

   private void Update()
   {
     GroundCheck(); 
     WallCheck();
     Rotation();
     MovementPattern();
     SmokeAttackTimer();
   }

   private void GroundCheck()
   {
      RaycastHit2D hit2D = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, checkDistance, groundLayer);
      if (!hit2D)
      {
         isGrounded = false;
         goingRight = !goingRight;
      }
      isGrounded = true;
   }

   private void WallCheck()
   {
      
      RaycastHit2D hit2D = Physics2D.Raycast(wallCheck.transform.position, Vector2.right, checkDistance, groundLayer);
      if (hit2D)
      {
        goingRight = !goingRight;
      }
   }

   private void Rotation()
   {
      if (goingRight == false)
      {
        transform.rotation = Quaternion.Euler(0,180,0); 
        return;
      }
      transform.rotation = Quaternion.Euler(0,0,0);
   }

   private void MovementPattern()
   {
      if (canMove != true) return;
      switch (goingRight)
      {
         case true: rb2D.velocity = new Vector2((speed), rb2D.velocity.y);
            break;
         case false: rb2D.velocity = new Vector2((-speed), rb2D.velocity.y);
            break;
      }    
   }

   private void SmokeAttackTimer()
   {
      attackInterval -= Time.deltaTime;
      if (attackInterval > 0) return;
      attackInterval = attackCountdown;
      SmokeAttack();
   }

   private void SmokeAttack()
   {
      Instantiate(smokePrefab, smokeCheck.position, Quaternion.identity);
   }
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         GameManager.Instance.TakeDamage(damage);
      }
   }
   
   public void TakeDamage(int damageTaken)
   {
      currentHitpoints -= damageTaken;
      animator.SetTrigger("isHit");
      if (currentHitpoints > 0) return;
      animator.SetBool("isDead", true);
      canMove = false;
      Invoke(nameof(Death), 1f);
   }
   private void Death()
   {
      Destroy(gameObject);
   }
}
