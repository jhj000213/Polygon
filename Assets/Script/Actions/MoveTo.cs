using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTo : Action
{
    public MoveTo(GameObject obj,Vector3 targetpos, bool localpos, bool moveparent, float actiontime)
    {
        n_obj = obj;
        n_start = false;
        n_localPos = localpos;
        n_MoveParent = moveparent;
        n_targetPos = targetpos;
        n_actionTime = actiontime;
        timeSpeed = 1.0f / n_actionTime;
    }


    public override bool n_update(float dt)
    {
        CheckStart_To();
        n_nowTime += dt;
        float x = (n_targetPos.x - n_originPos.x) * dt * timeSpeed;
        float y = (n_targetPos.y - n_originPos.y) * dt * timeSpeed;
        Vector3 pos = new Vector3(x, y, 0);
        if (n_MoveParent)
        {
            if (n_localPos)
                n_obj.transform.parent.localPosition += pos;
            else
                n_obj.transform.parent.position += pos;
        }
        else
        {
            if (n_localPos)
                n_obj.transform.localPosition += pos;
            else
                n_obj.transform.position += pos;
        }
        if (n_nowTime >= n_actionTime)
        {
            Correction();
            return true;
        }
        return false;
    }
}
