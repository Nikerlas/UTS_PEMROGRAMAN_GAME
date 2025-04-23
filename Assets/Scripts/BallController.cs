using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force = 300;
    public AudioClip hitSound;
    public AudioClip goalWhistle;
    public GoalPanelAnimator goalPanelAnimator;
    int scoreP1 = 0;
    int scoreP2 = 0;

    Text scoreUIP1;
    Text scoreUIP2;

    AudioSource audioSource;
    GameObject panelSelesai;
    Text txtPemenang;

    Rigidbody2D rb;
    GameObject paddle1, paddle2;
    Vector2 initialBallPos = Vector2.zero;
    Vector2 initialPaddle1Pos;
    Vector2 initialPaddle2Pos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();
        panelSelesai = GameObject.Find("PanelSelesai");
        txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
        panelSelesai.SetActive(false);

        paddle1 = GameObject.Find("Paddle1");
        paddle2 = GameObject.Find("Paddle2");

        initialPaddle1Pos = paddle1.transform.position;
        initialPaddle2Pos = paddle2.transform.position;

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 arah = new Vector2(Random.Range(0, 2) == 0 ? -1 : 1, 0).normalized;
        rb.AddForce(arah * force);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Kanan")
        {
            scoreP1++;
            TampilkanScore();

            if (scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txtPemenang.text = "Player 1 Menang!";
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(HandleGoal(true));
            }
        }
        else if (coll.gameObject.name == "Kiri")
        {
            scoreP2++;
            TampilkanScore();

            if (scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txtPemenang.text = "Player 2 Menang!";
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(HandleGoal(false));
            }
        }
        else if (coll.gameObject.name == "Paddle1" || coll.gameObject.name == "Paddle2")
        {
            audioSource.PlayOneShot(hitSound);
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rb.velocity.x, sudut).normalized;
            rb.velocity = Vector2.zero;
            rb.AddForce(arah * force * 2);
        }
    }

    IEnumerator HandleGoal(bool leftToRight)
    {
        rb.velocity = Vector2.zero;
        transform.position = initialBallPos;
        paddle1.transform.position = initialPaddle1Pos;
        paddle2.transform.position = initialPaddle2Pos;

        goalPanelAnimator.PlayGoalAnimation(leftToRight);
        audioSource.PlayOneShot(goalWhistle);

        yield return new WaitForSeconds(2f);
        Vector2 arah = new Vector2(leftToRight ? -1 : 1, 0).normalized;
        rb.AddForce(arah * force);
    }

    void TampilkanScore()
    {
        scoreUIP1.text = scoreP1.ToString();
        scoreUIP2.text = scoreP2.ToString();
    }

}
