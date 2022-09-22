using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargerController : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(PlayerEnergyController.instance.currentEnergy < 100)
            {
                PlayerEnergyController.instance.AddEnergy(2);
            }

        }
    }
}
