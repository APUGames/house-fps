using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] GameObject cameraRoot;
    [SerializeField] CharacterController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2.0f;
    [SerializeField] float zoomInSensitivity = 0.5f;

    bool zoomedInToggle = false;
    float originalSensitivity;

    private Vector2 mouseLook;

    private void OnEnable()
    {
        originalSensitivity = Mouse.current.delta.ReadValue().magnitude;
    }

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void LateUpdate()
    {
        if (zoomedInToggle)
        {
            // Get the mouse movement from InputSystem
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            mouseLook += mouseDelta * (zoomedInToggle ? zoomInSensitivity : zoomOutSensitivity) * originalSensitivity;

            // Rotate the camera children based on the mouse movement
            foreach (Transform child in cameraRoot.transform)
            {
                child.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
                child.transform.localRotation *= Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            }
        }
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        fpsCamera.fieldOfView = zoomedInFOV;
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
    }
}