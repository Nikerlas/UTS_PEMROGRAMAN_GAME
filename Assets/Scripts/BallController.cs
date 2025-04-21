using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    int scoreP1;
    int scoreP2;

    Text scoreUIP1;
    Text scoreUIP2;

    AudioSource audioPlay;
    public AudioClip hitSound;

    GameObject panelSelesai;
    Text txtPemenang;

    public int force;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        scoreP1 = 0;
        scoreP2 = 0;

        rb = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rb.AddForce(arah * force);

        audioPlay = GetComponent<AudioSource>();

        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        audioPlay.PlayOneShot(hitSound);
        if(coll.gameObject.name == "Kanan")
        {
            scoreP1 += 1;
            TampilkanScore();
            if(scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txtPemenang.text = "Player 1 Menang!";
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rb.AddForce(arah * force);
        }
        if(coll.gameObject.name == "Kiri")
        {
            scoreP2 += 1;
            TampilkanScore();
            if(scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txtPemenang.text = "Player 2 Menang!";
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rb.AddForce(arah * force);
        }
        if(coll.gameObject.name == "Paddle1" || coll.gameObject.name == "Paddle2")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rb.velocity.x, sudut).normalized;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(arah * force * 2);
        }
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
    }

    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }
}
