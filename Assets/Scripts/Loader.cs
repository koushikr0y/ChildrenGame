using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader {

    public enum Scene
    {
        FoodGame,
        MainMenu,
        SkateGame,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(targetScene.ToString());

    }

    static IEnumerator LoadAsyncScene(Scene targteScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targteScene.ToString());
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < .9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
