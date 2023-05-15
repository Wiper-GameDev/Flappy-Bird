using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
struct PipeConfiguration
{
    public float min;
    public float max;
}

public class PipesManager : MonoBehaviour
{
    [SerializeField] private GameObject pipesPrefab;
    [SerializeField] private Transform pipesSpawnPoint;


    [SerializeField] PipeConfiguration PipesGroupDist;

    [SerializeField] PipeConfiguration PipesOffset;
    [SerializeField] PipeConfiguration PipeGroupVertOffset;

    private List<GameObject> pipes = new List<GameObject>();

    float pipesWidth;
    new Camera camera;

    void Awake()
    {
        camera = Camera.main;
        pipesWidth = pipesPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;

        // Spawn 3 Pipes
        for (int i = 0; i < 3; i++)
        {
            GameObject pipe = Instantiate(pipesPrefab, ComputeSpawnPosition(), Quaternion.identity, transform);
            SetupPipe(pipe);
            pipes.Add(pipe);
        }
    }

    void Start()
    {
        GameManager.OnGameRestarted.AddListener(ResetAllPipes);
    }

    void OnDestroy()variability
    {
        GameManager.OnGameRestarted.RemoveListener(ResetAllPipes);
    }

    void SetupPipe(GameObject pipe)
    {
        Transform topPipe = pipe.transform.GetChild(0);

        // Pipes Offset
        float pipesOffset = Random.Range(PipesOffset.min, PipesOffset.max);

        topPipe.localPosition = new Vector2(0, pipesOffset);


        // Pipe Group Vertical Offset
        float pipesVertOffset = Random.Range(PipeGroupVertOffset.min, PipeGroupVertOffset.max - pipesOffset * pipe.transform.localScale.x);
        pipe.transform.position = new Vector2(pipe.transform.position.x, pipesVertOffset);
    }

    void ReSpawnPipe(GameObject pipe)
    {
        // Top Pipe doesn't require reset..

        // Pipe Group Vertical Offset Reset
        pipe.transform.position = ComputeSpawnPosition();
        pipes.Remove(pipe);
        pipes.Add(pipe);
    }

    void ResetAllPipes()
    {
        Vector2 spawnPosition = pipesSpawnPoint.position;

        foreach (GameObject pipe in pipes)
        {
            pipe.transform.position = spawnPosition;
            SetupPipe(pipe);
            float dist = Random.Range(PipesGroupDist.min, PipesGroupDist.max);
            spawnPosition += new Vector2(pipesWidth + dist, 0f);
        }
    }

    Vector2 ComputeSpawnPosition()
    {
        if (pipes.Count > 0)
        {
            float dist = Random.Range(PipesGroupDist.min, PipesGroupDist.max);
            return new Vector2(pipes[pipes.Count - 1].transform.position.x + pipesWidth + dist, pipesSpawnPoint.position.y);
        }

        return pipesSpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Return if game is not started, or is over already
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver) return;


        GameObject pipe = pipes[0];
        // Optimized way maybe?
        Vector2 pipePosition = new Vector2(pipe.transform.position.x + pipesWidth / 2, 0);
        Vector2 viewportPipePosition = camera.WorldToViewportPoint(pipePosition);

        if (viewportPipePosition.x >= 0) return;

        ReSpawnPipe(pipe);
        SetupPipe(pipe);
    }
}
