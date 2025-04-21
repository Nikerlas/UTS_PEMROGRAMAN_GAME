using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float batasAtas;
    public float batasBawah;
    public float kecepatan;
    public string axis;
    public string axisX;

    void Update()
    {
        float gerak = Input.GetAxis(axis) * kecepatan * Time.deltaTime;
        float gerakX = Input.GetAxis(axisX) * kecepatan * Time.deltaTime;

        float nextPos = transform.position.y + gerak;
        float nextPosX = transform.position.x + gerakX;

        if(nextPos > batasAtas)
        {
            gerak = 0;
        }
        if(nextPos < batasBawah)
        {
            gerak = 0;
        }
        transform.Translate(gerakX, gerak, 0);
    }
}
