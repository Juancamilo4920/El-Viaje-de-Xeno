using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    SpriteRenderer spriterenderer;
    GameObject objtoremove;
    Color startColor;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        spriterenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriterenderer.color;
        objtoremove = animator.gameObject;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        
        float newAlpha =startColor.a * (1 - ( timeElapsed / fadeTime ));

        spriterenderer.color = new Color(startColor.r, startColor.g, startColor.b);
        
        if (timeElapsed > fadeTime) 
        {
            Destroy(objtoremove);
        }
    }
}
