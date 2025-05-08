using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunculHewan : MonoBehaviour
{
    public float jeda = 0.8f;
    float timer;

    public GameObject[] obyekHewan;
    public GameObject bomb;

    [Range(0f, 1f)]
    public float peluangBomb = 0.2f; // 20% kemungkinan bomb

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= jeda)
        {
            timer = 0;

            float chance = Random.value; // antara 0 dan 1
            if (chance <= peluangBomb && bomb != null)
            {
                Instantiate(bomb, transform.position, transform.rotation);
            }
            else
            {
                int random = Random.Range(0, obyekHewan.Length);
                Instantiate(obyekHewan[random], transform.position, transform.rotation);
            }
        }
    }
}
