using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static List<string> sceneNames = new List<string>()
    {
        "Menu",
        "Level1",
        "Level2",
        "Level3",
        "Level4",
        "Level5"
    };

    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private Image lvl1, lvl2, lvl3, lvl4, lvl5;
    [SerializeField] private Sprite levelLocked, levelUnlocked;
    private float timeToCompleteAnim = 1f;
    private float timePassed;
    [SerializeField] private RectTransform settingsFrameRect, levelSelectorFrameRect;
    private float startY = 565f;
    private bool isBusy;
    [SerializeField] private List<RectTransform> clouds;
    private List<float> cloudsSpeed = new List<float>();

    private static readonly List<float> curveValues = new List<float>() // 20 first
        {
            0f, 0f, 0f, 0.004f, 0.01f, 0.015f, 0.05f, 0.07f, 0.09f,
            0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.41f, 0.48f, 0.56f, 0.64f,
            0.78f, 0.84f, 0.88f, 0.93f, 0.97f, 1f, 1.04f, 1.065f, 1.09f,
            1.12f, 1.125f, 1.13f, 1.13f, 1.12f, 1.09f, 1.07f, 1.03f, 1f
        };

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    private void OnEnable()
    {
        for (int i = 0; i < clouds.Count; i++)
        {
            float speed = Random.Range(0.01f, 0.05f);
            cloudsSpeed.Add(speed);
        }

        if (DemoData.Instance.Level2Opened) lvl2.sprite = levelUnlocked;
        else lvl2.sprite = levelLocked;
        if (DemoData.Instance.Level3Opened) lvl3.sprite = levelUnlocked;
        else lvl3.sprite = levelLocked;
        if (DemoData.Instance.Level4Opened) lvl4.sprite = levelUnlocked;
        else lvl4.sprite = levelLocked;
        if (DemoData.Instance.Level5Opened) lvl5.sprite = levelUnlocked;
        else lvl5.sprite = levelLocked;
    }

    public void SettingsOpen()
    {
        if (isBusy)
        {
            return;
        }
        StartCoroutine(nameof(ShowSequence), settingsFrameRect);
    }

    public void SettingsClose()
    {
        if (isBusy)
        {
            return;
        }
        StartCoroutine(nameof(HideSequence), settingsFrameRect);
    }

    public void LevelSelectorOpen()
    {
        if (isBusy)
        {
            return;
        }
        StartCoroutine(nameof(ShowSequence), levelSelectorFrameRect);
    }

    public void LevelSelectorClose()
    {
        if (isBusy)
        {
            return;
        }
        StartCoroutine(nameof(HideSequence), levelSelectorFrameRect);
    }

    private IEnumerator ShowSequence(RectTransform window)
    {
        window.anchoredPosition = new Vector3(0f, startY);
        isBusy = true;
        int frame = 0;

        while (frame < curveValues.Count)
        {
            window.anchoredPosition = new Vector3(0f, startY - 1.5f* startY * curveValues[frame]);
            frame++;
            yield return new WaitForFixedUpdate();
        }
        window.anchoredPosition = new Vector3(0f, - 0.5f * startY);
        isBusy = false;
    }

    private IEnumerator HideSequence(RectTransform window)
    {
        isBusy = true;
        int frame = curveValues.Count - 1;

        while (frame > 0)
        {
            window.anchoredPosition = new Vector3(0f, startY - 1.5f* startY * curveValues[frame]);
            frame--;
            yield return new WaitForFixedUpdate();
        }

        window.anchoredPosition = new Vector3(0f, startY);
        isBusy = false;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < clouds.Count; i++)
        {
            clouds[i].transform.position += Vector3.left * cloudsSpeed[i];
            if (clouds[i].transform.position.x < -30)
            {
                clouds[i].transform.position += new Vector3(60, 0f);
            }
        }
    }

    public void LoadLevelOnIndex(int index)
    {
        if (index == 2 && !DemoData.Instance.Level2Opened)
        {
            return;
        }
        else if (index == 3 && !DemoData.Instance.Level3Opened)
        {
            return;
        }
        else if (index == 4 && !DemoData.Instance.Level4Opened)
        {
            return;
        }
        else if (index == 5 && !DemoData.Instance.Level5Opened)
        {
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneNames[index]);
        }
    }
}
