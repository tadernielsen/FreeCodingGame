using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TextMeshProUGUI sanityText;
    private PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sanityText = GameObject.Find("SanityCounter").GetComponent<TextMeshProUGUI>();

        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSanity = playerManager.playerSanity;
        string sanityOr;

        if (currentSanity > 20)
        {
            sanityOr = "SANITY: ";
        }
        else
        {
            sanityOr = "INSANITY: ";
        }

        string sanityTextValue = sanityOr + currentSanity.ToString();
        
        sanityText.text = sanityTextValue;
    }
}
