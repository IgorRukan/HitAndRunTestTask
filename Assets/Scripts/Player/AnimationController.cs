using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    public void Stay()
    {
        animator.SetTrigger("Stay");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Shoot");
    }
    
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
        animator.ResetTrigger("Walk");
    }
    
    public void Walk()
    {
        animator.SetTrigger("Walk");
        animator.ResetTrigger("Stay");
        animator.ResetTrigger("Shoot");
    }
}
