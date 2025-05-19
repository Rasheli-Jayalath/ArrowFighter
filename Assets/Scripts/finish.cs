using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class finish : MonoBehaviour
{
    public static bool finished = false;
    public static bool enemiesNotFinished = false;
    public int enemies;
    void Start()
    {
        enemies = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        if (coll.CompareTag("Player"))
        {
            if(enemies == 0)
            {
                finished = true;
                print("finished");
            }
            else
            {
                enemiesNotFinished = true;
            }
        
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            enemiesNotFinished = false;

        }
    }
}
