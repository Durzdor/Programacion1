using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraJumpPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.maxJumps++;
            GameManager.Instance.jumpsCounter.text = $"Max Jumps: {GameManager.Instance.maxJumps}";
            Destroy(gameObject);
        }
    }
}
