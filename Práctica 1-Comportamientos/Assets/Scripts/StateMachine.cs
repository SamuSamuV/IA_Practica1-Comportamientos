public class StateMachine
{
    private State _currentState;

    public StateMachine(State currentState)
    {
        UpdateState(currentState);
    }

    public void UpdateMachine()
    {
        this._currentState.Percieve();
        this._currentState.Think();
        this._currentState.Act();
    }

    public void UpdateState(State state)
    {
        this._currentState = state;
    }
}