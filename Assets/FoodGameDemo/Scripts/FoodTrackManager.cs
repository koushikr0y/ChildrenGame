using System.Collections.Generic;
using UnityEngine;

public enum FOODTYPE
{
    HEALTHY,
    NOTHEALTHY,
}
public class FoodTrackManager : MonoBehaviour
{
    [System.Serializable]
    private class FoodDetail { 
    
        public Sprite foodImage;
        public FOODTYPE foodType;
        public Transform spawnFoodPos;
    }

    [SerializeField] private List<FoodDetail> foodList;
    [SerializeField] private GameObject foodPrefab;

    public static FoodTrackManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
