using System;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    public event Action AttackAnimationFinished;
    public event Action AbilityAnimationFinished;

    public void AttackFinished(int i)
    {
        AttackAnimationFinished?.Invoke();
    }

    public void AbilityFinished(int i)
    {
        AbilityAnimationFinished?.Invoke();
    }
}