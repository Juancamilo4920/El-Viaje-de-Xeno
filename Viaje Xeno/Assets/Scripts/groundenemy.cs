using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundenemy : MonoBehaviour
{
    public Transform playerTransform; // Transform del jugador
    public float patrolSpeed = 3f; // Velocidad de patrulla del enemigo
    public float chaseSpeed = 6f; // Velocidad de persecuci�n del enemigo
    public float patrolDistance = 5f; // Distancia a recorrer en la patrulla
    public float chaseDistance = 5f; // Distancia de persecuci�n
    public float minDistance = 1f; // Distancia m�nima entre el enemigo y el jugador
    public Transform PersonajePrincipal; // Transform del objeto A
    public Transform Enemigo; // Transform del objeto B
    private Vector3 initialPosition; // Posici�n inicial individual del enemigo
    private Vector3 patrolPosition; // Posici�n de patrulla
    private bool isChasing = false; // Indicador de persecuci�n
    private bool isMovingRight = true; // Indicador de direcci�n de movimiento
    private float currentSpeed; // Velocidad actual del enemigo

    private void Start()
    {
        initialPosition = transform.position;
        patrolPosition = initialPosition + new Vector3(patrolDistance, 0f, 0f);
        currentSpeed = patrolSpeed;
    }

    private void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (PersonajePrincipal.position.x < Enemigo.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (PersonajePrincipal.position.x > Enemigo.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            // Los objetos est�n en la misma posici�n en el eje X
            Debug.Log("Objeto A y objeto B est�n en la misma posici�n en el eje X");
        }

        // Verificar si el jugador est� dentro de la distancia de persecuci�n
        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;
            isMovingRight = (playerTransform.position.x > transform.position.x); // Determinar direcci�n de movimiento hacia el jugador
            currentSpeed = chaseSpeed; // Utilizar velocidad de persecuci�n
        }
        else
        {
            isChasing = false;
            currentSpeed = patrolSpeed; // Utilizar velocidad de patrulla
        }

        if (isChasing)
        {
            // Calcular la direcci�n hacia el jugador y moverse en esa direcci�n
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f; // Ignorar el movimiento vertical
            direction.Normalize();
            transform.Translate(direction * currentSpeed * Time.deltaTime);
        }
        else
        {
            // Realizar la patrulla de izquierda a derecha con velocidad de patrulla
            float step = currentSpeed * Time.deltaTime;

            if (isMovingRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPosition, step);
                spriteRenderer.flipX = true;

                if (transform.position == patrolPosition)
                {
                    isMovingRight = false;
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);

                if (transform.position == initialPosition)
                {
                    isMovingRight = true;
                }
            }
        }

        // Desactivar capacidad de salto del enemigo
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
    }
}