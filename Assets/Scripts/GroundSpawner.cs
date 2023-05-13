using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;

    [SerializeField] private List<GameObject> groundObjects = new List<GameObject>();
    private Camera mainCamera;
    private float groundWidth;
    private void Awake()
    {
        mainCamera = Camera.main;
        var size = groundPrefab.GetComponent<SpriteRenderer>().bounds.size;
        groundWidth = size.x;
    }

    private void SpawnGround(float positionX)
    {
        var firstGroundObject = groundObjects[0];
        firstGroundObject.transform.position = new Vector2(positionX, firstGroundObject.transform.position.y);
        groundObjects.RemoveAt(0);
        groundObjects.Add(firstGroundObject);
    }

    private float ComputeSpawnPositionX()
    {
        var lastObject = groundObjects[groundObjects.Count - 1];
        return lastObject.transform.position.x + groundWidth;
    }

    private void Update()
    {
        var firstGroundObject = groundObjects[0];
        var firstGroundPosition = new Vector2(firstGroundObject.transform.position.x + groundWidth / 2, firstGroundObject.transform.position.y);
        var viewportPoint = mainCamera.WorldToViewportPoint(firstGroundPosition);

        if (viewportPoint.x < 0)
        {
            SpawnGround(ComputeSpawnPositionX());
        }
    }
}
