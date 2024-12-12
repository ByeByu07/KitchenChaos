using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingleManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeName(RecipeSO recipeSO)
    {
        recipeName.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.ingredients)
        {
            Transform iconClone = Instantiate(iconTemplate, iconContainer);
            iconClone.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconClone.gameObject.SetActive(true);
        }
    }


}
