using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum HatchDoorStates 
{ 
    Closing,
    Opening
}

public class HatchDoor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 openedPosition;
    private Vector3 closedPosition;
    private HatchDoorStates state;

    public bool isMove;

    private void Start()
    {
        closedPosition = transform.position;
    }

    public void Open()
    {
        state = HatchDoorStates.Opening;
    }

    public void Close()
    {
        state = HatchDoorStates.Closing;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case HatchDoorStates.Closing:
                transform.position = Vector3.MoveTowards(transform.position, closedPosition, speed);
                break;
            case HatchDoorStates.Opening:
                transform.position = Vector3.MoveTowards(transform.position, openedPosition, speed);
                break;
            default:
                break;
        }
    }
}
