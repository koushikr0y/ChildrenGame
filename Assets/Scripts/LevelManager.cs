using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Button foodBtn;
    [SerializeField] Button skateBtn;
    [SerializeField] Button appQuitBtn;

    private void Start()
    {
        foodBtn.onClick.AddListener(() => { Loader.Load(Loader.Scene.FoodGame); });
        skateBtn.onClick.AddListener(() => { Loader.Load(Loader.Scene.SkateGame); });
        appQuitBtn.onClick.AddListener(() => Application.Unload());
    }
}
