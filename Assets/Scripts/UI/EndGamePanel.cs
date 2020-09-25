using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public Text endMessage;
    public Button cont;
    Color starColor = Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinMessage()
    {
        string levelName = SceneManager.GetActiveScene().name;
        string[] levelNames = levelName.Split('-');
        int currentLevel = Convert.ToInt32(levelNames[1]);
        currentLevel++;
        string level = levelNames[0] + "-" + currentLevel.ToString();
        cont.onClick.AddListener(delegate { SceneManager.LoadScene(levelNames[0] + "-" + currentLevel.ToString()); });

        endMessage.text = "LEVEL PASS!";

    }

    public void LoseMessage()
    {
        endMessage.text = "TRY AGAIN...";
        cont.interactable = false;
    }
}
