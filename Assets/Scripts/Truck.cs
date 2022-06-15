using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Truck : Vehicle
{
    string vType = "Truck";
    float fCapacity = 40;
    float cCapacity = 5;
    float speedAmount = 3;
    Vector3 pOffset = new Vector3(0, 0.5f, 0);
    Vector3 nOffset = new Vector3(0, 0.5f, -4);
    void Start()
    {
        SetVehicleType(vType);
        SetFuelCapacity(fCapacity);
        SetCarryCapacity(cCapacity);
        SetSpeed(speedAmount);
        SetOffset(pOffset, nOffset);
        InitializeVehicle();
        transform.position = new Vector3(-12, 0.5f, 2);
    }
}
