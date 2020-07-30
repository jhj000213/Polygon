using UnityEngine;
using System.Collections;

public class JumpBy : Action
{

    float _width;

    Vector3 _originPos;
    public JumpBy(GameObject obj, Vector2 addpos, float height, bool localpos, bool moveparent, float actiontime)
    {
        n_obj = obj;

        _height = height;
        n_start = false;
        n_localPos = localpos;
        n_MoveParent = moveparent;
        n_addPos = addpos;
        n_actionTime = actiontime;
        timeSpeed = 1.0f / n_actionTime;
    }

    public override bool n_update(float dt)
    {
        CheckStart_By();
        n_nowTime += dt;
        _addHeight(n_nowTime * timeSpeed);

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
        y += n_addPos.y * t;
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
        Vector3 diff = currentPos - n_previousPos;
        Vector3 newPos = (n_originPos + diff + new Vector3(x, y, n_originPos.z));
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
        n_previousPos = newPos;
    }
}
