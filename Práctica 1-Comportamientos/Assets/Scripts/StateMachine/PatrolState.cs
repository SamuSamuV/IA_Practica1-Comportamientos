using UnityEngine;

public class PatrolState : State
{
    private Animator animator;

    public PatrolState(StateMachine stateMachine, Animator animator) : base(stateMachine)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Execute()
    {
        if (animator.GetBool("Follow"))
        {
            stateMachine.ChangeState(new FollowState(stateMachine, animator));
        }
        else if (animator.GetBool("Search"))
        {
            stateMachine.ChangeState(new SearchState(stateMachine, animator));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}
