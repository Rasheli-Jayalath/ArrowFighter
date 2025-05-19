using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class musicButtons : MonoBehaviour
{
    public static musicButtons instance;
    public Image Img;
    public Sprite m_ON;
    public Sprite m_OFF;
    public int sound;
    void Start()
    {
        //PlayerPrefs.SetInt("mute", 1);
        sound = PlayerPrefs.GetInt("mute");

        
  
        if (AudioListener.volume == 0)
        {
            Img.sprite = m_OFF;
            //music.instans.soundOFF();
        }
        else
        {
            Img.sprite = m_ON;
            //music.instans.soundON();
        }

        if (sound == 1)
        {
            AudioListener.volume = 1;
            Img.sprite = m_ON;

        }
        if (sound == 0)
        {
            AudioListener.volume = 0;
            Img.sprite = m_OFF;

        }
    }

    public void SoundON()
    {
        if (AudioListener.volume == 0)
        {
            PlayerPrefs.SetInt("mute", 1);
            AudioListener.volume = 1;
            Img.sprite = m_ON;
            //music.instans.soundON();
        }
        else
        {
            PlayerPrefs.SetInt("mute", 0);
            AudioListener.volume = 0;
            Img.sprite = m_OFF;
            //music.instans.soundOFF();
        }

    }
    void Update()
    {
        
    }
}
