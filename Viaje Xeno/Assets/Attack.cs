using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public  int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    Collider2D attackcollider;

    private void Awake()
    {
        attackcollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
       if (damageable != null) 
       {
            damageable.Hit(attackDamage, knockback);
            
       }
    }
}
