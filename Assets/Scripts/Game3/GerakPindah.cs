using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // untuk reload scene / game over

public class GerakPindah : MonoBehaviour
{
    float speed = 3f;
    public Sprite[] sprites;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float firstY;

    public GameObject ledakanPrefab; // efek ledakan
    public GameObject panelGameOver; // panel Game Over

    void Start()
    {
        int index = Random.Range(0, sprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }

    void Update()
    {
        float move = (speed * Time.deltaTime * -1f) + transform.position.x;
        transform.position = new Vector3(move, transform.position.y);
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "Bomb")
        {
            if (ledakanPrefab != null)
            {
                Instantiate(ledakanPrefab, transform.position, Quaternion.identity);
            }

            if (panelGameOver != null)
            {
                panelGameOver.SetActive(true);
            }

            Time.timeScale = 0f; // menghentikan game
            Destroy(gameObject); // hancurkan bomb
            return;
        }

        firstY = transform.position.y;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (gameObject.tag == "Bomb") return;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        if (gameObject.tag == "Bomb") return;

        transform.position = new Vector3(transform.position.x, firstY, transform.position.z);
    }
}
