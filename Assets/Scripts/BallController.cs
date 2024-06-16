using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //DECLARAÇÕES
    public GameManager gameManager;
    [Space(10)]

    private Rigidbody2D rb;
    private Vector2 startingVelocity = new Vector2(5f, 5f);
    [Space(10)]

    public float speedUp = 1.1f;


    [HideInInspector] public Vector2 finalVelocity = new Vector2(0f,0f);
    public void ResetBall()
    {
        transform.position = Vector3.zero;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = startingVelocity;
    }

    public void EndGameBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = finalVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            rb.velocity *= speedUp;
        }

        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            gameManager.ScorePlayer();
            if (gameManager.enemyScore >= gameManager.winPoints ||
                gameManager.playerScore >= gameManager.winPoints)
            {
                EndGameBall();
            }
            else
            {
                rb.velocity = startingVelocity;
                ResetBall();
            }

        }
        else if (collision.gameObject.CompareTag("PlayerWall"))
        {
            gameManager.ScoreEnemy();
            if (gameManager.enemyScore >= gameManager.winPoints ||
                gameManager.playerScore >= gameManager.winPoints)
            {
                EndGameBall();
            }
            else
            {
                rb.velocity = startingVelocity;
                ResetBall();
            }
        }


    }


}
