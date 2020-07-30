using UnityEngine;
using System.Collections;

public class TouchMng : MonoBehaviour
{

    public GameObject _PauseButton;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameMng.Data._ChangeingRound && !GameMng.Data._Pause && !GameMng.Data._GameOver && !GameMng.Data._Tutorialing)
        {

            if (Input.mousePosition.x <= Screen.width / 2)
                GameMng.Data._MainPolygon.GetComponent<MainPolygon>().VertexDown();
            else
            {
                Vector2 pos = _PauseButton.transform.localPosition;
                Vector2 mpos = new Vector2(Input.mousePosition.x * (1280.0f / Screen.width), Input.mousePosition.y * (720.0f / Screen.height));
                if (!(mpos.x >= pos.x - 30 && mpos.x <= pos.x + 30 && mpos.y >= pos.y - 30 && mpos.y <= pos.y + 30))
                    GameMng.Data._MainPolygon.GetComponent<MainPolygon>().VertexUp();
            }
        }
    }
}
