using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private const string PLAYER = "Player";

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y - 5.6f, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith(PLAYER))
        {
            LevelManager.instance.ZoneDeath();
        }
    }
}
