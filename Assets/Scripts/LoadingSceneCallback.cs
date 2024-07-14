using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneCallback : MonoBehaviour
{
    private bool isFirstLoading = true;
    private void Update()
    {
        if (isFirstLoading)
        {
            isFirstLoading = false;
            Loader.CallbackFromLoadingScene();
        }
    }
}
