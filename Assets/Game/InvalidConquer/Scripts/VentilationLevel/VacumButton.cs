using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumButton : MonoBehaviour
{
    [SerializeField] private VacumUp attachedVacum;
    [SerializeField] private GameObject plateSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GrabAbility>() != null)
        {
            plateSprite.transform.position = new Vector3(plateSprite.transform.position.x, plateSprite.transform.position.y - 0.111f, plateSprite.transform.position.z);
            attachedVacum.StartVacumUp();
        }
    }
}
