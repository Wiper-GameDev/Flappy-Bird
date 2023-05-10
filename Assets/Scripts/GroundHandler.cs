using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [SerializeField] GameObject ground;
    SpriteRenderer groundSprite;
    [SerializeField] List<GameObject> groundObjects = new List<GameObject>();
    Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        groundSprite = ground.GetComponent<SpriteRenderer>();
    }

    Vector2 ComputeSpawnPosition()
    {
        var lastObject = groundObjects[groundObjects.Count - 1];
        return new Vector2(lastObject.transform.position.x + groundSprite.bounds.size.x, lastObject.transform.position.y);
    }

    // Update is called once per frame

    void SpawnGround(Vector2 position)
    {
        var go = Instantiate(ground, position, Quaternion.identity, gameObject.transform);
        groundObjects.Add(go);
    }


    void Update()
    {
        var groundObject = groundObjects[0];
        var pos = new Vector2(groundObject.transform.position.x + groundSprite.bounds.size.x / 2, groundObject.transform.position.y);
        var viewportPoint = mainCamera.WorldToViewportPoint(pos);

        // Out of screen
        if (viewportPoint.x < 0)
        {
            Destroy(groundObject);
            groundObjects.RemoveAt(0);
            SpawnGround(ComputeSpawnPosition());
        }
    }
}
