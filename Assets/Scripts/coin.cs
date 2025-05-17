using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    public GameObject coinEffect;
    
    public AudioClip coinSounds;

    IEnumerator OnTriggerEnter2D(Collider2D coin)
    {
        if (coin.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinSounds, transform.position);
           
            Instantiate(coinEffect, transform.position, Quaternion.identity);
            int coinBal = PlayerPrefs.GetInt("coinBal");
            coinBal++;
            PlayerPrefs.SetInt("coinBal", coinBal);
            Destroy(this.gameObject);

            int score = PlayerPrefs.GetInt("score");
            score += PlayerPrefs.GetInt("health") * 5;
            PlayerPrefs.SetInt("score", score);
            yield return new WaitForSeconds(1f);
            
            
            
        }

    }
}
