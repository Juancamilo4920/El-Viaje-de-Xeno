using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingenemiesç : MonoBehaviour
{
    public Transform playerTransform; // Transform del jugador
    public float patrolSpeed = 3f; // Velocidad de patrulla del enemigo
    public float chaseSpeed = 4f; // Velocidad de persecución del enemigo
    public float patrolDistance = 5f; // Distancia a recorrer en la patrulla
    public float chaseDistance = 5f; // Distancia de persecución
    public float minDistance = 1f; // Distancia mínima entre el enemigo y el jugador
    public float maxVerticalDistance = 3f; // Altura máxima permitida para el enemigo
    public Transform PersonajePrincipal; // Transform del objeto A
    public Transform Enemigo; // Transform del objeto B
    private Vector3 initialPosition; // Posición inicial individual del enemigo
    private Vector3 patrolPosition; // Posición de patrulla
    private bool isChasing = false; // Indicador de persecución
    private bool isMovingRight = true; // Indicador de dirección de movimiento
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
       
        // Verificar si el jugador está dentro de la distancia de persecución y dentro del rango vertical permitido
        if (distanceToPlayer <= chaseDistance && Mathf.Abs(transform.position.y - playerTransform.position.y) <= maxVerticalDistance)
        {
            isChasing = true;
            isMovingRight = true; // Reiniciar dirección de movimiento en persecución
            currentSpeed = chaseSpeed; // Utilizar velocidad de persecución
        }
        else
        {
            isChasing = false;
            currentSpeed = patrolSpeed; // Utilizar velocidad de patrulla
        }

        if (isChasing)
        {
            // Calcular la dirección hacia el jugador y moverse en esa dirección
            Vector3 direction = playerTransform.position - transform.position;
            float moveDistance = Mathf.Max(direction.magnitude - minDistance, 0f); // Respetar distancia mínima
            direction.Normalize();
            transform.Translate(direction * moveDistance * currentSpeed * Time.deltaTime);
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
    }
}