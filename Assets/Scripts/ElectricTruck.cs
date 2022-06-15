using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class ElectricTruck : Vehicle
{
    [Header("Vehicle Specs")]
    [SerializeField] string vType = "Electric Truck";
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
    public override void MoveTowards(Vector3 destination)
    {
        //Rewrites the move function so it consumes more fuel
        //Save as <moveDirection> the normalized vector of target location minus current location
        Vector3 moveDirection = (destination - transform.position).normalized;
        //Move the vehicle along the <moveDirection> vector at <speed> speed
        transform.position += moveDirection * speed * Time.deltaTime;
        //Drain fuel depending on the speed and carry capacity
        remainignFuel -= Time.deltaTime * speed * carryCapacity / 1.5f;
    }

    // POLYMORPHISM
    public override void RefuelVehicle()
    {
        //Rewrites the function so refueling costs less but takes more time
        //If the player has money
        if (GameManager.gameManager.playerMoney > 0)
        {
            //If the remaining fuel is less than the fuel capacity
            if (remainignFuel < fuelCapacity)
            {
                //Pay for fuel and fill the tank
                GameManager.gameManager.playerMoney -= Time.deltaTime / 10;
                remainignFuel += Time.deltaTime * 1.5f;
            }
            else
            {
                //Call refueling as false and plot next destination
                Debug.Log("Fuel Tank Full!");
                refueling = false;
                // ABSTRACTION
                PlotRoute();
            }
        }
        else
        {
            //Stop refueling if there isn't enough money
            Debug.Log("You are out of money.");
            refueling = false;
            // ABSTRACTION
            PlotRoute();
        }
    }
}
