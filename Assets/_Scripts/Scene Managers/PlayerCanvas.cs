using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public static PlayerCanvas canvas;

    [Header("Component References")]
    [SerializeField]
    Image reticule;
    [SerializeField]
    UIFader damageImage;
    [SerializeField]
    Text gameStatusText;
    [SerializeField]
    Text healthValue;
    [SerializeField]
    Text killsValue;
    [SerializeField]
    Text logText;
    [SerializeField]
    AudioSource deathAudio;

    //Ensure there is only one PlayerCanvas
    void Awake()
    {
        if (canvas == null)
            canvas = this;
        else if (canvas != this)
            Destroy(gameObject);
    }

    //Find all of our resources
    void Reset()
    {
        reticule = GameObject.Find("Reticule").GetComponent<Image>();
        damageImage = GameObject.Find("Damage Flash").GetComponent<UIFader>();
        gameStatusText = GameObject.Find("Game Status Text").GetComponent<Text>();
        healthValue = GameObject.Find("Health Value").GetComponent<Text>();
        killsValue = GameObject.Find("Kills Value").GetComponent<Text>();
        logText = GameObject.Find("Log Text").GetComponent<Text>();
        deathAudio = GameObject.Find("Death Audio").GetComponent<AudioSource>();
    }

    public void Initialize()
    {
        reticule.enabled = true;
        gameStatusText.text = "";
        ClearLogText();
    }

    public void HideReticule()
    {
        reticule.enabled = false;
    }

    public void FlashDamageEffect()
    {
        damageImage.Flash();
    }

    public void PlayDeathAudio()
    {
        if (!deathAudio.isPlaying)
            deathAudio.Play();
    }

    public void SetKills(int amount)
    {
        killsValue.text = amount.ToString();
    }

    public void SetHealth(int amount)
    {
        healthValue.text = amount.ToString();
    }

    public void WriteGameStatusText(string text)
    {
        gameStatusText.text = text;
    }

    public void WriteLogText(string text, float duration)
    {
        CancelInvoke();
        logText.text = text;
        Invoke("ClearLogText", duration);
    }

    void ClearLogText()
    {
        logText.text = "";
    }
}