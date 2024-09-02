using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(Sniper sniper, float speed);

    public abstract void UpdateState(Sniper sniper, float speed);

    public abstract void ExitState(Sniper sniper, float speed);
}
