using UnityEngine;
using System.Collections;

public class BGLine : MonoBehaviour
{

    public int _Vector;
    public float _speed;



    void Update()
    {
        if (_Vector == 0)
            transform.Translate(Vector3.right * _speed * Time.smoothDeltaTime);
        else if (_Vector == 1)
            transform.Translate(Vector3.right * _speed * Time.smoothDeltaTime);
        else if (_Vector == 2)
            transform.Translate(Vector3.right * _speed * Time.smoothDeltaTime);
        else if (_Vector == 3)
            transform.Translate(Vector3.right * _speed * Time.smoothDeltaTime);
    }
}
