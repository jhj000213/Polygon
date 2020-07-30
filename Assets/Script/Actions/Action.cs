using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Action : MonoBehaviour
{

    //static public List<Action> n_List = new List<Action>();
    public GameObject n_obj;
    public bool n_MoveParent;
    public bool n_localPos;
    public bool n_start;
    public Vector3 n_originPos = new Vector3();
    public Vector3 n_targetPos = new Vector3();
    public Vector3 n_addPos = new Vector3();
    public Vector3 n_previousPos = new Vector3();
    public float n_actionTime = 0.0f;
    public float n_nowTime = 0.0f;
    public float distance_x;
    public float distance_y;
    public float distance;
    public float timeSpeed;
    public float _width;
    public float _height;
    virtual public bool n_update(float dt) { return false; }
    public void Correction()
    {
        if (n_MoveParent)
        {
            if (n_localPos)
                n_obj.transform.parent.localPosition = n_targetPos;
            else
                n_obj.transform.parent.position = n_targetPos;
        }
        else
        {
            if (n_localPos)
                n_obj.transform.localPosition = n_targetPos;
            else
                n_obj.transform.position = n_targetPos;
        }
    }
    public void CheckStart_To()
    {
        if (!n_start)
        {
            n_start = true;
            if (n_MoveParent)
            {
                if (n_localPos)
                    n_originPos = n_obj.transform.parent.localPosition;
                else
                    n_originPos = n_obj.transform.parent.position;
            }
            else
            {
                if (n_localPos)
                    n_originPos = n_obj.transform.localPosition;
                else
                    n_originPos = n_obj.transform.position;
            }

        }
    }
    public void CheckStart_By()
    {
        if (!n_start)
        {
            n_start = true;
            if (n_MoveParent)
            {
                if (n_localPos)
                    n_previousPos = n_obj.transform.parent.localPosition;
                else
                    n_previousPos = n_obj.transform.parent.position;
            }
            else
            {
                if (n_localPos)
                    n_previousPos = n_obj.transform.localPosition;
                else
                    n_previousPos = n_obj.transform.position;
            }
            if (n_MoveParent)
            {
                if (n_localPos)
                    n_originPos = n_obj.transform.parent.localPosition;
                else
                    n_originPos = n_obj.transform.parent.position;
            }
            else
            {
                if (n_localPos)
                    n_originPos = n_obj.transform.localPosition;
                else
                    n_originPos = n_obj.transform.position;
            }
            //Debug.Log("originY = "+n_originPos.y);
            n_targetPos = n_originPos + n_addPos;
            //Debug.Log(n_targetPos.y);
            //Debug.Log(n_addPos.y);
            _width = n_targetPos.x - n_originPos.x;
        }
    }
}
