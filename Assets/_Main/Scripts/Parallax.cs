using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 lastCameraPosition;
    [SerializeField] private float width;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, 0f, 0f);
        lastCameraPosition = cameraTransform.position;
        float distanceWithCamera = cameraTransform.position.x - transform.position.x;
        if (Mathf.Abs(distanceWithCamera) >= width)
        {
            float xMovement = distanceWithCamera > 0 ? width * 2f : width * -2f;
            transform.position = new Vector3(transform.position.x + xMovement, transform.position.y, 0f);
        }
    }
}
