using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene
    {
        MainMenu,
        LoadingScene,
        MainScene,
    }
    
    private static Scene scene;

    public static void LoadScene(Scene scene)
    {
        Loader.scene = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void CallbackFromLoadingScene()
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
