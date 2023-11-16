using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameplayLogic : MonoBehaviour
{
    public Image HealthBar;
    public Text HealthText;
    public GameObject PanelGameResult;
    public Text GameResultText;

    public void UpdateHealthBar(float CurrentHealth, float MaxHealth)
    {
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        HealthText.text = CurrentHealth.ToString();

        if (CurrentHealth <= 0)
        {
            GameResult(false);
        }
    }

    public void GameResult(bool win)
    {
        PanelGameResult.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (win)
        {
            GameResultText.color = Color.green;
            GameResultText.text = "Mission Complete";
        } else
        {
            GameResultText.color = Color.red;
            GameResultText.text = "Game Over";
        }
    }

    public void GameResultDecision(bool TryAgain)
    {
        if (TryAgain) SceneManager.LoadScene("MazeStage");
        else SceneManager.LoadScene("SampleScene");
    }
}
