using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGroup : MonoBehaviour{ 
    Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Camera.main);
    }

    void FixedUpdate(){
        var target = GameData.Instance.Speed;

        if (RB.velocity.x < 0){
            target += RB.velocity.x;
        }
        RB.AddForce(Vector2.left * target);
    }
}
