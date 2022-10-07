using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask grabbableLayerMask;
    private ObjectGrabbable currentlyInHand;
    [SerializeField] private Transform grabbedObjectHoldPoint;
    [SerializeField] private float reachDistance = 4f; // How far can the character reach to interact with something.
    // Camera follow distance (default 4f) should be added to reachDistance to see if it is in reach.
    private float camFollowDistance = 4f; // TODO Should be obtained from the camera, probably.


    private PlayerInput playerInput;
    void Start()
    {
        Debug.Log("ObjectInteraction.Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
            Debug.Log($"playerInput = {playerInput}");
            Debug.Log($"defaultActionMap = {playerInput.defaultActionMap}");
            Debug.Log($"currentActionMap = {playerInput.currentActionMap}");
            Debug.Log($"actions = {playerInput.actions}");
            InputAction grabRelease = playerInput.actions["GrabRelease"];
            Debug.Log($"grabRelease = {grabRelease}");

 //           grabRelease.started += context => Debug.Log($"{context.action} started");
            grabRelease.performed += context => Debug.Log($"{context.action} performed");
            //           grabRelease.canceled += context => Debug.Log($"{context.action} canceled");

            InputAction jump = playerInput.actions["Jump"];
            Debug.Log($"jump = {jump}");
            jump.performed += context => Debug.Log($"{context.action} performed");

        }
    }

    public void OnGrabRelease()
    {
        Debug.Log("ObjectInteraction.OnGrabRelease");

        ObjectGrabbable grabbableInReach = FindGrabbableInReach();
        Debug.Log($"grabbableInReach = {grabbableInReach}. currentlyInHand = {currentlyInHand}");

        ObjectGrabbable objectToDrop = currentlyInHand;

        ObjectGrabbable objectToGrab = (currentlyInHand != grabbableInReach) ? grabbableInReach : null;

        if (objectToDrop != null)
        {
            Debug.Log($"Dropping {objectToDrop}");
            objectToDrop.Drop();
            currentlyInHand = null;
        }

        if (objectToGrab != null)
        {
            Debug.Log($"picking up grabbable = {objectToGrab}");
            objectToGrab.Grab(grabbedObjectHoldPoint);
            currentlyInHand = objectToGrab;
        }
    }

    public ObjectGrabbable FindGrabbableInReach()
    {
        float raycastDistance = camFollowDistance + reachDistance;
        bool hadHit = Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, raycastDistance, grabbableLayerMask);
        Debug.Log($"hadHit = {hadHit}");
        Debug.Log($"raycastHit = {raycastHit}");
        if (!hadHit) return null;
        if (!raycastHit.transform.TryGetComponent<ObjectGrabbable>(out ObjectGrabbable grabbable))
            return null;
        return grabbable;
    }

}
