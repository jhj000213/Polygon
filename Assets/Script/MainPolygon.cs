using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainPolygon : MonoBehaviour
{

    public int _nowVertexNum;
    public int _verListNum;//tem
    public int _Level;
    public List<int> _CanVertexNumList = new List<int>();
    UISprite _MainFrame;
    public UISprite _SubFrame;
    public GameObject _ChangeEffect;
    public UISprite[] _NowPolygonsList;

    public float _RotateSpeed;

    public GameObject _SubPolygon;
    public Animator _ani;
    public Animator _subani;

    void Start()
    {
        _CanVertexNumList.Add(3);
        _CanVertexNumList.Add(4);
        _nowVertexNum = 3;
        _verListNum = 0;
        _Level = 1;
        _MainFrame = GetComponent<UISprite>();
    }

      public void HitAnimation()
    {
        _ani.SetTrigger("hittrigger");
        _subani.SetTrigger("hittrigger_sub");
    }

    void Update()
    {
        _MainFrame.spriteName = "block" + _nowVertexNum.ToString();
        _SubFrame.spriteName = "block" + _nowVertexNum.ToString();

        InforPolygonListUpdate();

        if(!GameMng.Data._Pause)
        {
            transform.localEulerAngles += new Vector3(0, 0, _RotateSpeed * Time.smoothDeltaTime);
            _SubPolygon.transform.localEulerAngles += new Vector3(0, 0, _RotateSpeed * Time.smoothDeltaTime);
        }
        
    }

    void InforPolygonListUpdate()
    {
        if (_Level <= 4)
        {
            _NowPolygonsList[0].spriteName = "mini_" + _CanVertexNumList[0];
            _NowPolygonsList[1].spriteName = "mini_" + _CanVertexNumList[1];
            _NowPolygonsList[2].enabled = false;
        }
        else
        {
            _NowPolygonsList[0].spriteName = "mini_" + _CanVertexNumList[0];
            _NowPolygonsList[1].spriteName = "mini_" + _CanVertexNumList[1];
            _NowPolygonsList[2].spriteName = "mini_" + _CanVertexNumList[2];
            _NowPolygonsList[2].enabled = true;
        }
    }

    public void PolygonUpgrade()
    {
        _Level++;
        _CanVertexNumList.Clear();
        _verListNum = 0;
        if (_Level == 2)
        {
            _CanVertexNumList.Add(4);
            _CanVertexNumList.Add(5);
            ChangeVertex(4);
        }
        if (_Level == 3)
        {
            _CanVertexNumList.Add(5);
            _CanVertexNumList.Add(6);
            ChangeVertex(5);
        }
        if (_Level == 4)
        {
            _CanVertexNumList.Add(6);
            _CanVertexNumList.Add(7);
            ChangeVertex(6);
        }
        if (_Level >= 5)
        {
            //int num1 = Random.Range(3, 8);
            //int num2 = Random.Range(3, 8);
            //while (num1 == num2)
            //    num2 = Random.Range(3, 8);
            //int num3 = Random.Range(3, 8);
            //while (num1 == num3 || num2 == num3)
            //    num3 = Random.Range(3, 8);
            //_CanVertexNumList.Add(num1);
            //_CanVertexNumList.Add(num2);
            //_CanVertexNumList.Add(num3);
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = i + 1; j < 3; j++)
            //    {
            //        if (_CanVertexNumList[j] < _CanVertexNumList[i])
            //        {
            //            int temp = _CanVertexNumList[i];
            //            _CanVertexNumList[i] = _CanVertexNumList[j];
            //            _CanVertexNumList[j] = temp;
            //        }
            //    }
            //}
            //if (_CanVertexNumList[0] == 3 && _CanVertexNumList[1] == 6)
            //{
            //    num1 = Random.Range(4, 8);
            //    while (num1 == _CanVertexNumList[2] && num1 == 6)
            //        num1 = Random.Range(4, 8);
            //    _CanVertexNumList[1] = num1;
            //}
            //else if (_CanVertexNumList[0] == 3 && _CanVertexNumList[2] == 6)
            //{
            //    num1 = Random.Range(4, 8);
            //    while (num1 == _CanVertexNumList[1] && num1 == 6)
            //        num1 = Random.Range(4, 8);
            //    _CanVertexNumList[2] = num1;
            //}

            int[] num = new int[3];
            num[0] = Random.Range(3, 8);
            num[1] = Random.Range(3, 8);
            while (num[0] == num[1])
                num[1] = Random.Range(3, 8);
            num[2] = Random.Range(3, 8);
            while (num[0] == num[2] || num[1] == num[2])
                num[2] = Random.Range(3, 8);
            Debug.Log("r1");
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (num[j] < num[i])
                    {
                        int temp = num[i];
                        num[i] = num[j];
                        num[j] = temp;
                    }
                }
            }
            Debug.Log("r2");
            if (num[0] == 3)
            {
                if (num[1] == 6)
                {
                    while (num[1] == 6 || num[1] == num[2])
                        num[1] = Random.Range(4, 8);
                }
                else if (num[2] == 6)
                {
                    while (num[2] == 6 || num[2] == num[1])
                        num[2] = Random.Range(4, 8);
                }
            }
            Debug.Log("r3");
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (num[j] < num[i])
                    {
                        int temp = num[i];
                        num[i] = num[j];
                        num[j] = temp;
                    }
                }
            }
            Debug.Log("r4");
            _CanVertexNumList.Clear();
            for (int i = 0; i < 3; i++)
                _CanVertexNumList.Add(num[i]);
            Debug.Log("r5");
            ChangeVertex(_CanVertexNumList[0]);
        }
    }

    public void ChangeVertex()
    {

        GameObject eff = NGUITools.AddChild(GameMng.Data._Root, _ChangeEffect);
    }

    void ChangeVertex(int setver)
    {
        GameObject eff = NGUITools.AddChild(GameMng.Data._Root, _ChangeEffect);
        _nowVertexNum = setver;
    }

    public void VertexUp()
    {
        ChangeVertex();
        _verListNum++;
        if (_CanVertexNumList.Count <= _verListNum)
            _verListNum = 0;
        _nowVertexNum = _CanVertexNumList[_verListNum];
    }
    public void VertexDown()
    {
        ChangeVertex();
        _verListNum--;
        if (0 > _verListNum)
            _verListNum = _CanVertexNumList.Count - 1;
        _nowVertexNum = _CanVertexNumList[_verListNum];
    }
}
