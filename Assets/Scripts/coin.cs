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
            //coinBal.totalCoins += 1;
            int coinBal = PlayerPrefs.GetInt("coinBal");
            coinBal++;
            PlayerPrefs.SetInt("coinBal", coinBal);
            Destroy(this.gameObject);
  
            yield return new WaitForSeconds(1f);
            
            
            
        }

    }
}
