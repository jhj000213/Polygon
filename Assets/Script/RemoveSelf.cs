using UnityEngine;
using System.Collections;

public class RemoveSelf : MonoBehaviour
{

    public float _DelayTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_DelayTime);

        Destroy(gameObject);
    }
}
