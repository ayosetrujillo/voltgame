using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyController : MonoBehaviour
{

    public int amountEnergy;

    public int currentEnergy;
    public static PlayerEnergyController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentEnergy > 100) currentEnergy = 100;
        if (currentEnergy < 0) currentEnergy = 0;

    }


    public void AddEnergy()
    {
        currentEnergy = currentEnergy + amountEnergy;
        UIController.instance.UpdateEnergy(currentEnergy);

    }


    public void SpendEnergy(int amount)
    {
        currentEnergy = currentEnergy - amount;
        if (currentEnergy > 100) currentEnergy = 100;
        if (currentEnergy < 0) currentEnergy = 0;
       
        UIController.instance.UpdateEnergy(currentEnergy);

    }



}
