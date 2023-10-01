using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private PlayerDinamyteBoost playerDinamyteBoost;
    [SerializeField] private GameObject fetil;
    [SerializeField] private GameObject TNTBarrels;
    [SerializeField] private ParticleSystem boomParticles;

    [System.Obsolete]
    private void Awake()
    {
        fetil.SetActive(false);
        boomParticles.loop = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("OGNIVO"))
        {
            StartCoroutine(nameof(ExplodeDelayed));
        }
    }

    private IEnumerator ExplodeDelayed()
    {
        fetil.SetActive(true);
        yield return new WaitForSeconds(2f);
        boomParticles.Play();
        if (DemoAudioManager.instance != null)
        {
            DemoAudioManager.instance.PlayClipByIndex(3);
        }
        playerDinamyteBoost.Explode(transform);
        TNTBarrels.SetActive(false);
    }
}
