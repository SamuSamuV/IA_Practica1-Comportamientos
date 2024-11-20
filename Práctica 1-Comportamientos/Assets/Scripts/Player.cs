using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject soundCollider;
    public Rigidbody rb;

    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        InstanciarDeteccionRuido();
    }

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            movement = new Vector3(moveX, 0f, moveY).normalized;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            Quaternion additionalRotation = Quaternion.Euler(0, -90, 0);
            transform.rotation = targetRotation * additionalRotation;
        }
    }

    public void InstanciarDeteccionRuido()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            soundCollider.SetActive(true);

        else if(Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
            soundCollider.SetActive(false);
    }
}