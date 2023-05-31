using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vueltas : MonoBehaviour
{
    public Transform centerPoint; // Punto central alrededor del cual el enemigo volará
    public float radius = 5f; // Radio del círculo
    public float speed = 2f; // Velocidad de vuelo

    private float angle = 0f;

    private void Update()
    {
        // Calcular la posición en el círculo
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;

        // Actualizar la posición del enemigo
        transform.position = new Vector3(x, y, transform.position.z);

        // Incrementar el ángulo para el siguiente frame
        angle += speed * Time.deltaTime;

        // Asegurarse de que el ángulo esté dentro de los límites de 0 a 2π (círculo completo)
        if (angle >= Mathf.PI * 2)
        {
            angle = 0f;
        }
    }
}