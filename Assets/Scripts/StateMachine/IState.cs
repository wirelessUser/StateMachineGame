using StatePattern.Enemy;

public interface IState
{
    public OnePunchManController owner { get; set; }
    public void OnStateEnter();

    public void OnStateExit();

    public void Update();
  
}



