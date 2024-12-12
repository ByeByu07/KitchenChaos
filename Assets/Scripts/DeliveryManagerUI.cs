using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform containerRecipeUI;
    [SerializeField] Transform recipeTemplateUI;

    private void Awake()
    {
        recipeTemplateUI.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeComplete += DeliveryManager_OnRecipeComplete;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeComplete(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawn(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform child in containerRecipeUI)
        {
            if (child == recipeTemplateUI) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetRecipeSOList())
        {
            Transform obj =  Instantiate(recipeTemplateUI, containerRecipeUI);
            obj.gameObject.SetActive(true);
            obj.GetComponent<DeliverySingleManagerUI>().SetRecipeName(recipeSO);
        }
    }
}
