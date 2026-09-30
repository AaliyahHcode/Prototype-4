using TMPro;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public TMP_Text winText;
    public TMP_Text gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    public void Win()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        if(gameOverText != null) //show game oevr
        {
            gameOverText.gameObject.SetActive(true);
        }
        Time.timeScale = 0f;
    }
}
