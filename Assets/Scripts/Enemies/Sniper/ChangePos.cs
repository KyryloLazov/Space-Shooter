using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePos : BaseState
{
    public override void EnterState(Sniper sniper, float speed)
    {

    }

    public override void UpdateState(Sniper sniper, float speed)
    {
        Move(sniper, speed);
                  
    }

    public override void ExitState(Sniper sniper, float speed)
    {

    }

    private void Move(Sniper sniper, float speed)
    {
        if (sniper.transform.position.x >= 8f && sniper.transform.position.x < 13f)
        {
            sniper.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (sniper.transform.position.x <= -8f && sniper.transform.position.x > -13f)
        {
            sniper.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            sniper.SwitchState(sniper._move);
        }
    }
}
