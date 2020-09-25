using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] Chick chick;
    Animator animator;

    private PlayerController pc;
    private bool selected = true;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!selected && Physics.Raycast(ray, out hit, LayerMask.GetMask("Egg")))
        {
            selected = true;
            animator.Play("DrawUpon");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<PlayerController>()) Hatch(collision);
    }

    private void Hatch(Collision col)
    {
        Chick c;
        GameManager.chicks.Add(c = Instantiate(chick, pc.transform.position, pc.transform.rotation));
        if (GameManager.chicks.Count == 1)
        {
            c.followingObject = col.gameObject;
        }
        else {
            c.followingObject = GameManager.chicks[GameManager.chicks.IndexOf(c)-1].gameObject;
        }

        chick.followingObject = (GameManager.chicks.Count == 1 ? FindObjectOfType<PlayerController>().gameObject : GameManager.chicks.Last().gameObject);
        Destroy(gameObject);
    }
}
