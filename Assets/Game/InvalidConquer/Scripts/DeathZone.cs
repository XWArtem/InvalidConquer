using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private const string PLAYER = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith(PLAYER))
        {
            LevelManager.instance.ZoneDeath();
        }
    }
}
