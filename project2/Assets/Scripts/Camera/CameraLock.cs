﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraLock : MonoBehaviour
{
    public GameObject target;
    
    public float distance = 10.0f;
    private const float CameraYAngleMax = 100.0f;
    private const float CameraYAngleMin = 10.0f;
    private const float MinDistance = 15;
    private const float MaxDistance = 50;
    private float mouseX, mouseY;
    
    // How fast the camera distance change
    private float scale = 2;
    // Start is called before the first frame update
    void Start()
    {
        // make cursor invisible
        Cursor.visible = false;
        // lock cursor at the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {   
        // Make the camera look at target
        transform.LookAt(target.transform);
        // Read mouse input
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        // Change camera distance from scroll input
        distance -= Input.mouseScrollDelta.y * scale;
        distance = Mathf.Clamp(distance, MinDistance, MaxDistance);
        // Clamp camera rotation with respect to Y axis
        mouseY = Mathf.Clamp(mouseY, CameraYAngleMin, CameraYAngleMax);
        // Camera transform
        Transform cameraTransform;
        // Apply change to camera rotation.
        (cameraTransform = transform).rotation = Quaternion.Euler(mouseY, mouseX, 0);
        // Apply change to camera position.
        cameraTransform.position = target.transform.position - cameraTransform.forward * distance;
    }
}
