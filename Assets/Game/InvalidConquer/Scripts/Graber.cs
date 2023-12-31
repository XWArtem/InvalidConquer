using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GraberState 
{ 
    Idle,
    GoingDown,
    GoingUp,
    Grabbing
}

public class Graber : MonoBehaviour
{
    public static Graber instance;

    public Rigidbody2D rb;
    [SerializeField] private GraberState state;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private Transform leftBorder, rightBorder;
    private float horizontalInputDirection;
    private Vector2 defaultPosition;
    private GrabAbility grubedPlayer;
    private bool isBusy;

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
        if (state == GraberState.GoingUp) return;
        if (state == GraberState.Grabbing) return;
        if (state == GraberState.Idle) return;

        if (collision.GetComponent<GrabAbility>() != null)
        {
            GrabAbility characterGrab = collision.GetComponent<GrabAbility>();
            if (characterGrab.TryGrab())
            {
                StartCoroutine(nameof(GrabRoutine), characterGrab);
                state = GraberState.Grabbing;
            }
        }
        else if (collision.GetComponent<GrabAbility>() == null && collision.name.StartsWith("Circle") == false)
        {
            state = GraberState.GoingUp;
        }
    }

    private IEnumerator GrabRoutine(GrabAbility charToGrab)
    {
        GetComponent<Animation>().Play("Grab");
        if (DemoAudioManager.instance != null)
        {
            DemoAudioManager.instance.PlayClipByIndex(2);
        }
        yield return new WaitForSeconds(0.25f);
        state = GraberState.GoingUp;
        grubedPlayer = charToGrab;

        // don't touch it, otherwise there will be a bug
        if (Input.GetMouseButton(0))
        {
            isPlayerGrubed = true;
        }
        else
        {
            grubedPlayer.UnGrab();
            isPlayerGrubed = false;
        }
    }

    public void ForceUngrab()
    {
        if (grubedPlayer)
        {
            grubedPlayer.UnGrab();
            isPlayerGrubed = false;
        }


        state = GraberState.GoingUp;
    }

    private void SetHorizontalDirection()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < -0.2 &&
            transform.position.x > leftBorder.position.x)
        {
            horizontalInputDirection = Mathf.Max(-6, horizontalInputDirection - 0.4f);
        }
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x > 0.2 &&
            transform.position.x < rightBorder.position.x)
        {
            horizontalInputDirection = Mathf.Min(6, horizontalInputDirection + 0.4f);
        }
        else
        {
            if (horizontalInputDirection > 0.8f) horizontalInputDirection -= 0.4f;
            else if (horizontalInputDirection < -0.8f) horizontalInputDirection += 0.4f;
            else horizontalInputDirection = 0f;
        }
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            if (state == GraberState.Idle)
            {
                if (mousePos.x > 1780f && mousePos.x < 1880f && mousePos.y < 1040f && mousePos.y > 940f) return;
                state = GraberState.GoingDown;
                Debug.Log("X: " + mousePos.x);
                Debug.Log("Y: " + mousePos.y);
            }
            if (state == GraberState.GoingDown)
            {
                if (transform.position.y < Camera.main.transform.position.y - 6f)
                {
                    state = GraberState.GoingUp;
                }
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
            if (mousePos.x > 1780f && mousePos.x < 1880f && mousePos.y < 1040f && mousePos.y > 940f) return;
            {
                GetComponent<Animation>().Play("Ungrab");
            }
            
        }
    }

    private void FixedUpdate()
    {
        SetHorizontalDirection();
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
            case GraberState.Grabbing:
                rb.velocity = Vector3.zero;
                break;
            default:
                break;
        }
        //transform.eulerAngles = Vector3.zero;
    }
}
