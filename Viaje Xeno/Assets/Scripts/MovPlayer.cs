using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 1;

    private int jumpsRemaining;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
        isGrounded = true;
    }

    private void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");

        // Mueve al personaje horizontalmente
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);


        // Gira al personaje hacia la direcci�n que se est� moviendo
        if (moveDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
        }

        // Cambia la animaci�n del personaje
        anim.SetFloat("Horizontal", Mathf.Abs(moveDirection));

        // Salta si a�n hay saltos disponibles y est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0 && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpsRemaining--;
        }
    }

    // Restablece el recuento de saltos cuando el personaje toca el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
            anim.SetBool("Grounded", isGrounded);
        }
    }

    // Detecta cuando el personaje deja de tocar el suelo
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
        }
    }
}


