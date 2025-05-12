using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class arrowShoot : MonoBehaviour
{
    public static bool arrowReady = true;
    public static bool availableArrow = true;
    public float arrowForce;
    public GameObject arrow;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if(arrowBal.totalArrow <= 0)
        {
            availableArrow = false;
        }
        else
        {
            availableArrow = true;
        }*/
    }

    public void Shoot()
    {
        arrowReady = false;
        GameObject ArrowIns = Instantiate(arrow, transform.position, transform.rotation);
        ArrowIns.GetComponent<Rigidbody2D>().velocity = transform.right * arrowForce;
        //arrowBal.totalArrow -= 1;
       // PlayerPrefs.SetInt("Arrow", arrowBal.totalArrow);
        StartCoroutine(arrowDelay());
    }
    IEnumerator arrowDelay()
    {
        yield return new WaitForSeconds(0.5f);
        arrowReady = true;
    }
}
