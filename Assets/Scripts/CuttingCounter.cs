using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArr;

    private int progressCutting;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipes(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    progressCutting = 0;
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
            progressCutting++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

            if(progressCutting >= cuttingRecipeSO.progressCuttingMax) { 
                KitchenObjectSO kitchenObject = GetKitchenObject().GetKitchenObjectSO();
                GetKitchenObject().SelfDestroy();

                KitchenObject.SpawnKitchenObject(GetOutputKitchenObjectFromInput(kitchenObject), this);
            }
        }
    }

    private bool HasRecipes(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    public KitchenObjectSO GetOutputKitchenObjectFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.outputKithenObjectSO;
        } else
        {
            return null;
        }
    }

   public CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArr)
        {
            if (cuttingRecipeSO.inputKithenObjectSO == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
