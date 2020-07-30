using UnityEngine;
using System.Collections;

public class NodeDown : MonoBehaviour
{

    public float _speed;

    void Update()
    {
        if(!GameMng.Data._Pause)
        {
            transform.Translate(Vector3.down * _speed * Time.smoothDeltaTime);
            if (transform.localPosition.y <= 0.0f)
            {
                GameMng.Data.PlusScore();
                if(!GameMng.Data._GameOver)
                {
                    GameObject obj = NGUITools.AddChild(GameMng.Data._Root, GameMng.Data._FlashEffect);
                    obj.transform.localEulerAngles = transform.parent.localEulerAngles + GameMng.Data._MainPolygon.transform.localEulerAngles;
                }
                
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
