using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float jumpForce;
   [SerializeField] private float speed;
   [SerializeField] private Rigidbody2D rb2D;
   [SerializeField] private bool isGrounded;
   [SerializeField] private int currentJumps;
   [SerializeField] private Transform groundCheck;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private float checkDistance;
   [SerializeField] private Projectile projectilePrefab;
   [SerializeField] private Transform shootFrom;
   [SerializeField] private bool ammoFull;
   [SerializeField] private Animator animator;
   [SerializeField] private Vector2 lastPositionCheck;
   [SerializeField] private AudioSource jumpSound;
   [SerializeField] private AudioSource footstepSound;
   public int currentAmmo;
   public float ammoRechargeTimer;
   public float ammoRecharging;
   public bool canMove;


   private void Awake()
   {
      rb2D = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      ammoRecharging = ammoRechargeTimer;
      lastPositionCheck = transform.position;
      canMove = true;
   }

   private void Update()
   {
      if (!canMove) return;
      GroundCheck();
      Jump();
      FallAnimationMath();
      Movement();
      Shoot();
      AmmoRecharge();
      //FootstepsAudio();
      if (Input.GetKeyDown(KeyCode.C))
      {
         GameManager.Instance.TakeDamage(10);
      }
   }

   private void Movement()
   {
      if (Input.GetKey(KeyCode.A))
      {
         transform.rotation = Quaternion.Euler(0,180,0);
         rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
      }
      if (Input.GetKey(KeyCode.D))
      {
         transform.rotation = Quaternion.Euler(0,0,0);
         rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
      }

      if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded)
      {
         animator.SetBool("isRunning", true);
      }
      else
      {
         animator.SetBool("isRunning", false);
      }
   }

   private void FootstepsAudio()
   {
      
      if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
      {
         if (!isGrounded) return;
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
         {
           footstepSound.Play(); 
         }
         
      }

      if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
      {
         footstepSound.Stop();
      }
      
   }

   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         if (isGrounded || currentJumps != 0)
         {
           isGrounded = false;
           if (currentJumps <= 0) return;
           currentJumps--;
           footstepSound.mute = true;
           jumpSound.Play();
           animator.SetBool("isJumping", true);
           rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
         }
      } 
   }

   private void Shoot()
   {
      if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
      {
         animator.SetBool("isAttacking", true);
         Invoke(nameof(AttackAnimationDuration),0.1f);
         currentAmmo--;
         GameManager.Instance.AmmoForceUpdate();
         ammoFull = false;
         //Instantiate(projectilePrefab, shootFrom.transform.position, shootFrom.rotation);
      }
   }

   private void GroundCheck()
   {
      isGrounded = false;
      RaycastHit2D hit2D = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, checkDistance, groundLayer);
      if (!hit2D) return;
      isGrounded = true;
      footstepSound.mute = false;
      FootstepsAudio();
      animator.SetBool("isFalling", false);
      animator.SetBool("isJumping", false);
      currentJumps = GameManager.Instance.maxJumps;
   }

   private void AmmoRecharge()
   {
      ammoFull = false;
      if (currentAmmo == GameManager.Instance.maxAmmo)
      {
         ammoFull = true;
      }
      if (ammoFull != false) return;
      ammoRecharging -= Time.deltaTime;
      GameManager.Instance.ReloadBarFillMath();
      if (ammoRecharging > 0) return;
      currentAmmo++;
      GameManager.Instance.AmmoForceUpdate();
      ammoRecharging = ammoRechargeTimer;
   }
   public void Stats(int maxAmmo, int maxJumps)
   {
      currentAmmo = maxAmmo;
      currentJumps = maxJumps;
   }

   private void FallAnimationMath()
   {
      var deltaJump = Mathf.Abs(lastPositionCheck.y) - Mathf.Abs(transform.position.y);
      if (Mathf.Sign(deltaJump) > 0 && !isGrounded)
      {
         animator.SetBool("isFalling", true);
      }
      lastPositionCheck = transform.position;
   }

   private void AttackAnimationDuration()
   {
      animator.SetBool("isAttacking", false);
      Instantiate(projectilePrefab, shootFrom.transform.position, shootFrom.rotation);
   }
   
}
