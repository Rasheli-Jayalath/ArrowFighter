using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class missionUnlock : MonoBehaviour
{

    public GameObject[] locks;
    public Text[] scores;
    public Button[] UnlockBtns;
    public GameObject[] Btns;
    public Button[] missions;
    public Text coinText;
    public int unlocked;
    public int missionNum;
    private int coinBal;
    private void Awake()
    {
        missionNum = PlayerPrefs.GetInt("missionnum");
        unlocked = PlayerPrefs.GetInt("unlocked");
        coinBal = PlayerPrefs.GetInt("coinBal");
        coinText.text = coinBal.ToString();
    }
    void Start()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            if (i <= missionNum)
            {
                if(i == 0)
                {
                    Btns[i].SetActive(false);
                }

                scores[i].text = "score: " + PlayerPrefs.GetInt("highscore" + i).ToString();
                UnlockBtns[i].interactable = true;
            }
            else
            {

                scores[i].text = "";
                UnlockBtns[i].interactable = false;

            }
            if (i <= unlocked)
            {
                locks[i].SetActive(false);
                missions[i].interactable = true;
                Btns[i].SetActive(false);
            }
            else
            {
                missions[i].interactable = false;
                locks[i].SetActive(true);
                Btns[i].SetActive(true);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        unlocked = PlayerPrefs.GetInt("unlocked");
        coinText.text = coinBal.ToString();

        for (int i = 0; i < locks.Length; i++)
        {
            if (i <= missionNum)
            {
                UnlockBtns[i].interactable = true;
                scores[i].text = "score: "+ PlayerPrefs.GetInt("highscore" + i).ToString();
            }
            else
            {
                UnlockBtns[i].interactable = false;
            }
            if (i <= unlocked)
            {
                locks[i].SetActive(false);
                missions[i].interactable = true;
                Btns[i].SetActive(false);
            }
            else
            {
                missions[i].interactable = false;
                locks[i].SetActive(true);
                Btns[i].SetActive(true);
            }

        }

  
    }

    public void Unlock(int missionNum)
    {
        if(coinBal>= 25 * missionNum)
        {
            unlocked = missionNum;
            PlayerPrefs.SetInt("unlocked", unlocked);
            coinBal -= 25 * missionNum;
            PlayerPrefs.SetInt("coinBal", coinBal);
        }

    }
 
}
