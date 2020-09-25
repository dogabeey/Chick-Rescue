using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUI : MonoBehaviour, IDragHandler
{
    public Text levelText, chickText, swipeText;

    LevelManager lm;

    public void OnDrag(PointerEventData e)
    {
        swipeText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
        levelText.text = SceneManager.GetActiveScene().name.Replace('-',' ');
    }

    // Update is called once per frame
    void Update()
    {
        chickText.text = GameManager.chicks.Count.ToString();
        if (lm.isWon()) gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(Input.touches.Length > 0)
        if(Input.GetTouch(0).deltaPosition.magnitude > 0.01f) swipeText.gameObject.SetActive(false);
    }
}
