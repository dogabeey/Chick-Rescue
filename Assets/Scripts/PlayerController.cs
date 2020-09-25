using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 1, runSpeed = 2;
    public BoxCollider finishLine;
    [HideInInspector]public bool isFinished;

    Rigidbody rb;
    Collision col;
    LineDrawer ld;
    int index = 0;
    private float distanceThreshold = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ld = FindObjectOfType<LineDrawer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(index < ld.path.Count)
        {
            GoToPoint(ld.path[index], Input.GetMouseButton(0) ? walkSpeed : runSpeed);
            if ((ld.path[index] - transform.position).magnitude < distanceThreshold)
            {
                index++;
            }
        }
        else
        {
            GoToPoint(new Vector3(finishLine.transform.position.x,transform.position.y,finishLine.transform.position.z), Input.GetMouseButton(0) ? walkSpeed : runSpeed);
        }

        if (new Vector3(finishLine.transform.position.x, transform.position.y, finishLine.transform.position.z) == transform.position) isFinished = true;
    }

    void GoToPoint(Vector3 point, float speed)
    {
        transform.LookAt(point);
        rb.velocity = (point - transform.position).normalized * speed;
    }

    private void OnDestroy()
    {
        
    }

}
