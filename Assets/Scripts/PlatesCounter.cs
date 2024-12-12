using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnSpawnPlate;
    public event EventHandler OnDestroyPlate;
    
    private float spawnPlatesTimer;
    [SerializeField] private float spawnPlatesTimerMax = 3f;
    private int spawnPlatesAmount;
    [SerializeField] private int spawnPlatesAmountMax = 4;
    [SerializeField] private KitchenObjectSO platesSO;

    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if (spawnPlatesTimer > spawnPlatesTimerMax)
        {
            spawnPlatesTimer = 0;

            if(spawnPlatesAmount < spawnPlatesAmountMax)
            {
                spawnPlatesAmount++;
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            if(spawnPlatesAmount > 0)
            {
                spawnPlatesAmount--;
                KitchenObject.SpawnKitchenObject(platesSO, player);
                OnDestroyPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
