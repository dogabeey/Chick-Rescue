using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int wins, loses, tries, chicks, totalChicks;
    // Start is called before the first frame update
    void Start()
    {
        wins = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_wins", 0);
        loses = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_loses", 0);
        tries = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_tries", 0);
        chicks = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_chicks", 0);
        /*totalChicks = */PlayerPrefs.SetInt("_totalChicks", 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_wins", wins);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_loses", loses);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_tries", tries);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_chicks", chicks);
        PlayerPrefs.SetInt("_totalChicks", totalChicks);
    }
}
