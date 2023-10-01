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
    [SerializeField] private float overallTimeForLvl;
    [SerializeField] private Image blackTint;
    private float timeLeft;
    private bool isGrabbing;

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

        StartCoroutine(nameof(BlackTintSmoothAnim), false);
    }

    private void OnEnable()
    {
        StaticActions.OnMainHeroGrabbed += UpdateGrabbingState;
    }

    private void OnDisable()
    {
        StaticActions.OnMainHeroGrabbed -= UpdateGrabbingState;
    }

    private void UpdateGrabbingState(bool isGrabbing) => this.isGrabbing = isGrabbing;

    public void FinishLevel()
    {
        Time.timeScale = 0f;
        //popup
    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        StartCoroutine(nameof(BlackTintSmoothAnim), true);
        //popup
    }

    private void UpdateTimerTxt()
    {
        timerText.text = ((int)timeLeft).ToString();
    }

    private void Update()
    {
        if (isGrabbing)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerTxt();
            if (timeLeft <= 0f)
            {
                Graber.instance.ForceUngrab();
                StartCoroutine(nameof(GameOverDelayed));
            }
        }
    }

    private IEnumerator GameOverDelayed()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(nameof(BlackTintSmoothAnim), true);
        
    }

    private IEnumerator BlackTintSmoothAnim(bool setActive)
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

            GameOver();
            yield return null;
        }
        else
        {
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
