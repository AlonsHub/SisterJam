using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInventory : MonoBehaviour
{
    public static AnimalInventory Instance;

    int animalCount = 0;
    public int AnimalCount { get => animalCount;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddAnimal()
    {
        animalCount++;
        //Call update UI


    }
}
