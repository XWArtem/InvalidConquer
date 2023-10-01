using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainHero : MonoBehaviour
{
    [SerializeField] private SpriteRenderer faceImg;
    [SerializeField] private Sprite faceIdle, faceScared;
    [SerializeField] private ParticleSystem landingParticles;
    private LayerMask groundLayer = 7;
    private bool isGrounded;

    [System.Obsolete]
    private void OnEnable()
    {
        StaticActions.OnMainHeroGrabbed += ChangeFace;
        landingParticles.loop = false;
        isGrounded = false;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded) return;
        if (collision.gameObject.layer == groundLayer)
        {
            landingParticles.Play();
            isGrounded = true;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isGrounded) return;
    //    if (collision.gameObject.layer == groundLayer)
    //    {
    //        landingParticles.Play();
    //        isGrounded = true;
    //    }
    //}
}
