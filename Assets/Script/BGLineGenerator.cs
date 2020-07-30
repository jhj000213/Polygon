using UnityEngine;
using System.Collections;

public class BGLineGenerator : MonoBehaviour
{

    float _nowtime;
    public GameObject _BGLine;
    public GameObject _BG;

    void Update()
    {
        _nowtime += Time.smoothDeltaTime;
        if (_nowtime >= 1.0f)
        {
            _nowtime = 0.0f;
            int rand = Random.Range(0, 4);
            if (rand == 0)//down
            {
                GameObject obj = NGUITools.AddChild(_BG, _BGLine);
                obj.GetComponent<BGLine>()._Vector = rand;
                obj.transform.localEulerAngles = new Vector3(0, 0, 90);
                float x = Random.Range(-630, 630);
                obj.transform.localPosition = new Vector3(x, -370, 0);
            }
            else if (rand == 1)//right
            {
                GameObject obj = NGUITools.AddChild(_BG, _BGLine);
                obj.GetComponent<BGLine>()._Vector = rand;
                float x = Random.Range(-350, 350);
                obj.transform.localPosition = new Vector3(-670, x, 0);
            }
            else if (rand == 2)//up
            {
                GameObject obj = NGUITools.AddChild(_BG, _BGLine);
                obj.GetComponent<BGLine>()._Vector = rand;
                obj.transform.localEulerAngles = new Vector3(0, 0, -90);
                float x = Random.Range(-630, 630);
                obj.transform.localPosition = new Vector3(x, 370, 0);
            }
            else if (rand == 3)//left
            {
                GameObject obj = NGUITools.AddChild(_BG, _BGLine);
                obj.GetComponent<BGLine>()._Vector = rand;
                obj.transform.localEulerAngles = new Vector3(0, 0, -180);
                float x = Random.Range(-350, 350);
                obj.transform.localPosition = new Vector3(670, x, 0);
            }
        }
    }
}
