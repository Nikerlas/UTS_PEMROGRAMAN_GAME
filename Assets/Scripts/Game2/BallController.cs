using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallController : MonoBehaviour
{
    public GameObject hitSFX;
    public int force;
    Rigidbody2D rigid;
    int Score1;
    int Score2;
    Text ScoreUI1;
    Text ScoreUI2;
    private Rigidbody2D rb;

    public int maxBounceSpeed = 800; // batas maksimum force total
    public int bounceIncrement = 50; // berapa besar force ditambahkan setiap pantulan
    private int bounceCount = 0;
    private int maxBounceCount = 5;

    public GameObject pemukul1;
    public GameObject pemukul2;
    private Vector2 posisiAwalP1;
    private Vector2 posisiAwalP2;
    private int arahSetelahSkor = 1; // 1 = kanan, -1 = kiri



    // Use this for initialization
    void Awake()
    {
        posisiAwalP1 = pemukul1.transform.position;
        posisiAwalP2 = pemukul2.transform.position;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);
        Score1 = 0;
        Score2 = 0;
        ScoreUI1 = GameObject.Find("Score1").GetComponent<Text>();
        ScoreUI2 = GameObject.Find("Score2").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "gawang_kanan")
        {
            ResetBall();
            Score1 += 1;
            // TampilkanScore();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "gawang_kiri")
        {
            ResetBall();
            Score2 += 1;
            // TampilkanScore();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "paddle_merah" || coll.gameObject.name == "paddle_biru")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            int tambahanForce = force;
            if (bounceCount < maxBounceCount)
            {
                tambahanForce += bounceCount * bounceIncrement;
                bounceCount++;
            }
            else
            {
                tambahanForce = maxBounceSpeed;
            }
            tambahanForce = Mathf.Min(tambahanForce, maxBounceSpeed);
            rigid.AddForce(arah * tambahanForce);
        }
        Instantiate(hitSFX, transform.position, transform.rotation);
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
        bounceCount = 0;
        // Reset posisi dan velocity paddle
        pemukul1.transform.position = posisiAwalP1;
        pemukul2.transform.position = posisiAwalP2;

        if (pemukul1.TryGetComponent<Rigidbody2D>(out var rb1))
            rb1.velocity = Vector2.zero;

        if (pemukul2.TryGetComponent<Rigidbody2D>(out var rb2))
            rb2.velocity = Vector2.zero;
    }

    void TampilkanScore()
    {
        Debug.Log("Score1: " + Score1 + " Score2: " + Score2);
        ScoreUI1.text = Score1 + "";
        ScoreUI2.text = Score2 + "";
    }
}