using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunculHewan : MonoBehaviour
{
    public float jeda = 0.8f;
    float timer;
    public GameObject[] obyekHewan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= jeda)
        {
            int random = Random.Range(0, obyekHewan.Length);
            Instantiate(obyekHewan[random], transform.position, transform.rotation);
            timer = 0;
        }
    }
}
