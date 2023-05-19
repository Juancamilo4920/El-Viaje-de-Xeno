using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    public Transform playerTransform; // Transform del jugador
    public float patrolSpeed = 3f; // Velocidad de patrulla del enemigo
    public float chaseSpeed = 6f; // Velocidad de persecuci�n del enemigo
    public float patrolDistance = 5f; // Distancia a recorrer en la patrulla
    public float chaseDistance = 5f; // Distancia de persecuci�n
    public float minDistance = 2f; // Distancia m�nima entre el enemigo y el jugador

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
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseDistance && distanceToPlayer > minDistance)
        {
            isChasing = true;
            isMovingRight = true; // Reiniciar direcci�n de movimiento en persecuci�n
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
            float moveDistance = Mathf.Max(direction.magnitude - minDistance, 0f); // Respetar distancia m�nima
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

                if (transform.position == patrolPosition)
                {
                    isMovingRight = false;
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