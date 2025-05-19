using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    private Transform player;
    public GameObject projectilePrefab;

    
    public float moveSpeed = 2f;
    public float throwCooldown = 2f;
    public float projectileForce = 5f;
    public bool isThrowing;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector3 nextPoint;
    private bool facingRight = true;
    private bool canThrow;


    private int health;
    public GameObject[] hearts;
    private bool isDie;

    public Vector2 setDirection;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        nextPoint = pointB.position;
        isThrowing = false;
        isDie = false;
        canThrow = true;
        health = hearts.Length;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isDie)
        {
            if (isThrowing)
            {
                rb.velocity = Vector2.zero;
                anim.SetBool("walk", false);

                FacePlayer();

                if (canThrow)
                    StartCoroutine(ThrowRoutine());
            }
            else
            {
                Patrol();
            }

       
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
        for (int j = health; j < hearts.Length; j++)
        {
            hearts[j].SetActive(false);
        }

        Vector2 playerPos = player.position;
        Vector2 enemyPos = transform.position;
        setDirection = playerPos - enemyPos;
        if (setDirection.x < -6 || setDirection.x > 6)
        {
            isThrowing = false;

        }
        else
        {
            isThrowing = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("arrow"))
        {

            if (!isDie)
            {
                anim.SetBool("walk", false);
                anim.SetBool("damage", true);
                StartCoroutine(HealthDown());
                Destroy(coll.gameObject);
                health--;
            }
            if (health == 0)
            {
                anim.SetBool("die", true);
                isDie = true;

                int score = PlayerPrefs.GetInt("score");
                score += PlayerPrefs.GetInt("health") * 25;
                PlayerPrefs.SetInt("score", score);

                StartCoroutine(AfterDie());
            }
        }
    }

    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(1.5f);
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null && !archerScript.isDie)
            {
                // Determine push direction based on enemy and player position
                float pushDirection = collision.transform.position.x > transform.position.x ? 1 : -1;

                int healthArcher = PlayerPrefs.GetInt("health");
                healthArcher--;
                if (healthArcher != 0)
                {
                    playerRb.AddForce(new Vector2(pushDirection * 3000f, 0));
                }
                PlayerPrefs.SetInt("health", healthArcher);
            }
        }
    }

    IEnumerator HealthDown()
    {

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("damage", false);

        int score = PlayerPrefs.GetInt("score");
        score += PlayerPrefs.GetInt("health") * 15;
        PlayerPrefs.SetInt("score", score);
    }
    private void Patrol()
    {
        anim.SetBool("walk", true);

        transform.position = Vector2.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPoint) < 0.1f)
        {
           
            if (nextPoint == pointA.position)
            {
                nextPoint = pointB.position;
                FaceDirection(true);
            }
            else
            {
                nextPoint = pointA.position;
                FaceDirection(false); 
            }
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }

    private void FaceDirection(bool faceRight)
    {
        if (facingRight != faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (facingRight ? 1 : -1);
        transform.localScale = localScale;
    }


    IEnumerator ThrowRoutine()
    {
        canThrow = false;
        anim.SetBool("throw",true);

        yield return new WaitForSeconds(0.3f); // sync with throw animation

        Vector3 spawnPos = transform.position + new Vector3(facingRight ? 1f : -1f, 0.5f, 0);
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        projRb.velocity = new Vector2(facingRight ? 1 : -1, 0) * projectileForce;
        anim.SetBool("throw", false);
        yield return new WaitForSeconds(throwCooldown);
        canThrow = true;

    }
}
