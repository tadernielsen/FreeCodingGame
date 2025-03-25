using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float sanityChanger;

    public float playerSanity;
    public string playerState;
    
    private GameObject playerCamera;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerState = "Normal";
        playerSanity = 75;
        sanityChanger = 0;

        playerCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
