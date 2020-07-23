using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBehaviour : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private Transform groundCheck;
   [SerializeField] private Transform wallCheck;
   [SerializeField] private float checkDistance;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private int maxHitpoints;
   [SerializeField] private int currentHitpoints;
   [SerializeField] private Animator animator;
   [SerializeField] private int damage;
   private bool isGrounded;
   private bool goingRight = true;
   private Rigidbody2D rb2D;
   public bool canMove;

   private void Awake()
   {
      rb2D = GetComponent<Rigidbody2D>();
      currentHitpoints = maxHitpoints;
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
