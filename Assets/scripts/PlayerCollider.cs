using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private Explodable _explodable;

    public bool hasFinished = false;

    void Start()
    {
        _explodable = GetComponent<Explodable>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !hasFinished)
        {
            Debug.Log("Game Over");
            Explode();



            var gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();


        }

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            var gameManager = FindObjectOfType<GameManager>();
            gameManager.Finish();
            hasFinished = true;
        }

    }

    public void Explode()
    {
        _explodable.explode();
        ExplosionForce ef = FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
    }
}


