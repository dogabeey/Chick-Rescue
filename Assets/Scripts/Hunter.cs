using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hunter : MonoBehaviour
{
    public float huntSpeed = 5;
    public FieldOfView fieldOfView;
    public LayerMask preyMask;

    Rigidbody rb;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (fieldOfView == null)
        {
            Debug.LogWarning("DEVELOPER'S NOTE: No Field of View component found in Hunter behaviour of " + gameObject + ". Trying to add first one found in children.");
            fieldOfView = GetComponentInChildren<FieldOfView>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hunt(GameObject go)
    {
        transform.LookAt(go.transform.position);
        rb.velocity = (go.transform.position - transform.position).normalized * huntSpeed;
        if (GetComponent<RotationCurve>() != null) GetComponent<RotationCurve>().enabled = false; 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(preyMask == (preyMask | (1 << collision.gameObject.layer)))
        {

            rb.isKinematic = true;
            //if (GetComponent<RotationCurve>() != null) GetComponent<RotationCurve>().enabled = true;
        }
    }

    internal void HandleHunting(RaycastHit hit)
    {

        Hunt(hit.collider.gameObject);
    }

    internal bool isHuntable(RaycastHit hit)
    {
        return preyMask == (preyMask | (1 << hit.collider.gameObject.layer));
    }
}
