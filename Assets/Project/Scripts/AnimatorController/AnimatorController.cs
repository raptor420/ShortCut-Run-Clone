using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
   public void StateIdle()
    {
        animator.applyRootMotion = true;
        animator.Play("IdleState");
    }
    public void StateRun()
    {
        //animator.applyRootMotion = false;
        animator.applyRootMotion = false;

        animator.Play("Run");


    }
    public void StateCarry()
    {
        animator.applyRootMotion = false;

        animator.Play("Carry");

    }
    public void StateJump()
    {
        animator.applyRootMotion = false;

        animator.Play("Jump");


    }
    public void StateDeath()
    {

        animator.applyRootMotion = false;
        animator.Play("Fall");


    }

    public void StateVictory()
    {
        animator.applyRootMotion = false;
        animator.Play("Victory");
    } 
    public void StateLose()
    {
      //  animator.applyRootMotion = true;
        animator.Play("Defeat");
    }
    public void StateThrow()
    {
        animator.applyRootMotion = false;
        animator.Play("Throw");
    }
}
