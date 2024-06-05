using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipes(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipes(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO kitchenObject = GetKitchenObject().GetKitchenObjectSO();
            GetKitchenObject().SelfDestroy();

            KitchenObject.SpawnKitchenObject(GetOutputKitchenObjectFromInput(kitchenObject), this);
        }
    }

    private bool HasRecipes(KitchenObjectSO kitchenObjectSO)
    {
        foreach(CuttingRecipeSO k in cuttingRecipeSO)
        {
            if(k.inputKithenObjectSO == kitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    public KitchenObjectSO GetOutputKitchenObjectFromInput(KitchenObjectSO input)
    {
        foreach(CuttingRecipeSO k in cuttingRecipeSO)
        {
            if(input == k.inputKithenObjectSO)
            {
                return k.outputKithenObjectSO;
            }
        }

        return null;
    }

   
}
