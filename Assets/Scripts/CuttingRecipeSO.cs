using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputKithenObjectSO;
    public KitchenObjectSO outputKithenObjectSO;
    public int progressCuttingMax;
}
