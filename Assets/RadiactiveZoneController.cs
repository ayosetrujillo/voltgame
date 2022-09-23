using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiactiveZoneController : MonoBehaviour
{
    public PlayerController _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
               //Surf
            if (_player.playerIsSurfing)
            {
                PlayerEnergyController.instance.AddEnergy(100);
            } else {
                PlayerHealthController.instance.AddDamage(100);
            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Surf
            if (_player.playerIsSurfing)
            {
                PlayerEnergyController.instance.AddEnergy(100);
            }
            else
            {
                PlayerHealthController.instance.AddDamage(100);
            }
        }
    }



}
