using UnityEngine;

public class FollowState : State
{
    private Animator animator;

    public FollowState(StateMachine stateMachine, Animator animator) : base(stateMachine)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        Debug.Log("Entering Follow State");
    }

    public override void Execute()
    {
        if (animator.GetBool("Patroll"))
        {
            stateMachine.ChangeState(new PatrolState(stateMachine, animator));
        }
        else if (animator.GetBool("Search"))
        {
            stateMachine.ChangeState(new SearchState(stateMachine, animator));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Follow State");
    }
}
