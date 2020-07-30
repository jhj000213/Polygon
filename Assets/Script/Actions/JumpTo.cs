using UnityEngine;
using System.Collections;

public class JumpTo : Action
{
    float _height;
    Vector3 _previousPos;
    Vector3 _originPos;
    public JumpTo(GameObject obj,Vector3 targetpos, float height, bool localpos, bool moveparent, float actiontime)
    {
        n_obj = obj;
        if (n_MoveParent)
        {
            if (n_localPos)
                _previousPos = n_obj.transform.parent.localPosition;
            else
                _previousPos = n_obj.transform.parent.position;
        }
        else
        {
            if (n_localPos)
                _previousPos = n_obj.transform.localPosition;
            else
                _previousPos = n_obj.transform.position;
        }
        _height = height;
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
        _addHeight(n_nowTime*timeSpeed);

        if (n_nowTime >= n_actionTime)
        {
            Correction();
            return true;
        }
        return false;
    }
    void _addHeight(float t)
    {
        float frac = Mathf.Repeat(t, 1.0f);
        float x = _width * t;
        float y = _height * 4 * frac * (1.0f - frac);
        y += n_targetPos.y * t;
        Vector3 currentPos;
        if (n_MoveParent)
        {
            if (n_localPos)
                currentPos = n_obj.transform.parent.localPosition;
            else
                currentPos = n_obj.transform.parent.position;
        }
        else
        {
            if (n_localPos)
                currentPos = n_obj.transform.localPosition;
            else
                currentPos = n_obj.transform.position;
        }
        Vector3 diff = currentPos - _previousPos;
        Vector3 newPos = (_originPos + diff + new Vector3(x, y, 0));
        if (n_MoveParent)
        {
            if (n_localPos)
                n_obj.transform.parent.localPosition = newPos;
            else
                n_obj.transform.parent.position = newPos;
        }
        else
        {
            if (n_localPos)
                n_obj.transform.localPosition = newPos;
            else
                n_obj.transform.position = newPos;
        }
        _previousPos = newPos;
    }
}
