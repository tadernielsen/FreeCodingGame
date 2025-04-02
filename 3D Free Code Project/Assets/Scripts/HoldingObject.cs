using UnityEngine;

public class HoldingObject : MonoBehaviour
{
    public string objectName;
    public string objectType;

    public bool isActive;

    public int calmLevel;
    public int fuel;
    public float duration;

    public void useObject()
    {
        if (objectType == "fuel")
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
        else if (objectType == "duration")
        {

        }
    }
}
