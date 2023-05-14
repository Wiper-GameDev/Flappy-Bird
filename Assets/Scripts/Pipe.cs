using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    new Collider2D collider;
    Collider2D birdCollider;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        birdCollider = FindObjectOfType<Bird>().GetComponent<Collider2D>();
    }


    void Update()
    {
        Physics2D.IgnoreCollision(collider, birdCollider, GameManager.Instance.IsGameOver);
    }
}
