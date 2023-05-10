using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    Rigidbody2D RB;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameData.Instance.IsGameOver)
        {
            RB.velocity = Vector2.zero;
            return;
        };
        RB.velocity = new Vector2(-GameData.Instance.Speed, 0);
    }
}
