using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerMosquito : MonoBehaviour
{
    
    public float walkSpeed = 3f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(walkSpeed * Vector2.left.x, rb.velocity.y);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
