using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PreGameUI : MonoBehaviour, IPointerClickHandler
{
    public Text totalChickText, tapText;
    public InGameUI inGameUI;
    public LineDrawer lineDrawer;

    public void OnPointerClick(PointerEventData e)
    {
        if (e.clickCount >= 2)
        {
            StartCoroutine(ActivateLevel());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalChickText.text = PlayerPrefs.GetInt("_totalChicks", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touches.Length > 0)
            if (Input.GetTouch(0).tapCount >= 2)
            {
                
                StartCoroutine(ActivateLevel());
            }
    }

    IEnumerator ActivateLevel()
    {
        inGameUI.gameObject.SetActive(true);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("disable_on_end"))
        {
            obj.SetActive(true);
        }
        Animator animator = Camera.main.GetComponent<Animator>();
        animator.Play("startingPan");
        tapText.enabled = false;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        lineDrawer.enabled = true;
        gameObject.SetActive(false);
    }
}
