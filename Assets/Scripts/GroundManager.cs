using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    List<GameObject> grounds = new List<GameObject>();
    new Camera camera;
    float groundWidth;


    void Awake()
    {
        GameObject ground = transform.GetChild(0).gameObject;
        groundWidth = ground.GetComponent<SpriteRenderer>().bounds.size.x;
        camera = Camera.main;

        // Add default ground in list
        grounds.Add(ground);

        // Add second ground object
        GameObject newGround = Instantiate(ground, ComputeSpawnPosition(), Quaternion.identity, transform);
        Debug.Log(newGround);
        grounds.Add(newGround);


    }

    Vector3 ComputeSpawnPosition()
    {
        return grounds[grounds.Count - 1].transform.position + new Vector3(groundWidth / 2, 0f);
    }

    void ResetGround(GameObject ground)
    {
        ground.transform.position = ComputeSpawnPosition();
        grounds.Remove(ground);
        grounds.Add(ground);
    }



    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        GameObject ground = grounds[0];
        Vector2 groundPosition = new Vector2(ground.transform.position.x + groundWidth / 2, 0f);

        Vector2 viewportGroundPosition = camera.WorldToViewportPoint(groundPosition);

        if (viewportGroundPosition.x >= 0) return;

        ResetGround(ground);
    }
}
