using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveEffect;
    [SerializeField] private GameObject particleEffect;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnChangedState;
    }

    private void StoveCounter_OnChangedState(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool isShowVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveEffect.SetActive(isShowVisual);
        particleEffect.SetActive(isShowVisual);
    }
}

