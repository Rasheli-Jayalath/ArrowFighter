using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{

    public GameObject infoMenu;
    void Start()
    {
        infoMenu.SetActive(false);
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void HomeBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void InfoBtn()
    {
        infoMenu.SetActive(true);
    }
    public void InfoCloseBtn()
    {
        infoMenu.SetActive(false);
    }

    public void Mission(int m)
    {
        SceneManager.LoadScene(m+1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
