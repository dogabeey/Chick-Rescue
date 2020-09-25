using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    [SerializeField] LayerMask hunterMask;
    [SerializeField] ParticleSystem deathParticle;

    private Type ht;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hunterMask == (hunterMask | (1 << collision.gameObject.layer)))
        {
            Kill();
        }
    }

    private void Kill()
    {
        Instantiate(deathParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
