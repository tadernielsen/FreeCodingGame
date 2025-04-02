using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player Data Members
    private float sanityChanger;
    private float sanityChangeInterval;
    private int fearLevel;
    private int calmLevel;

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

    private void CalculateSanityChange()
    {
        int totalLevel = fearLevel - calmLevel;
        if (totalLevel > 0)
        {
            sanityChanger = -(2 * totalLevel)/(calmLevel + 1);
        }
        else if (totalLevel < 0)
        {
            sanityChanger = totalLevel * 2/(fearLevel + 2);
        }
        else
        {
            sanityChanger = 0f;
        }
        Debug.Log("Sanity Changer: " + sanityChanger);
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
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, lookDistance) && !hit.collider.gameObject.CompareTag("Untagged"))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * lookDistance, Color.green);
            if (hit.collider.gameObject.CompareTag("SanityAffectingObject"))
            {
                VisibleObject visibleObject = hit.collider.gameObject.GetComponent<VisibleObject>();
                fearLevel = visibleObject.fearLevel;
            }
        }
        else
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * lookDistance, Color.red);

            fearLevel = 0;
            calmLevel = 0;
        }
        
        CalculateSanityChange();
    }
}
