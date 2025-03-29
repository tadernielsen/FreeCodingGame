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
            sanityChanger = -(fearLevel^2)/3;
        }
        else if (totalLevel < 0)
        {
            sanityChanger = (fearLevel^2)/10;
        }
        else
        {
            sanityChanger = 0f;
        }
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
                Debug.Log("Fear level " + fearLevel);
            }
            else
            {
                fearLevel = 0;
            }
        }
        else
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * lookDistance, Color.red);
        }
        
        CalculateSanityChange();
    }
}
