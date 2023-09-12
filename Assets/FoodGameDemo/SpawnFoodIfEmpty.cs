using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFoodIfEmpty : MonoBehaviour
{
    [SerializeField] private GameObject childObjectToSpawn;

    private bool hasSpawnedChild = false;

    private void Start()
    {
        FoodContainer.Instane.spawnNewFood += SpawnNewFoodChild;
    }

    [ContextMenu("spawnitm")]public void Spawn() { SpawnNewFoodChild(); }
    private void SpawnNewFoodChild()
    {
        if(transform.childCount == 1 && !hasSpawnedChild)
        {
            GameObject obj = Instantiate(childObjectToSpawn, transform);
            obj.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            LeanTween.scale(obj, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeOutQuad);
            hasSpawnedChild = false;
        }
    }
}
