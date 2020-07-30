using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class NodeGenerator : MonoBehaviour {

    public GameObject _Node;
    public GameObject _Parent;
    public float _DelayTime;
    public float _nowTime;

    void Start()
    {
        _nowTime = _DelayTime;
        _Parent = GameMng.Data._MainPolygon;
    }

    void Update()
    {
        if(!GameMng.Data._Pause && GameMng.Data._NodeStart && !GameMng.Data._ChangeingRound)
        {
            _nowTime += Time.smoothDeltaTime;
            if(_nowTime>=_DelayTime)
            {
                MakeNode();
                _nowTime -= _DelayTime;
            }
        }
    }

    void MakeNode()
    {
        int num = Random.Range(0, _Parent.GetComponent<MainPolygon>()._CanVertexNumList.Count);
        int nowver = _Parent.GetComponent<MainPolygon>()._CanVertexNumList[num];

        TextAsset pattun = Resources.Load<TextAsset>("Pattuns/" + nowver.ToString());
        
        List<string> LineList = new List<string>();
        StreamReader sr = new StreamReader(new MemoryStream(pattun.bytes));
        while(sr.Peek() >=0)
            LineList.Add(sr.ReadLine());
        int listrand = Random.Range(0, LineList.Count);
        char[] patarr = LineList[listrand].ToCharArray();


        for (int i = 0; i < nowver; i++)
        {
            if(patarr[i]=='1')
            {
                GameObject obj = NGUITools.AddChild(_Parent, _Node);
                obj.transform.localEulerAngles = new Vector3(0, 0, i * (360 / nowver));
            }
        }
        GameMng.Data._nowNodeNum.Add(nowver);

    }
}
