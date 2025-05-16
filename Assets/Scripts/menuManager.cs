using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    int coinBal;
    public Text coinValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinBal = PlayerPrefs.GetInt("coinBal");
        coinValue.text = "x"+coinBal.ToString();
    }
}
