using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player Data Members
    private float sanityChanger;
    private float sanityChangeInterval;

    public float playerSanity;
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
        time += Time.deltaTime;
        if (time >= sanityChangeInterval)
        {
            UpdateSanity();
            time -= sanityChangeInterval;
        }
    }
}
