using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StaticPrefab
{

    public static List<MusicName> _MusicName = new List<MusicName>();
}

public class MusicName
{
    public string _musicName;
    public int _musicnum;
    public int _score;
    public bool _nanido;
}   