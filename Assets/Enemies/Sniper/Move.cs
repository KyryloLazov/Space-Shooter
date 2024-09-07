using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : BaseState
{
    public override void EnterState(Sniper sniper, float speed)
    {

    }

    public override void UpdateState(Sniper sniper, float speed)
    {
        if (sniper.transform.position.x > 8f)
        {
            sniper.transform.position = Vector3.MoveTowards(sniper.transform.position, new Vector3(8f,  sniper.transform.position.y, 0), speed * Time.deltaTime);
        }
        else if(sniper.transform.position.x < -8f)
        {
            sniper.transform.position = Vector3.MoveTowards(sniper.transform.position, new Vector3(-8f, sniper.transform.position.y, 0), speed * Time.deltaTime);
        }
        else
        {
            sniper.SwitchState(sniper._shoot);
        }
    }

    public override void ExitState(Sniper sniper, float speed)
    {

    }
}
