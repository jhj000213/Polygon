using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MusicSelect : MonoBehaviour
{
    public GameObject _NanidoSelect;
    public ButtonInput _SceneChanger;
    void Start()
    {
        StaticPrefab._MusicName.Clear();
    }

    public void MusicSelectFunc(string musicname, int musicnum)
    {
        if (StaticPrefab._MusicName.Count == 0)
        {
            Debug.Log(musicname);
            StaticPrefab._MusicName.Add(new MusicName { _musicName = musicname, _musicnum = musicnum, _score = 0 });
            _NanidoSelect.SetActive(true);
        }
    }

    public void NanidoSelectFunc_Easy()
    {
        StaticPrefab._MusicName[0]._nanido = false;
        _SceneChanger.SceneChange("GameScene");
    }
    public void NanidoSelectFunc_Hard()
    {
        StaticPrefab._MusicName[0]._nanido = true;
        _SceneChanger.SceneChange("GameScene");
    }
    public void NanidoTableOff()
    {
        StaticPrefab._MusicName.Clear();
        _NanidoSelect.SetActive(false);
    }
    public void GameStart()
    {
        _NanidoSelect.SetActive(false);
    }
}