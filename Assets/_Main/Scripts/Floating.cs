using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
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
          transform.position = tempPosition + new Vector2(0f, Mathf.Sin(speed * Time.time)) * amplitude;
     }
}
