using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameMng : MonoBehaviour
{

    private static GameMng instance = null;

    public static GameMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(GameMng)) as GameMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    float _ProgressingTime;
    /// <summary>
    /// 0 - notStart
    /// 1 - NodeStart
    /// 2 - MusicStart
    /// </summary>
    public int _nowProgressing;

    /// <summary>
    /// true - Hard
    /// false - Easy
    /// </summary>
    bool _Nanido;
    int _HP;
    public UI2DSprite _HPBar;


    public ReaderBoardScoreSet _BoardLinker;
    public float[] _StartDelayList;
    public float[] _ChangeDelayList;

    public GameObject _FlashEffect;
    public GameObject _RoundChangeEffect;
    public GameObject _InfinityCallange;
    public GameObject _NextRound;
    public GameObject _Root;

    public GameObject _MainPolygon;
    public List<int> _nowNodeNum;

    public UILabel _ScoreLabel;
    public int _Score;
    public int _PassNode;

    public float _MusicDelayTime;
    public float _NextPolygonTimeset;
    float _delayTime;

    float _RunTime;
    float[] _MaxRunTime = { 120.0f, 120.0f, 120.0f, 120.0f };

    public bool _Pause;
    public int _CanPauseNum;
    public UILabel _PauseNumLabel;
    public bool _NodeStart;
    public bool _MusicStart;
    public bool _ChangeingRound;

    public GameObject _GameOverEffect;
    public GameObject _GameOverLayer;
    public GameObject _FadeIn;
    public bool _GameOver;

    public bool _Tutorialing;
    public GameObject _TutorialScene;

    public GameObject _PauseLayer;

    public NodeGenerator _NodeGenerator;

    public AudioClip _FlashSound = new AudioClip();
    public AudioClip _TurnOff = new AudioClip();

    public List<AudioClip> _BgmList = new List<AudioClip>();
    public ButtonInput _SceneChanger;


    void Start()
    {
        
        _Pause = false;
        _CanPauseNum = 3;
        if (PlayerPrefs.GetInt("tutorial") != 1)
        {
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("tutorial", 1);
            _Tutorialing = true;
            _TutorialScene.SetActive(true);
        }

        string[] musicname = { "Cloistered Story", "Dusty Seas", "Flaming Spheres", "Miniature Road" };
        float[] bpm = { 175.0f, 127.5f, 163.5f, 172.0f };
        //float[] delaytemp = { 60/bpm[0]*2, 60 / bpm[1] * 2, 60 / bpm[2] * 2, 1.36f, 1.39f, 60 / bpm[5] * 2, 1.17f };

        for (int i = 0; i < _BgmList.Count;i++ )
        {
            if(StaticPrefab._MusicName[0]._musicName == musicname[i])
            {
                GetComponent<AudioSource>().clip = _BgmList[i];
                if (bpm[i] >= 150)
                    _NodeGenerator._DelayTime = 60.0f / bpm[i] * 4.0f;
                else
                    _NodeGenerator._DelayTime = 60.0f / bpm[i] * 2.0f;
                break;
            }
        }
        _MusicDelayTime = _StartDelayList[StaticPrefab._MusicName[0]._musicnum];
        _NextPolygonTimeset = _ChangeDelayList[StaticPrefab._MusicName[0]._musicnum];
        _Score = StaticPrefab._MusicName[0]._score;
        _Nanido = StaticPrefab._MusicName[0]._nanido;
        if (!_Nanido)
        {
            _HP = 100;
            _MainPolygon.GetComponent<MainPolygon>()._RotateSpeed = -48.0f;
        }
        else
             _MainPolygon.GetComponent<MainPolygon>()._RotateSpeed = -120.0f;
            
    }

    void Update()
    {
        if (_Nanido)
            _HPBar.transform.parent.gameObject.SetActive(false);
        else
            _HPBar.fillAmount = _HP / 100.0f;
        _ScoreLabel.text = _Score.ToString();
        _PauseNumLabel.text = _CanPauseNum.ToString() + " / 3";
        if(!_Pause)
        {
            _ProgressingTime += Time.smoothDeltaTime;
            if (_nowProgressing == 0 && _ProgressingTime >= 3.0f && !_Tutorialing)
                StartGame();
            else if(_nowProgressing == 1 && _ProgressingTime>=1.2f + _MusicDelayTime)
                StartMusic();


            _RunTime += Time.smoothDeltaTime;
            if (_RunTime >= _MaxRunTime[StaticPrefab._MusicName[0]._musicnum])
                NextMusic();
            _delayTime -= Time.smoothDeltaTime;
            if (_PassNode >= 10)
            {
                _MainPolygon.GetComponent<MainPolygon>().PolygonUpgrade();
                
                _PassNode = 0;
                ChangeRound();
                StartCoroutine(StartRount(_NodeGenerator._DelayTime + _NextPolygonTimeset));
            }
        }
    }

    public void ChangeRound()
    {
        _ChangeingRound = true;
        _NodeGenerator._nowTime = _NodeGenerator._DelayTime;
        _nowNodeNum.Clear();
        if (_MainPolygon.GetComponent<MainPolygon>()._Level == 5)
        {
            GameObject obj = NGUITools.AddChild(_Root, _InfinityCallange);
            obj.transform.localPosition = new Vector3(0, -280, 0);
        }
        else
        {
            GameObject obj = NGUITools.AddChild(_Root, _RoundChangeEffect);
            obj.transform.localPosition = new Vector3(0, -280, 0);
        }
        for (int i = 0; i < _MainPolygon.transform.childCount; i++)
        {
            if (_MainPolygon.transform.GetChild(i).name == "Shild_Node(Clone)")
                Destroy(_MainPolygon.transform.GetChild(i).gameObject);
            
        }
    }

    public void PlusScore()
    {
        if (_delayTime <= 0.0f)
        {
            if (_nowNodeNum[0] == _MainPolygon.GetComponent<MainPolygon>()._nowVertexNum)
            {
                _nowNodeNum.RemoveAt(0);
                _delayTime = 0.3f;
                _Score++;
                _PassNode++;
                if (!_Nanido)
                    _HP += 5;
            }
            else
            {
                if(_Nanido)
                {
                    Destroy(_MainPolygon.transform.parent.gameObject);
                    _GameOver = true;
                    _NodeStart = false;
                    GetComponent<AudioSource>().Stop();
                    AudioSource.PlayClipAtPoint(_TurnOff, Vector2.zero);
                    GameObject obj = NGUITools.AddChild(_Root, _GameOverEffect);
                    for (int i = 0; i < _MainPolygon.transform.childCount; i++)
                    {
                        if (_MainPolygon.transform.GetChild(i).name == "Shild_Node(Clone)")
                            Destroy(_MainPolygon.transform.GetChild(i).gameObject);
                    }
                    StartCoroutine(GameOver());
                }
                else
                {
                    _HP -= 70;
                    _nowNodeNum.RemoveAt(0);
                    _delayTime = 0.3f;
                    if(_HP<=0)
                    {
                        Destroy(_MainPolygon.transform.parent.gameObject);
                        _GameOver = true;
                        _NodeStart = false;
                        GetComponent<AudioSource>().Stop();
                        AudioSource.PlayClipAtPoint(_TurnOff, Vector2.zero);
                        GameObject obj = NGUITools.AddChild(_Root, _GameOverEffect);
                        for (int i = 0; i < _MainPolygon.transform.childCount; i++)
                        {
                            if (_MainPolygon.transform.GetChild(i).name == "Shild_Node(Clone)")
                                Destroy(_MainPolygon.transform.GetChild(i).gameObject);
                        }
                        StartCoroutine(GameOver());
                    }
                    else
                    {
                        _MainPolygon.GetComponent<MainPolygon>().HitAnimation();
                    }
                }
            }
        }
    }
  

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);

        _NodeStart = false;
        _GameOverLayer.SetActive(true);

        if(_Score>PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", _Score);

            if (_Nanido)
                _BoardLinker.SetScore_ReaderBoard();
        }

         _GameOverLayer.transform.Find("FinalScore").GetComponent<UILabel>().text = _Score.ToString();
    }

    void StartGame()
    {
        _nowProgressing++;
        _NodeStart = true;
        _ProgressingTime -= 3.0f;
    }
    void StartMusic()
    {
        if (!_GameOver)
        {
            _nowProgressing++;
            Debug.Log("MusicStart");
            GetComponent<AudioSource>().Play();
            _MusicStart = true;
        }
        
    }
    IEnumerator StartRount(float time)
    {
        yield return new WaitForSeconds(time);

        _ChangeingRound = false;
    }
    void NextMusic()
    {
        
        if(!_GameOver)
        {
            _RunTime = 0.0f;
            for (int i = 0; i < _MainPolygon.transform.childCount; i++)
            {
                if (_MainPolygon.transform.GetChild(i).name == "Shild_Node(Clone)")
                    Destroy(_MainPolygon.transform.GetChild(i).gameObject);
            }

            GameObject obj = NGUITools.AddChild(_Root, _NextRound);
            obj.transform.localPosition = new Vector3(0, 0, 0);
            _GameOver = true;
            _NodeStart = false;
            string[] musicname = { "Cloistered Story", "Dusty Seas", "Flaming Spheres", "Miniature Road" };
            int num = Random.Range(0, 4);
            while (num == StaticPrefab._MusicName[0]._musicnum)
                num = Random.Range(0, 4);
            bool nanido = StaticPrefab._MusicName[0]._nanido;
            StaticPrefab._MusicName.Clear();
            StaticPrefab._MusicName.Add(new MusicName { _musicName = musicname[num], _musicnum = num, _score = _Score,_nanido=nanido });
            StartCoroutine(NextRound_Coroutine(2.5f));
        }
            
    }
    IEnumerator NextRound_Coroutine(float time)
    {
        yield return new WaitForSeconds(time);

        _SceneChanger.ChangeGameScene();
    }
}