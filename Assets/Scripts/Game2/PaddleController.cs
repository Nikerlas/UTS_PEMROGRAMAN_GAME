using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PaddleController : MonoBehaviour
{
    public float batasAtas, batasBawah, batasKanan, batasKiri;
    public float kecepatan;
    public string Verticalaxis, Horizontalaxis;
    
    // Use this for initialization 
    void Start()
    {
    }
    // Update is called once per frame 
    void Update()
    {
        float gerakVertical = Input.GetAxis(Verticalaxis) * kecepatan * Time.deltaTime;
        float gerakHorizontal = Input.GetAxis(Horizontalaxis) * kecepatan * Time.deltaTime;
        float nextPos1 = transform.position.y + gerakVertical; 
        float nextPos2 = transform.position.x + gerakHorizontal; 
        if (nextPos1 > batasAtas) 
        { 
            gerakVertical = 0; 
        } 
        if (nextPos1 < batasBawah) 
        { 
            gerakVertical = 0; 
        } 
        if (nextPos2 > batasKanan) 
        { 
            gerakHorizontal = 0; 
        } 
        if (nextPos2 < batasKiri) 
        { 
            gerakHorizontal = 0; 
        } 

        transform.Translate(gerakHorizontal, gerakVertical, 0);
    }
}
