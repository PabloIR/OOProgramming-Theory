using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Bike : Vehicle
{
    string vType = "Bike";
    float fCapacity = 1;
    float cCapacity = 1;
    float speedAmount = 2;
    Vector3 pOffset = new Vector3(0, 0, 1);
    Vector3 nOffset = new Vector3(0, 0, -5);
    void Start()
    {
        SetVehicleType(vType);
        SetFuelCapacity(fCapacity);
        SetCarryCapacity(cCapacity);
        SetSpeed(speedAmount);
        SetOffset(pOffset, nOffset);
        transform.position = new Vector3(-12, 0, 3);
        InitializeVehicle();
    }

    // POLYMORPHISM
    public override void MoveTowards(Vector3 destination)
    {
        //Rewrites the move function so it doesn't consume fuel
        //Save as <moveDirection> the normalized vector of target location minus current location
        Vector3 moveDirection = (destination - transform.position).normalized;
        //Move the vechicle along the <moveDirection> vector at <speed> speed
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
