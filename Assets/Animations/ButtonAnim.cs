using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    public Animator animator;

    public void OnHover()
    {
        animator.SetBool("Hover", true);
    }

    public void OnExit()
    {
        animator.SetBool("Hover", false);
    }
}
