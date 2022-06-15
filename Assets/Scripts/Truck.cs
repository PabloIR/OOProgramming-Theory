using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Truck : Vehicle
{
    [Header("Vehicle Specs")]
    [SerializeField] string vType = "Truck";
    [SerializeField] float fCapacity = 40;
    [SerializeField] float cCapacity = 5;
    [SerializeField] float speedAmount = 3;
    [SerializeField] Vector3 pOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] Vector3 nOffset = new Vector3(0, 0.5f, -4);

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

    // POLYMORPHISM
    // Modify the method to deactivate the smoke particles while refueling
    public override void RefuelVehicle()
    {
        base.RefuelVehicle();
        if (remainignFuel < fuelCapacity)
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        else
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
