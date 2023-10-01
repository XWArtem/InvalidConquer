using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilationTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Player"))
        {
            StartCoroutine(collision.GetComponent<VacumUp>().ShootPlayer());
            
        }
    }
}
