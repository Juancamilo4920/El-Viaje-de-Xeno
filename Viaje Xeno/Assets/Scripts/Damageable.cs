using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    
    public int MaxHealth
    {
        get 
        { 
            return _maxHealth; 
        }
        set 
        { 
            _maxHealth = value; 
        }
    }
    [SerializeField]
    private int _health = 100;

    public int Health
    { 
        get 
        { 
            return _health; 
        } 
        set 
        {  
            _health = value; 
            // si vida llega a 0 muere
            if (_health <= 0)
            {
                IsAlive = false;

            }
        } 
    }
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible= false;
    private float timeSinceHit = 0;
    public float invincibilitytime = 0.25f;

    public bool IsAlive
    {
        get 
        { 
            return _isAlive; 
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }

    }
    private void Awake()   
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        if (isInvincible) 
        { 
            if(timeSinceHit > invincibilitytime) 
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public void Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
    public void Heal(int healthRestore)
    {
        if (IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            if (MaxHealth == Health)
            {
                Health += 0;

            }
            else if (MaxHealth > Health)
            {
                Health += actualHeal;

            }

        }
    }
}
