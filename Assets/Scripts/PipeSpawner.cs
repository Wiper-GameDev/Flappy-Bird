using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] float minPipeOffsetVertical;
    [SerializeField] float maxPipeOffsetVertical;
    [SerializeField] float maxPipeGroupOffsetPossibleTop;
    [SerializeField] float maxPipeGroupOffsetPossibleBottom;
    [SerializeField] GameObject pipeGroupPrefab;
    [SerializeField] Transform pipeSpawnPosition;
    List<GameObject> pipes = new List<GameObject>();
    SpriteRenderer pipeSprite;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        pipeSprite = pipeGroupPrefab.GetComponentInChildren<SpriteRenderer>();

        // Spawn at least 3 pipes
        SpawnRandomPipe(pipeSpawnPosition.position);
        SpawnRandomPipe(ComputeSpawnPosition());
        SpawnRandomPipe(ComputeSpawnPosition());
    }

    Vector2 ComputeSpawnPosition(){
        var lastPipe = pipes[pipes.Count - 1];
        var randomX = Random.Range(4.5f, 6f);
        return new Vector2(lastPipe.transform.position.x + randomX, pipeSpawnPosition.position.y);
    }

    void SpawnRandomPipe(Vector2 position)
    {
        // Doesn't actually spawn randomly as of now
        GameObject pipe = Instantiate(pipeGroupPrefab, position, Quaternion.identity, transform);
        var topPipe = pipe.transform.GetChild(0);
        var bottomPipe = pipe.transform.GetChild(1);
        var maxOffsetRange = Random.Range(minPipeOffsetVertical, maxPipeOffsetVertical);
        var randomOffsetTop = Random.Range(0, maxOffsetRange);
        var randomOffsetBottom = maxOffsetRange - randomOffsetTop;

        // Applying Offset
        topPipe.transform.position = new Vector2(topPipe.transform.position.x, topPipe.transform.position.y + randomOffsetTop);
        bottomPipe.transform.position = new Vector2(bottomPipe.transform.position.x, bottomPipe.transform.position.y - randomOffsetBottom);

        // Apply Offset to pipe group
        var randomPipeGroupOffset = Random.Range(-(maxPipeGroupOffsetPossibleBottom - randomOffsetBottom), maxPipeGroupOffsetPossibleTop - randomOffsetTop);
        pipe.transform.position = new Vector2(pipe.transform.position.x, pipe.transform.position.y + randomPipeGroupOffset);

        pipes.Add(pipe);
    }

    // Update is called once per frame
    void Update()
    {
        var pipe = pipes[0];
        var pos = new Vector2(pipe.transform.position.x + pipeSprite.bounds.size.x / 2, pipe.transform.position.y);
        var viewportPoint = mainCamera.WorldToViewportPoint(pos);

        // Out of screen
        if (viewportPoint.x < 0)
        {
            Destroy(pipe);
            pipes.RemoveAt(0);
            SpawnRandomPipe(ComputeSpawnPosition());
        }
    }
}
