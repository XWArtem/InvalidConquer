using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum LevelStates
{ 
    PlayState,
    Finish
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerClockImg;
    [SerializeField] private float overallTimeForLvl;
    [SerializeField] private Image blackTint;
    [SerializeField] private RectTransform gameOverWindow;
    private float timeLeft;
    private bool isGrabbing;
    private float gameOverWindowStartY = 450f;
    private bool gameIsOn;

    private static readonly List<float> curveValues = new List<float>() // 20 first
        {
            0f, 0f, 0f, 0.004f, 0.01f, 0.015f, 0.05f, 0.07f, 0.09f,
            0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.41f, 0.48f, 0.56f, 0.64f,
            0.78f, 0.84f, 0.88f, 0.93f, 0.97f, 1f, 1.04f, 1.065f, 1.09f,
            1.12f, 1.125f, 1.13f, 1.13f, 1.12f, 1.09f, 1.07f, 1.03f, 1f
        };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        isGrabbing = false;
        timeLeft = overallTimeForLvl;
        UpdateTimerTxt();
        gameIsOn = true;
    }

    private void OnEnable()
    {
        StaticActions.OnMainHeroGrabbed += UpdateGrabbingState;
    }

    private void OnDisable()
    {
        StaticActions.OnMainHeroGrabbed -= UpdateGrabbingState;
    }

    private void Start()
    {
        StartCoroutine(nameof(BlackTintSmoothAnim), false);
    }

    private void UpdateGrabbingState(bool isGrabbing) => this.isGrabbing = isGrabbing;

    public void FinishLevel()
    {
        Time.timeScale = 0f;
        //popup
    }

    

    public IEnumerator GameOverView()
    {
        Debug.Log("GameOver");
        //popup
        gameOverWindow.anchoredPosition = new Vector3(0f, gameOverWindowStartY);
        int frame = 0;

        while (frame < curveValues.Count)
        {
            gameOverWindow.anchoredPosition = new Vector3(0f, gameOverWindowStartY - 2f * gameOverWindowStartY * curveValues[frame]);
            frame++;
            yield return new WaitForFixedUpdate();
        }
        gameOverWindow.anchoredPosition = new Vector3(0f, -gameOverWindowStartY);
    }

    private void UpdateTimerTxt()
    {
        timerText.text = ((int)timeLeft).ToString();
        timerClockImg.fillAmount = timeLeft / overallTimeForLvl;
    }

    private void Update()
    {
        if (isGrabbing)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerTxt();
            if (timeLeft <= 0f && gameIsOn)
            {
                Graber.instance.ForceUngrab();
                StartCoroutine(nameof(GameOverDelayed));
                gameIsOn = false;
            }
        }
    }

    private IEnumerator GameOverDelayed()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(nameof(BlackTintSmoothAnim), true); 
    }

    public void ZoneDeath()
    {
        if (gameIsOn)
        {
            StartCoroutine(nameof(BlackTintSmoothAnim), true);
            gameIsOn = false;
        }
    }

    public IEnumerator BlackTintSmoothAnim(bool setActive)
    {
        if (setActive)
        {
            blackTint.gameObject.SetActive(true);
            byte a = 0;
            blackTint.color = new Color32(55, 55, 55, a);
            while (a <= 249)
            {
                a += 5;
                blackTint.color = new Color32(55, 55, 55, a);
                yield return new WaitForFixedUpdate();
            }
            blackTint.color = new Color32(55, 55, 55, 255);

            StartCoroutine(nameof(GameOverView));
            yield return null;
        }
        else
        {
            blackTint.gameObject.SetActive(true);
            byte a = 255;
            blackTint.color = new Color32(55, 55, 55, a);
            while (a > 6)
            {
                a -= 5;
                blackTint.color = new Color32(55, 55, 55, a);
                yield return new WaitForFixedUpdate();
            }
            blackTint.color = new Color32(55, 55, 55, 0);
            blackTint.gameObject.SetActive(false);
            yield return null;
        }
    }
}
