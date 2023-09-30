using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private PlayerDinamyteBoost playerDinamyteBoost;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("OGNIVO"))
        {
            playerDinamyteBoost.Explode();
        }
    }
}
