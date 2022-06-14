using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Vehicle
{
    void Start()
    {
        vehicleType = "Truck";
        fuelCapacity = 40;
        carryCapacity = 5;
        speed = 3;
        positiveOffset = new Vector3(0, 0.5f, 0);
        negativeOffset = new Vector3(0, 0.5f, -4);
        transform.position = new Vector3(-8, 0.5f, 2);
        remainignFuel = fuelCapacity;
        InitializeVehicle();
    }

}
