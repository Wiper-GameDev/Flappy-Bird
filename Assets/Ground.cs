using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Rigidbody2D RB;

    void Awake(){
        RB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameData.Instance.IsGameOver) return;
        var target = GameData.Instance.Speed;
        RB.velocity = new Vector2(-target, 0);
    }
}
