using UnityEngine;
using UnityEngine.AI;

public class HoldingObject : MonoBehaviour
{
    public string objectName;

    public bool isActive;

    public int calmLevel;
    public int fuel;

    public GameObject lightObject;
    private Light lightSource;

    void Start()
    {
        isActive = false;
        fuel = 100;
        lightSource = lightObject.GetComponent<Light>();
        lightSource.enabled = false;
    }

    public void useObject()
    {
        if (fuel > 0)
        {
            isActive = true;
            fuel -= 1;
            Debug.Log("Fuel: " + fuel);

            lightSource.enabled = true;
        }
        else
        {
            isActive = false;
            Debug.Log("No fuel left!");
        }
    }

    public void stopObject()
    {
        isActive = false;
        lightSource.enabled = false;
    }
}
