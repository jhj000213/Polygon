using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour {

	void Awake()
    {
        Screen.SetResolution(1280, 720, true);
    }
}
