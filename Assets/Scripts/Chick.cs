using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{

    public GameObject followingObject;
    public float followDistance;

    PlayerController pc;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Follow(followingObject);
    }

    void Follow(GameObject go)
    {
        if (followingObject != null && pc != null)
        {
            if (Vector3.Distance(transform.position, go.transform.position) > followDistance)
            {
                transform.LookAt(go.transform.position);
                rb.velocity = (go.transform.position - transform.position).normalized * pc.runSpeed;
            }
            else rb.velocity = Vector3.zero;
        }
    }
}
