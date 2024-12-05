using UnityEngine;

public class SearchState : State
{
    private Animator animator;

    public SearchState(StateMachine stateMachine, Animator animator) : base(stateMachine)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        Debug.Log("Entering Search State");
    }

    public override void Execute()
    {
        if (animator.GetBool("Follow"))
        {
            stateMachine.ChangeState(new FollowState(stateMachine, animator));
        }
        else if (animator.GetBool("Patroll"))
        {
            stateMachine.ChangeState(new PatrolState(stateMachine, animator));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Search State");
    }
}
