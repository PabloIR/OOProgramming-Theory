using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : Vehicle
{
    void Start()
    {
        vehicleType = "Bike";
        carryCapacity = 1;
        speed = 2;
        positiveOffset = new Vector3(0, 0, 1);
        negativeOffset = new Vector3(0, 0, -5);
        transform.position = new Vector3(-8, 0, 3);
    }

    public override void Move(Vector3 destination)
    {
        if ((int)destination.x != (int)transform.position.x)
        {
            MoveTowards(destination);
        }
        else if ((int)destination.x == (int)transform.position.x)
        {
            //transform.position = destination;
            moving = false;
            Debug.Log("Destination reached!");
            refueling = true;
        }

    }

    public override void MoveTowards(Vector3 destination)
    {
        //Save as <lookDirection> the normalized vector of target location minus current location
        Vector3 lookDirection = (destination - transform.position).normalized;
        //Move the vechicle along the <lookDirection> vector at <speed> speed
        transform.position += lookDirection * speed * Time.deltaTime;
    }
}
