using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDinamyteBoost : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Explode()
    {
        // distance = transform.position.x - self.transform.position.x;
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector2(1000,750));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Explode();
        }
    }
}
