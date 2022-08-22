using UnityEngine;

public static class AnimID
{
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Move = Animator.StringToHash("Move");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int FindEnemy = Animator.StringToHash("FindEnemy");
}