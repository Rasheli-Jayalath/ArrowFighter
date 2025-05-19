using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class arrowScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float life = 3.0f;
    bool hit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (hit == false)
        {
            ArrowTracker();
        }

    }

    void ArrowTracker()
    {
        Vector2 direction = rb.velocity;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "ground" || coll.tag == "environment")
        {

            hit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(life);
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(life
            );
        Destroy(this.gameObject);
    }

}
