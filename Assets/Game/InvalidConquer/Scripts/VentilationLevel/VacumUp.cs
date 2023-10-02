using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumUp : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;
    public Vector3 shootPos;
    public bool isVacuming;

    public void StartVacumUp()
    {
        isVacuming = true;
    }

    private void FixedUpdate()
    {
        if (isVacuming)
        {
            rbPlayer.velocity += new Vector2(0, 1f);
        }
    }

    public IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitForFixedUpdate();
        rbPlayer.transform.position = shootPos;
        rbPlayer.velocity = Vector2.zero;
        isVacuming = false;
        rbPlayer.AddForce(new Vector2(6000f,0));
    }
}
