using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class LevelManager : MonoBehaviour
{
    public static List<LevelManager> levels = new List<LevelManager>();
    public EndGamePanel endGamePanel;


    PlayerController pc;
    FinishLine fl;
    Texture2D dustTexture;
    PlayerStats stats;
    bool isEnded = false;

    public float FinishTime { get; set; }
    public float CurrentFinishTime { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        fl = FindObjectOfType<FinishLine>();
        stats = FindObjectOfType<PlayerStats>();


        FinishTime = float.PositiveInfinity;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWon() && !isEnded) StartCoroutine(WonLevel());
        if (isLost() && !isEnded) StartCoroutine(LostLevel());
    }

    public bool isWon()
    {
        return fl.isPlayerReached;
    }

    public bool isLost()
    {
        return pc == null;
    }

    IEnumerator WonLevel()
    {
        isEnded = true;
        stats.wins++;
        stats.chicks = FindObjectsOfType<Chick>().Length;
        stats.totalChicks += FindObjectsOfType<Chick>().Length;
        Animator animator = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        animator.enabled = true;
        animator.Play("Win Anim");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        endGamePanel.gameObject.SetActive(true);
        if (CurrentFinishTime < FinishTime) FinishTime = CurrentFinishTime;
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_FinishTime", FinishTime);

        endGamePanel.WinMessage();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("disable_on_end"))
        {
            obj.SetActive(false);
        }
        FindObjectOfType<LineDrawer>().enabled = false;
    }

    IEnumerator LostLevel()
    {
        isEnded = true;
        stats.loses++;
        GameManager.chicks = new List<Chick>();

        yield return new WaitForSeconds(0.5f);

        endGamePanel.gameObject.SetActive(true);
        endGamePanel.LoseMessage();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("disable_on_end"))
        {
            obj.SetActive(false);
        }
        FindObjectOfType<LineDrawer>().enabled = false;
    }

    private void Reset()
    {
        GameManager.chicks = new List<Chick>();
        stats.tries++;
    }
}
