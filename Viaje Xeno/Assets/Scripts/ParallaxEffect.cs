using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Posición de inicio del objeto que recibirá el efecto 
    Vector2 StartingPosition;

    //Valor Z inicial del objeto
    float startingZ;

    // Distancia que se ha movido la cámara desde el punto de inicio del objeto de paralaje
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - StartingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //Entre más lejos esté el objeto del jugador, más se moverá este primero. Entre más cerca el valor Z al jugador, significará un efecto más lento
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = StartingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
