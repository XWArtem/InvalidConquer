using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAbility : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private WheelJoint2D[] wheelJoints;
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
            if (wheelJoints.Length > 0)
            {
                SetDefaultAngle();
            }
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (wheelJoints.Length > 0) 
            {
                foreach (var item in wheelJoints)
                { // For stop wheel rotating. Not working
                    item.GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void SetDefaultAngle()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void UnGrab()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector2.zero;
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
