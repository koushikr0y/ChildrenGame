using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject generationPoint;
    private int numberOfObstaclesSpawned;
    [SerializeField] private int totalObstaclesToSpawn = 10;
    private bool isGameOver = false;
    Vector3 pos = new(30, -3, 0);

    [SerializeField] private float timeBetweenObstaclesSpawn = 5f;

    void Start()
    {
        //generationPoint = GameObject.FindGameObjectWithTag("ObstaclesGenerationPos");
        InvokeRepeating("SpawnObstacles", 7f, timeBetweenObstaclesSpawn);
        numberOfObstaclesSpawned = 0;
    }

    private void SpawnObstacles()
    {
        if (!isGameOver && GameManager.Instance.gameState == GameManager.GameState.GAMERUNNING)
        {
            //Vector3 position = new Vector3(generationPoint.transform.position.x, generationPoint.transform.position.y, 0);
            if (numberOfObstaclesSpawned < totalObstaclesToSpawn)
            {
                //Instantiate(obstacles[0], generationPoint.transform.position,Quaternion.identity);

                Instantiate(obstacles[0], pos,Quaternion.identity);
                numberOfObstaclesSpawned++;
                pos = new Vector3(pos.x + 40f, pos.y, pos.z);
            }
            else
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;
        GameManager.Instance.GameOverAction?.Invoke();
    }

}
