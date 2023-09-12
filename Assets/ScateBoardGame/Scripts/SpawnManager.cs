using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnSpeed = 5f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float despawnXPosition = -10f;

    [SerializeField] private List<GameObject> objectPool = new List<GameObject>();
    private int poolIndex = 0;
    private bool hasInitializedPool = false;

    private int objectsSpawned = 0; 
    private int objectsToSpawn = 10; 

    public void StartSpawning()
    {
        InitializeObjectPool();
        StartCoroutine(SpawnObjects());
    }

    private void InitializeObjectPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newObj = Instantiate(objectPrefab);
            newObj.SetActive(false);
            objectPool.Add(newObj);
        }
        hasInitializedPool = true;
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (objectsSpawned < objectsToSpawn)
            {
                GameObject spawnedObject = GetPooledObject();
                if (spawnedObject != null)
                {
                    spawnedObject.transform.position = spawnPoint.position;
                    spawnedObject.SetActive(true);

                    Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = Vector2.left * spawnSpeed;
                    }
                }

                objectsSpawned++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetPooledObject()
    {
        if (!hasInitializedPool)
        {
            InitializeObjectPool();
        }

        GameObject obj = objectPool[poolIndex];
        poolIndex = (poolIndex + 1) % objectPool.Count;

        if (obj.activeInHierarchy)
        {
            obj = Instantiate(objectPrefab);
            objectPool.Add(obj);
        }

        return obj;
    }

    private void Update()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].activeInHierarchy && objectPool[i].transform.position.x < despawnXPosition)
            {
                objectPool[i].SetActive(false);
            }
        }
    }
}
