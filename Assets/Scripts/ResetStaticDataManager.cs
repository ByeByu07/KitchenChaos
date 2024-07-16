using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticListenerData();
        BaseCounter.ResetStaticListenerData();
        TrashCounter.ResetStaticListenerData();
    }
}