using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GraberState 
{ 
    Idle,
    GoingDown,
    GoingUp
}

public class Graber : MonoBehaviour
{
    public static Graber instance;

    public Rigidbody2D rb;
    [SerializeField] private GraberState state;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    private float horizontalInputDirection;
    private Vector2 defaultPosition;
    private GrabAbility grubedPlayer;

    private bool isPlayerGrubed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        defaultPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GrabAbility>() != null)
        {
            GrabAbility characterGrab = collision.GetComponent<GrabAbility>();
            if (characterGrab.TryGrab())
            {
                state = GraberState.GoingUp;
                grubedPlayer = characterGrab;
                isPlayerGrubed = true;
            }
        }
        else if (collision.GetComponent<GrabAbility>() == null)
        {
            state = GraberState.GoingUp;
        }
    }

    private void SetHorizontalDirection()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < -0.1)
        {
            horizontalInputDirection = -1;
        }
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x > 0.1)
        {
            horizontalInputDirection = 1;
        }
        else
        {
            horizontalInputDirection = 0;
        }
    }

    private void Update()
    {
        SetHorizontalDirection();

        if (Input.GetMouseButtonDown(0))
        {
            if (state == GraberState.Idle)
            {
                state = GraberState.GoingDown;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isPlayerGrubed)
            {
                grubedPlayer.UnGrab();
                isPlayerGrubed = false;
            }
            else
            {
                state = GraberState.GoingUp;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case GraberState.Idle:
                //rb.velocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x + horizontalInputDirection * horizontalSpeed, 0), 1);
                rb.velocity = new Vector3(horizontalInputDirection * horizontalSpeed, 0, 0);
                break;
            case GraberState.GoingDown:
                rb.velocity = new Vector3(0, -verticalSpeed, 0);
                break;
            case GraberState.GoingUp:
                if (transform.position.y < defaultPosition.y)
                {
                    rb.velocity = new Vector3(0, verticalSpeed, 0);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, defaultPosition.y);
                    state = GraberState.Idle;
                }
                break;
            default:
                break;
        }
    }
}
