using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenuPanel;
    private float timeToCompleteAnim = 1f;
    private float timePassed;
    [SerializeField] private RectTransform frameRect;
    private float startY = 565f;
    private bool isBusy;

    private static readonly List<float> curveValues = new List<float>() // 20 first
        {
            0f, 0f, 0f, 0.004f, 0.01f, 0.015f, 0.05f, 0.07f, 0.09f,
            0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.41f, 0.48f, 0.56f, 0.64f,
            0.78f, 0.84f, 0.88f, 0.93f, 0.97f, 1f, 1.04f, 1.065f, 1.09f,
            1.12f, 1.125f, 1.13f, 1.13f, 1.12f, 1.09f, 1.07f, 1.03f, 1f
        };


    private void OnEnable()
    {
        settingsMenuPanel.SetActive(false);
    }
    public void SettingsOpen()
    {
        if (isBusy)
        {
            return;
        }
        StartCoroutine(nameof(ShowSequence));
    }

    public void SettingsClose()
    {
        if (isBusy)
        {
            return;
        }
        StartCoroutine(nameof(HideSequence));
    }

    private IEnumerator ShowSequence()
    {
        settingsMenuPanel.SetActive(true);
        frameRect.anchoredPosition = new Vector3(0f, startY);
        isBusy = true;
        int frame = 0;

        while (frame < curveValues.Count)
        {
            frameRect.anchoredPosition = new Vector3(0f, startY - 1.5f* startY * curveValues[frame]);
            frame++;
            yield return new WaitForFixedUpdate();
        }
        frameRect.anchoredPosition = new Vector3(0f, - 0.5f * startY);
        isBusy = false;
    }

    private IEnumerator HideSequence()
    {
        isBusy = true;
        int frame = curveValues.Count - 1;

        while (frame > 0)
        {
            frameRect.anchoredPosition = new Vector3(0f, startY - 1.5f* startY * curveValues[frame]);
            frame--;
            yield return new WaitForFixedUpdate();
        }

        frameRect.anchoredPosition = new Vector3(0f, startY);
        settingsMenuPanel.SetActive(false);
        isBusy = false;
        settingsMenuPanel.SetActive(false);
    }
}
