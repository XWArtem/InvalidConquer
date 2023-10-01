using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainHero : MonoBehaviour
{
    [SerializeField] private SpriteRenderer faceImg;
    [SerializeField] private Sprite faceIdle, faceScared;
    [SerializeField] private ParticleSystem landingParticles;
    [SerializeField] private GameObject exclamation0, exclamation1;
    private LayerMask groundLayer = 7;
    private bool isGrounded;
    private bool isScared = false;

    [System.Obsolete]
    private void OnEnable()
    {
        StaticActions.OnMainHeroGrabbed += ChangeFace;
        landingParticles.loop = false;
        isGrounded = false;
        exclamation0.SetActive(false);
        exclamation1.SetActive(false);
    }

    private void OnDisable()
    {
        StaticActions.OnMainHeroGrabbed -= ChangeFace;
    }

    private void ChangeFace(bool isScared)
    {
        if (isScared) isGrounded = false;
        StartCoroutine(nameof(ChangeFaceDelayed), isScared);
    }

    private void FixedUpdate()
    {
        if (isScared)
        {
            if (Random.Range(0, 200) == 0)
            {
                exclamation0.SetActive(!exclamation0.activeSelf);
            }
            else if (Random.Range(0, 300) == 0)
            {
                exclamation1.SetActive(!exclamation0.activeSelf);
            }
        }
        else
        {
            exclamation0.SetActive(false);
            exclamation1.SetActive(false);
        }
    }

    private IEnumerator ChangeFaceDelayed(bool isScared)
    {
        if (isScared)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
        faceImg.sprite = isScared ? faceScared : faceIdle;
        this.isScared = isScared;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGrounded) return;
        if (collision.gameObject.layer == groundLayer)
        {
            landingParticles.Play();
            isGrounded = true;
            if (DemoAudioManager.instance != null)
            {
                DemoAudioManager.instance.PlayClipByIndex(1);
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (isGrounded) return;
    //    if (collision.gameObject.layer == groundLayer)
    //    {
    //        landingParticles.Play();
    //        isGrounded = true;
    //        if (DemoAudioManager.instance != null)
    //        {
    //            DemoAudioManager.instance.PlayClipByIndex(1);
    //        }
    //    }
    //}
}
