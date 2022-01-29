using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInventory : MonoBehaviour
{
    public static AnimalInventory Instance;

    int animalCount = 0;
    [SerializeField]
    int zRows;
    [SerializeField]
    int xCols;
    public int AnimalCount { get => animalCount;}

    //[SerializeField]
    //Transform firstParkingSpot;

    //[SerializeField]
    //GameObject animalPrefab;

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

        //Vector3 parkingSpot = firstParkingSpot.position + (animalCount%zRows)* Vector3.forward * 2f + (animalCount / zRows) * Vector3.right * 2f;

        //Instantiate(animalPrefab, firstParkingSpot.position, firstParkingSpot.rotation);
    }
}
