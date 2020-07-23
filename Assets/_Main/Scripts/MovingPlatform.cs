using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
    [SerializeField] private Vector2 tempPosition;
    
    private void Start()
    {
        tempPosition = transform.position;
    }

    private void Update()
    {
        transform.position = tempPosition + new Vector2(Mathf.Sin(speed * Time.time), 0f) * amplitude;
    }
}

