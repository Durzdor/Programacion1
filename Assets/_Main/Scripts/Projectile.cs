using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float throwStrength;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Invoke(nameof(Kill), lifeTime);
    }

    private void Start()
    {
        rb2D.AddForce(new Vector2(transform.right.x * throwStrength,0.5f), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb2D.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Blob"))
        {
            other.gameObject.GetComponent<BlobBehaviour>().TakeDamage(damage);
            Kill();
        }

        if (other.gameObject.CompareTag("Fungi"))
        {
            other.gameObject.GetComponent<FungiBehaviour>().TakeDamage(damage);
            Kill();
        }
        if (other.gameObject.CompareTag("Bat"))
        {
            other.gameObject.GetComponent<BatBehaviour>().TakeDamage(damage);
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

}
