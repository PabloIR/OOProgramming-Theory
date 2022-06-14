using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Vehicle Stats")]
    protected static string vehicleType;
    protected static float fuelCapacity;
    protected static float remainignFuel;
    protected static float carryCapacity;
    protected static float speed;
    protected static Vector3 deliveryDestination;
    int currentDestination;

    protected static bool moving;
    protected static bool refueling;
    protected static bool isReturning;

    protected static Vector3 positiveOffset;
    protected static Vector3 negativeOffset;
    // Start is called before the first frame update
    public void InitializeVehicle()
    {
        moving = true;
        currentDestination = 1;
        PlotRoute();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Move(deliveryDestination);
        }
        if (refueling)
        {
            RefuelVehicle();
        }

    }

    public virtual void Move(Vector3 destination)
    {
        if (remainignFuel > 0 && ((int)destination.x != (int)transform.position.x))
        {
            MoveTowards(destination);
        }
        else if (remainignFuel <= 0)
        {
            OutOfFuel();
        }
        else if ((int)destination.x == (int)transform.position.x)
        {
            //transform.position = destination;
            moving = false;
            Debug.Log("Destination reached!");
            refueling = true;
        }

    }

    public virtual void MoveTowards(Vector3 destination)
    {
        //Save as <lookDirection> the normalized vector of target location minus current location
        Vector3 lookDirection = (destination - transform.position).normalized;
        //Move the vechicle along the <lookDirection> vector at <speed> speed
        transform.position += lookDirection * speed * Time.deltaTime;
        //Drain fuel depending on the speed and carry capacity
        remainignFuel -= Time.deltaTime * speed * carryCapacity / 3;
    }

    public void OutOfFuel()
    {
        Debug.Log(vehicleType + " has run out of fuel. Erasing");
        Destroy(gameObject);
    }

    public void RefuelVehicle()
    {
        if (GameManager.gameManager.playerMoney > 0)
        {
            if (remainignFuel < fuelCapacity)
            {
                GameManager.gameManager.playerMoney -= Time.deltaTime;
                remainignFuel += Time.deltaTime * 3;
            }
            else
            {
                Debug.Log("Fuel Tank Full!");
                refueling = false;
                PlotRoute();
            }
        }
        else
        {
            Debug.Log("You are out of money. GameOver");
            refueling = false;
        }
    }

    void PlotRoute()
    {
        GameObject tempGO = GameObject.Find("City0" + currentDestination);
        if (!isReturning)
        {
            CalculateOutbound(tempGO);
        }
        else
        {
            CalculateInbound(tempGO);
        }
    }

    void CalculateOutbound(GameObject tempRoute)
    {
        if (tempRoute != null)
        {
            deliveryDestination = tempRoute.transform.position + positiveOffset;
            currentDestination++;
        }
        else
        {
            Debug.Log("Last Stop!");
            isReturning = true;
            currentDestination--;
            GameManager.gameManager.CalculatePayment(carryCapacity);
        }
        moving = true;
    }

    void CalculateInbound(GameObject tempRoute)
    {
        if (tempRoute != null)
        {
            deliveryDestination = tempRoute.transform.position + negativeOffset;
            currentDestination--;
        }
        else
        {
            Debug.Log("First Stop!");
            isReturning = false;
            currentDestination++;
            GameManager.gameManager.CalculatePayment(carryCapacity);
        }
        moving = true;
    }
}
