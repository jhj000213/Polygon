using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonInput : MonoBehaviour
{
    public GameObject _Parent;
    public GameObject _FadeObject;

    int _tutorialSceneNum;


    public void SceneChange(string Scenename)
    {
        GameObject obj = NGUITools.AddChild(_Parent, _FadeObject);
        if (Scenename == "GameScene")
            StartCoroutine(GoGameScene());
    }

    public void ChangeGameScene()
    {
        Time.timeScale = 1.0f;
        GameObject obj = NGUITools.AddChild(_Parent, _FadeObject);
        StartCoroutine(GoGameScene());
    }
    public void ChangeMusicSelect()
    {
        Time.timeScale = 1.0f;
        GameObject obj = NGUITools.AddChild(_Parent, _FadeObject);
        StartCoroutine(GoMusicSelectScene());
    }
    public void ChangeRanking()
    {
        Time.timeScale = 1.0f;
        GameObject obj = NGUITools.AddChild(_Parent, _FadeObject);
        StartCoroutine(GoRankingScene());
    }

    IEnumerator GoGameScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("GameScene");
    }
    IEnumerator GoMusicSelectScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MusicSelectScene");
    }
    IEnumerator GoRankingScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("RankingScene");
    }

    public void SetPause()
    {
        if (!GameMng.Data._GameOver && GameMng.Data._CanPauseNum > 0) 
        {
            if (GameMng.Data._nowProgressing > 1)
                Time.timeScale = 0.0f;
            GameMng.Data.gameObject.GetComponent<AudioSource>().Pause();
            GameMng.Data._PauseLayer.SetActive(true);
            GameMng.Data._Pause = true;
            GameMng.Data._CanPauseNum--;
        }
    }
    public void SetResume()
    {
        if (GameMng.Data._MusicStart)
            GameMng.Data.gameObject.GetComponent<AudioSource>().Play();
        Time.timeScale = 1.0f;
        GameMng.Data._Pause = false;
        GameMng.Data._PauseLayer.SetActive(false);
    }
    public void NextTutorial()
    {
        for (int i = 0; i < 7; i++)
            GameMng.Data._TutorialScene.transform.GetChild(i).gameObject.SetActive(false);
        _tutorialSceneNum++;
        if (_tutorialSceneNum > 6)
            ChangeGameScene();
        else
            GameMng.Data._TutorialScene.transform.GetChild(_tutorialSceneNum).gameObject.SetActive(true);
    }
    void CheckGameMgn()
    {
        if(GameMng.Data!=null)
        {
            Time.timeScale = 1.0f;
            GameMng.Data._GameOver = true;
            GameMng.Data._NodeStart = false;
            GetComponent<AudioSource>().Stop();
            for (int i = 0; i < GameMng.Data._MainPolygon.transform.childCount; i++)
            {
                if (GameMng.Data._MainPolygon.transform.GetChild(i).name == "Shild_Node(Clone)")
                    Destroy(GameMng.Data._MainPolygon.transform.GetChild(i).gameObject);
            }
        }
    }
}