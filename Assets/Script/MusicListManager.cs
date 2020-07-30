using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicListManager : MonoBehaviour
{

    public List<string> _MusicName = new List<string>();
    public GameObject _GridObj;
    public GameObject _BlockTemp;
    //public MusicSelect _MusicList;

    void Start()
    {
        MakeBlock();
    }

    void MakeBlock()
    {
        for (int i = 0; i < _MusicName.Count; i++)
        {

            GameObject obj = NGUITools.AddChild(_GridObj, _BlockTemp);
            obj.transform.Find("MusicName").GetComponent<UILabel>().text = _MusicName[i];
            obj.GetComponent<MusicInfo>()._MusicName = _MusicName[i];
            obj.GetComponent<MusicInfo>()._MusicNum = i;
            obj.transform.localPosition = new Vector3(0, -140 * i, 0);
        }
    }
}
