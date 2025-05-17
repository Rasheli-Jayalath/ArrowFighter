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
    public int Arrows;
    public GameObject[] hearts;
    private int health;
    private int score;

    public GameObject[] panels;

    void Start()
    {
        PlayerPrefs.SetInt("arrows",Arrows);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("score", 0);

        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int coinBal = PlayerPrefs.GetInt("coinBal");
        int arrowBal = PlayerPrefs.GetInt("arrows");
        coinValue.text = "x"+coinBal.ToString();
        arrowValue.text = "x"+ arrowBal.ToString();
        scoreText.text = PlayerPrefs.GetInt("score").ToString();


        health = PlayerPrefs.GetInt("health");

        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
        for (int j = health; j < hearts.Length; j++)
        {
            hearts[j].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            BuyArrow();
        }
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(1);
    }

    public void BuyArrow()
    {
        int arrowBal = PlayerPrefs.GetInt("arrows");
        int coinBal = PlayerPrefs.GetInt("coinBal");
        arrowBal += 2;
        coinBal--;
        PlayerPrefs.SetInt("arrows", arrowBal);
        PlayerPrefs.SetInt("coinBal", coinBal);
    }
}
