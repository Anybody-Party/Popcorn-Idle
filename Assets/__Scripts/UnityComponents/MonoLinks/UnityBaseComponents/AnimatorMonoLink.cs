using Leopotam.Ecs;
using UnityEngine;

public class AnimatorMonoLink : MonoLink<AnimatorLink>
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        if (Value.Value == null)
            Value = new AnimatorLink { Value = animator };
    }
}