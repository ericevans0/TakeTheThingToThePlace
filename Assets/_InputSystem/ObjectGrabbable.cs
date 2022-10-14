using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour {

    [SerializeField] Transform nonphysicsDouble;
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    private void Awake() {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform) {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
//        this.GetComponent<MeshRenderer>().enabled = false;
//        nonphysicsDouble.SetActive(false);
//        nonphysicsDouble.SetParent(objectGrabPointTransform, false);
//        nonphysicsDouble.localPosition = new Vector3(0f, 0f, -0.2f);
//        nonphysicsDouble.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void Drop() {
        this.objectGrabPointTransform = null;
//        this.GetComponent<MeshRenderer>().enabled = true;
//        this.transform.position = nonphysicsDouble.position;
//        nonphysicsDouble.SetActive(false);
        objectRigidbody.useGravity = true;
        nonphysicsDouble.gameObject.SetActive(false);

    }

    private void FixedUpdate() {
        if (objectGrabPointTransform != null) {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }

    private void SwingIntoPlace(Transform objectGrabPointTransform)
    {
        // lerp this into position
    }

    private void Attach(Transform objectGrabPointTransform)
    {
        // make a joint attaching to the grab point or to the player somehow
        // stop lerping the position
        // turn gravity back on
    }

    private void Detach()
    {
        // remove the joint
    }

}