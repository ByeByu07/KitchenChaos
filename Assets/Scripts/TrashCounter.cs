using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
   public static event EventHandler OnDrop;
   public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().SelfDestroy();
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
    }
}
