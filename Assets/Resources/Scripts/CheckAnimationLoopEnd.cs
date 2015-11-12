using UnityEngine;
using System.Collections;

public class CheckAnimationLoopEnd : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float state = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (state >= 1f)
        {
            animator.SetTrigger("EndOfAnimation");
        }
    }
}
