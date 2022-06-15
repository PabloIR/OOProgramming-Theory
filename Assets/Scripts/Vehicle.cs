using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // ENCAPSULATION
    public string vehicleType { get; private set; }
    public float fuelCapacity { get; private set; }
    public float remainignFuel { get; protected set; }
    public float carryCapacity { get; private set; }
    public float speed { get; protected set; }
    Vector3 deliveryDestination;
    int currentDestination;

    bool moving;
    public bool refueling { get; protected set; }
    bool isReturning;

    public Vector3 positiveOffset { get; private set; }
    public Vector3 negativeOffset { get; private set; }
    // Start is called before the first frame update
    public void InitializeVehicle()
    {
        //Set the vehicle as moving and point him toward the first target location
        moving = true;
        currentDestination = 0;
        // ABSTRACTION
        PlotRoute();
    }

    // Update is called once per frame
    void Update()
    {
        //If the vehicle is moving move towards destination
        if (moving)
        {
            // ABSTRACTION
            Move(deliveryDestination);
        }
        //If the vehicle is refueling wait
        if (refueling)
        {
            // ABSTRACTION
            RefuelVehicle();
        }

    }

    public virtual void Move(Vector3 destination)
    {
        //If there is fuel and the destination is different enough from the vehicle position
        if (remainignFuel > 0 && ((int)destination.x != (int)transform.position.x))
        {
            //Move towards destination
            // ABSTRACTION
            MoveTowards(destination);
        }
        //If there isn't fuel
        else if (remainignFuel <= 0)
        {
            //Start Out of fuel method
            // ABSTRACTION
            OutOfFuel();
        }
        //If the destination is near enough to the vehicle position
        else if ((int)destination.x == (int)transform.position.x)
        {
            //set moving false and start refueling
            moving = false;
            Debug.Log("Destination reached!");
            refueling = true;
        }

    }

    public virtual void MoveTowards(Vector3 destination)
    {
        //Save as <moveDirection> the normalized vector of target location minus current location
        Vector3 moveDirection = (destination - transform.position).normalized;
        //Move the vehicle along the <moveDirection> vector at <speed> speed
        transform.position += moveDirection * speed * Time.deltaTime;
        //Drain fuel depending on the speed and carry capacity
        remainignFuel -= Time.deltaTime * speed * carryCapacity / 2;
    }

    public void OutOfFuel()
    {
        //Destroy vehicle on empty tank
        Debug.Log(vehicleType + " has run out of fuel. Erasing");
        Destroy(gameObject);
    }

    public virtual void RefuelVehicle()
    {
        //If the player has money
        if (GameManager.gameManager.playerMoney > 0)
        {
            //If the remaining fuel is less than the fuel capacity
            if (remainignFuel < fuelCapacity)
            {
                //Pay for fuel and fill the tank
                GameManager.gameManager.playerMoney -= Time.deltaTime;
                remainignFuel += Time.deltaTime * 3;
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

    public void PlotRoute()
    {
        //Save as a temporary var the object with the name of the next target
        GameObject tempGO = GameObject.Find("City0" + currentDestination);
        //If the player is in the outbound route Calculate the outbound route
        if (!isReturning)
        {
            // ABSTRACTION
            CalculateOutbound(tempGO);
        }//Else calculate the inbound route
        else
        {
            // ABSTRACTION
            CalculateInbound(tempGO);
        }
    }

    void CalculateOutbound(GameObject tempRoute)
    {
        //If the next destination exist
        if (tempRoute != null)
        {
            //The vehicle destination is the target postion plus offset
            deliveryDestination = tempRoute.transform.position + positiveOffset;
            //The next destination is updated
            currentDestination++;
        }
        else
        {
            Debug.Log("Last Stop!");
            //Call for the inbound route
            isReturning = true;
            currentDestination--;
            //Pay the player for a successful delivery
            GameManager.gameManager.CalculatePayment(carryCapacity);
            //Rotate the vehicle object
            transform.Rotate(Vector3.up * 180);
        }
        //Either wait set the vehicle as moving
        moving = true;
    }

    void CalculateInbound(GameObject tempRoute)
    {
        //If the next destination exist
        if (tempRoute != null)
        {
            //The vehicle destination is the target postion plus offset
            deliveryDestination = tempRoute.transform.position + negativeOffset;
            //The next destination is updated
            currentDestination--;
        }
        else
        {
            Debug.Log("First Stop!");
            //Call for the outbound route
            isReturning = false;
            currentDestination++;
            //Pay the player for a successful delivery
            GameManager.gameManager.CalculatePayment(carryCapacity);
            //Rotate the vehicle object
            transform.Rotate(Vector3.up * 180);
        }
        //Either wait set the vehicle as moving
        moving = true;
    }

    public void SetVehicleType(string vType)
    {
        vehicleType = vType;
    }

    public void SetFuelCapacity(float amount)
    {
        fuelCapacity = amount;
        remainignFuel = amount;
    }

    public void SetCarryCapacity(float amount)
    {
        carryCapacity = amount;
    }

    public void SetSpeed(float amount)
    {
        speed = amount;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetOffset(Vector3 pOffset, Vector3 nOffset)
    {
        positiveOffset = pOffset;
        negativeOffset = nOffset;
    }
}
