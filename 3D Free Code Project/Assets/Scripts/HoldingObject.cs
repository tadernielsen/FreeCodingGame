using UnityEngine;

public class HoldingObject : MonoBehaviour
{
    public string objectName;

    public bool isActive;

    public int calmLevel;
    public int fuel;

    public void useObject()
    {
        if (fuel > 0)
        {
            isActive = true;
            fuel -= 1;
            Debug.Log("Fuel: " + fuel);
        }
        else
        {
            isActive = false;
            Debug.Log("No fuel left!");
        }
    }
}
