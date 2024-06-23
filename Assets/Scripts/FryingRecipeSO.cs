using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputKithenObjectSO;
    public KitchenObjectSO outputKithenObjectSO;
    public float fryingTimerMax;
}
