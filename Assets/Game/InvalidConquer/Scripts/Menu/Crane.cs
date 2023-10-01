using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    [SerializeField] private Transform crane;
    private float speed = 0.002f;

    private void Start()
    {
        StartCoroutine(nameof(CraneDown));
    }

    private IEnumerator CraneDown()
    {
        while (crane.transform.position.y > -8)
        {
            crane.transform.position += Vector3.down * speed;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(nameof(CraneUp));
        yield break;
    }

    private IEnumerator CraneUp()
    {
        while (crane.transform.position.y < -4)
        {
            crane.transform.position += Vector3.up * speed;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(nameof(CraneUp));
        yield break;
    }
}
