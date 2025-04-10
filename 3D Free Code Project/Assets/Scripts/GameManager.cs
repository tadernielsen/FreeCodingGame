using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    private TextMeshProUGUI sanityText;
    private PlayerManager playerManager;

    private Volume volume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sanityText = GameObject.Find("SanityCounter").GetComponent<TextMeshProUGUI>();

        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        volume.profile.TryGet(out Vignette viginette);
        float currentSanity = playerManager.playerSanity;
        string sanityOr;

        if (currentSanity > 20)
        {
            sanityOr = "SANITY: ";
            viginette.active = false;
        }
        else
        {
            sanityOr = "INSANITY: ";
            viginette.active = true;
        }

        string sanityTextValue = sanityOr + currentSanity.ToString();
        
        sanityText.text = sanityTextValue;
    }
}
