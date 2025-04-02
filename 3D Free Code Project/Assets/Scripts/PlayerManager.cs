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
        Debug.Log("Sanity Changer: " + sanityChanger);

        return sanityChanger;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerState = "Normal";
        playerSanity = 75f;
        sanityChanger = 0f;
        sanityChangeInterval = 1;

        playerCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        // Sanity Changer
        time += Time.deltaTime;
        if (time >= sanityChangeInterval)
        {
            UpdateSanity();
            time -= sanityChangeInterval;
        }

        // Player Eyes
        int fearLevel = 0, calmLevel = 0;

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
        
        sanityChanger = CalculateSanityChange(fearLevel, calmLevel);
    }
}
