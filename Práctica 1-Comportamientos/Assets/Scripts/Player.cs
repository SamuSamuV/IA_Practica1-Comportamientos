using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Rigidbody rb;

    private Vector3 movement;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movimiento();
    }

    void Movimiento()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector3(moveX, 0f, moveY).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        this.transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0.0f);
    }
}