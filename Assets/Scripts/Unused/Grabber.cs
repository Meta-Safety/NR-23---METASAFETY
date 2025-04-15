using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
   /* public Transform grabTransform;
    Rigidbody rigidBody;
    public float grabSpeed = 1f;
    private bool isGrabbing = false;

    // Start is called before the first frame update

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        isGrabbing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Grab()
    {

        this.transform.SetParent(grabTransform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        isGrabbing = true;
    }

    public void Release()
    {
        // Remove o objeto do grabTransform, tornando-o independente novamente
        isGrabbing = false;
        //this.transform.SetParent(null);
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
    }*/
}
