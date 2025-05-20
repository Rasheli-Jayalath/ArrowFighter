using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    public Text coinValue;
    public Text arrowValue;
    public Text scoreText;
    public Text scoreTextCompletePanel;
    public GameObject EnemiesText;
    public GameObject ArrowText;
    public int Arrows;
    public GameObject[] hearts;
    private int health;
    private int score;

    private bool isFailed;
    private bool isFinished;

    public GameObject[] panels;  // 0 for mission complete, 1 for mission failed panel

    private void Awake()
    {
        PlayerPrefs.SetInt("arrows", Arrows);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("score", 0);
        finish.finished = false;
        finish.enemiesNotFinished = false;
    }
    void Start()
    {
        

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        isFailed = false;
        isFinished = false;
        EnemiesText.SetActive(false);
        ArrowText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int coinBal = PlayerPrefs.GetInt("coinBal");
        int arrowBal = PlayerPrefs.GetInt("arrows");
        coinValue.text = "x" + coinBal.ToString();
        arrowValue.text = "x" + arrowBal.ToString();
        score = PlayerPrefs.GetInt("score");
        scoreText.text = score.ToString();
        scoreTextCompletePanel.text = "Your Score: "+score.ToString();

        health = PlayerPrefs.GetInt("health");

        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
        for (int j = health; j < hearts.Length; j++)
        {
            hearts[j].SetActive(false);
        }

       

        if (archerScript.isDie)
        {
            isFailed = true;
        }
        if (finish.finished)
        {
            isFinished = true;
        }

        if (isFailed)
        {
            StartCoroutine(Failed());
            
        }
        if (isFinished)
        {
            StartCoroutine(Finish());
        }
        if (finish.enemiesNotFinished)
        {
            EnemiesText.SetActive(true);
        }
        else
        {
            EnemiesText.SetActive(false);
        }

        if(PlayerPrefs.GetInt("arrows") == 0)
        {
            ArrowText.SetActive(true);
        }
        else
        {
            ArrowText.SetActive(false);

        }
    }

    public void HighScore()
    {
        int scoreValue = PlayerPrefs.GetInt("highscore" + (SceneManager.GetActiveScene().buildIndex - 2));
        if (score > scoreValue)
        {
            PlayerPrefs.SetInt("highscore" + (SceneManager.GetActiveScene().buildIndex - 2), score);
        }
    }
    IEnumerator Failed()
    {
        yield return new WaitForSeconds(2f);
        panels[1].SetActive(true);
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.5f);
        panels[0].SetActive(true);
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextMission()
    {
        PlayerPrefs.SetInt("missionnum", SceneManager.GetActiveScene().buildIndex - 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(1);
    }



  
}
