
using UnityEngine;
using System.Collections;

public class BackButtonInput : MonoBehaviour {

    public GameObject _GameOffLayer;
    public ButtonInput _ForPause;
    public bool _gamescene;

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_gamescene)
            {
                if (!GameMng.Data._Pause)
                    _ForPause.SetPause();
                else
                    _ForPause.SetResume();
            }
            else
            {
                _GameOffLayer.SetActive(!_GameOffLayer.activeSelf);
            }

        }

    }
    public void SetLayerOff()
    {
        _GameOffLayer.SetActive(false);
    }
    public void GameOff()
    {
        Debug.Log("GameOff");
        Application.Quit();
    }

}
