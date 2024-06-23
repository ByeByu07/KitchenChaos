using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

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

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float) progressCutting / cuttingRecipeSO.progressCuttingMax });
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

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)progressCutting / cuttingRecipeSO.progressCuttingMax });

            if (progressCutting >= cuttingRecipeSO.progressCuttingMax) { 
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
