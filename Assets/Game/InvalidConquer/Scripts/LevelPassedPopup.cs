using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelPassedPopup : MonoBehaviour
{
    [SerializeField] private Button btnNext, btnRestart;
    [SerializeField] private int currentLvlIndex;
    private RectTransform rect;
    private float gameOverWindowStartY = 450f;
    private static List<string> SCENE_NAMES = new List<string>()
    {
        "Level1",
        "Level2",
        "Level3",
        "Level4",
        "Level5",
        "Level6"
    };

    private static readonly List<float> curveValues = new List<float>() // 20 first
        {
            0f, 0f, 0f, 0.004f, 0.01f, 0.015f, 0.05f, 0.07f, 0.09f,
            0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.41f, 0.48f, 0.56f, 0.64f,
            0.78f, 0.84f, 0.88f, 0.93f, 0.97f, 1f, 1.04f, 1.065f, 1.09f,
            1.12f, 1.125f, 1.13f, 1.13f, 1.12f, 1.09f, 1.07f, 1.03f, 1f
        };

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        btnNext.onClick.AddListener(NextLevel);
        btnRestart.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        btnNext.onClick.RemoveListener(NextLevel);
        btnRestart.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        StartCoroutine(nameof(WindowHideAction), true);
    }

    private void NextLevel()
    {
        StartCoroutine(nameof(WindowHideAction), false);
    }

    public void ShowWindow()
    {
        StartCoroutine(nameof(ShowWindowRoutine));
    }

    private IEnumerator ShowWindowRoutine()
    {
        rect.anchoredPosition = new Vector3(0f, gameOverWindowStartY);
        int frame = 0;

        while (frame < curveValues.Count)
        {
            rect.anchoredPosition = new Vector3(0f, gameOverWindowStartY - 2 * gameOverWindowStartY * curveValues[frame]);
            frame++;
            yield return new WaitForFixedUpdate();
        }
        rect.anchoredPosition = new Vector3(0f, -gameOverWindowStartY);
    }

    public IEnumerator WindowHideAction(bool restart)
    {
        rect.anchoredPosition = new Vector3(0f, -gameOverWindowStartY);
        int frame = curveValues.Count - 1;

        while (frame > 0)
        {
            rect.anchoredPosition = new Vector3(0f, gameOverWindowStartY - 2 * gameOverWindowStartY * curveValues[frame]);
            frame--;
            yield return new WaitForFixedUpdate();
        }
        rect.anchoredPosition = new Vector3(0f, gameOverWindowStartY);
        if (restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            if (SceneManager.GetActiveScene().name != "Level6")
            {
                SceneManager.LoadScene(SCENE_NAMES[currentLvlIndex]); // it goes to the next one
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
