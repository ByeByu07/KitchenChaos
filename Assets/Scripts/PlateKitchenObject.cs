using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddIngredientEventArgs> OnAddIngredient;
    public class OnAddIngredientEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOToPlate;
    private List<KitchenObjectSO> listKitchenObjectSOs;

    private void Awake()
    {
        listKitchenObjectSOs = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // check valid ingredients
        if (!validKitchenObjectSOToPlate.Contains(kitchenObjectSO))
        {
            return false;
        }

        // check duplicate KitchenObjectSO
        if(listKitchenObjectSOs.Contains(kitchenObjectSO))
        {
            return false;
        } else
        {
            listKitchenObjectSOs.Add(kitchenObjectSO);
            OnAddIngredient?.Invoke(this, new OnAddIngredientEventArgs
            {
                kitchenObjectSO = kitchenObjectSO,
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetListKitchenObjectSO()
    {
        return listKitchenObjectSOs;
    }
}
