using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISLamp : MonoBehaviour
{
    //Refference to the object that needs spawning
    public GameObject spawnItem;

    //Reference to MoneyCounter to get the state of an appliance
    public MoneyCounter moneyCounter;
    //Check variable to see if the object exists, eliminates dublicates
    private bool iconIsDisplaying = false;


    void Update()
    {
        //Check appliance state
        var displayIcon = moneyCounter.stateLamp;
        if (displayIcon == true && iconIsDisplaying == false) //If appliance is on and there is no Icon spawn 1
        {
            Spawn();
            iconIsDisplaying = true;
        }
        else if (displayIcon == false && iconIsDisplaying == true) // If appliance is off and there is still an Icon destroy the icon object
        {
            DestroyPreFab();
            iconIsDisplaying = false;
        }

    }

    //Spawn the Icon (Prefab)
    //I technically spawn an Object (Prefab), that is why everything that I want to spawn has to be made into a prefab (object file inside the unity project)
    public void Spawn()
    {
        //Instantiate used to create a clone with the specified parameters Prefab - Position - Rotation
        GameObject newSpawnedObject = Instantiate(spawnItem, transform.position, Quaternion.Euler(-90, 0, 0));
        newSpawnedObject.transform.parent = transform;
    }

    //Destroy the Icon (Prefab)
    public void DestroyPreFab()
    {
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
                Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
