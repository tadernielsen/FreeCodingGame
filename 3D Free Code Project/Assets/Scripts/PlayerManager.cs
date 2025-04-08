using System;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player Data Members
    private float sanityChanger;
    private float sanityChangeInterval;

    public float playerSanity;
    public float lookDistance;
    public string playerState;
    
    
    private GameObject playerCamera;
    private GameObject playerHand;

    public Light[] lights;

    // Other Members
    float time;

    private void UpdateSanity()
    {
        playerSanity += sanityChanger;
        
        if (playerSanity < 0)
        {
            playerSanity = 0;
        }
        else if (playerSanity > 100)
        {
            playerSanity = 100;
        }
    }

    private float CalculateSanityChange(int fearLevel, int calmLevel)
    {
        int totalLevel = fearLevel - calmLevel;
        float sanityChanger = 0;

        if (totalLevel > 0)
        {
            sanityChanger = -(2 * totalLevel)/(calmLevel + 1);
        }
        else if (totalLevel < 0)
        {
            sanityChanger = -totalLevel * 2/(fearLevel + 2);
        }

        return sanityChanger;
    }

    private void FearUpdatingHelper()
    {
        int fearLevel = 0, calmLevel = 0;
        time += Time.deltaTime;

        // Player Eyes
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, lookDistance) && !hit.collider.gameObject.CompareTag("Untagged"))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * lookDistance, Color.green);
            if (hit.collider.gameObject.CompareTag("SanityAffectingObject"))
            {
                VisibleObject visibleObject = hit.collider.gameObject.GetComponent<VisibleObject>();
                if (visibleObject.isCalm)
                {
                    calmLevel = visibleObject.level;
                }
                else
                {
                    fearLevel = visibleObject.level;
                }
            }
        }
        else
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * lookDistance, Color.red);
        }

        // Player Hand
        Transform holdingObject = playerHand.transform.GetChild(0);
        if (holdingObject)
        {
            HoldingObject objectScript = holdingObject.GetComponent<HoldingObject>();
            if (objectScript.isActive)
            {
                calmLevel = objectScript.calmLevel;
            }
            else
            {
                calmLevel = 0;
            }

            if (Input.GetKey(KeyCode.E))
            {
                if (time >= sanityChangeInterval)
                {
                    objectScript.useObject();
                }
            }
            else
            {
                objectScript.stopObject();
            }
        }

        calmLevel += FindLights(calmLevel);
        
        sanityChanger = CalculateSanityChange(fearLevel, calmLevel);

        
        // Sanity Changer
        if (time >= sanityChangeInterval)
        {
            UpdateSanity();
            time -= sanityChangeInterval;
        }
    }

    private int FindLights(int calmLevel = 0, int fearLevel = 0)
    {
         float lightLevel = 0;

        foreach (Light lightSource in lights)
        {

            // Calculate distance and intensity
            float distance = Vector3.Distance(lightSource.transform.position, transform.position);
            float intensity = lightSource.intensity / (distance * distance);

            // Add intensity to the total light level
            lightLevel += intensity;
        }

        if (lightLevel > 1)
        {
            calmLevel = 1;
            fearLevel = 0;
        }
        else
        {
            calmLevel = 0;
            fearLevel = 1;
        }

        return calmLevel;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerState = "Normal";
        playerSanity = 75f;
        sanityChanger = 0f;
        sanityChangeInterval = 1;

        playerCamera = GameObject.Find("Main Camera");
        playerHand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        FearUpdatingHelper();
    }
}
