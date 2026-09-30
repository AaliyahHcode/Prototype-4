using TMPro;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float dayLength = 30f;
    public float nightLength = 45f;
    public TMP_Text timeText; //Day or Night
    public CanvasGroup darkScreen;
    public static bool isNight = false;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isNight = false; timer = dayLength;
        if (darkScreen != null)
        {
            darkScreen.alpha = 0f;
        }
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            SwitchTime();
        }

        if (darkScreen != null)
        {
            float targetAlpha;
            if (isNight)
            {
                targetAlpha = 0.75f;
            }
            else
            {
                targetAlpha = 0f;
            }

            darkScreen.alpha = Mathf.Lerp(darkScreen.alpha, targetAlpha, Time.deltaTime * 2f); //slowly fade the screen - unitydocumentation help
        }
    }

    void SwitchTime()
    {
        if (isNight)
        {
            isNight = false;
            timer = dayLength;
            UpdateText();

            DayNightManager game = FindFirstObjectByType<DayNightManager>();
            if (game != null)
            {
                game.Win();
            }
        }
        else
        {
            isNight = true;
            timer = nightLength;
            UpdateText();
        }
    }

    void UpdateText()
    {
        if (timeText != null)
        {
            if (isNight)
            {
                timeText.text = "NIGHT";
            }
            else
            {
                timeText.text = "DAY";
            }
        }
    }
}
