using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeComplete;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> recipeSOListEntry;
    [SerializeField] private float spawnTimeMax = 4f;
    private float spawntimeInit;
    [SerializeField] private int spawnAmountMax = 1;

    private void Awake()
    {
        recipeSOListEntry = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        spawntimeInit -= Time.deltaTime;
        if(spawntimeInit <= 0f)
        {
            spawntimeInit = spawnTimeMax;

            if(recipeSOListEntry.Count < spawnAmountMax)
            {
                RecipeSO recipeSO = recipeListSO.recipeListSO[UnityEngine.Random.Range(0,recipeListSO.recipeListSO.Count)];
                recipeSOListEntry.Add(recipeSO);

                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < recipeSOListEntry.Count; i++)
        {
            RecipeSO recipeSOEntry = recipeSOListEntry[i];

            if (recipeSOListEntry[i].ingredients.Count == plateKitchenObject.GetListKitchenObjectSO().Count)
            {
                bool plateRecipeMatchRecipeEntry = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in recipeSOEntry.ingredients)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO deliverKitchenObjectSO in plateKitchenObject.GetListKitchenObjectSO())
                    {
                        if(recipeKitchenObjectSO == deliverKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateRecipeMatchRecipeEntry = false;
                    }
                }
                if (plateRecipeMatchRecipeEntry)
                {
                    recipeSOListEntry.RemoveAt(i);
                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);  
    }

    public List<RecipeSO> GetRecipeSOList()
    {
        return recipeSOListEntry;
    }
}
