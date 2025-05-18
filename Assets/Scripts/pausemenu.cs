using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class pausemenu : MonoBehaviour
{
  
    public bool isPaused;

    public GameObject menu;


    private void Start()
    {
        isPaused = false;
        resume();
    }
    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }
    public void pause()
    {
        //music.instans.Click();
        isPaused = true;
        menu.SetActive(true);
        Time.timeScale = 0;
        
    }
    public  void resume() 
    {
       // music.instans.Click();
        isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1;
       
    }

    
}
