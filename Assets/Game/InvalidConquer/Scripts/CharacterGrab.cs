using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrab : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public bool isGrabed;
    private Vector3 posOffset;

    private bool CheckIsCanBeGrabbed()
    {
        return true;
    }

    public bool TryGrab()
    {
        if (CheckIsCanBeGrabbed())
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            posOffset = Graber.instance.transform.position - transform.position;
            isGrabed = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UnGrab()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isGrabed = false;
        rb.velocity += new Vector2(Graber.instance.rb.velocity.x * 0.8f, 0f);
    }

    private void LateUpdate()
    {
        if (isGrabed)
        {
            transform.position = Graber.instance.transform.position - posOffset;
        }
    }
}
