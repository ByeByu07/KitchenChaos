using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputKithenObjectSO;
    public KitchenObjectSO outputKithenObjectSO;
    public float burningTimerMax;
}
