using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerakPindah : MonoBehaviour
{
    float speed = 3f;
    public Sprite[] sprites;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float firstY;

    void Start()
    {
        int index = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }

    void Update()
    {
        float move = transform.position.x - (speed * Time.deltaTime);
        transform.position = new Vector3(move, transform.position.y);
    }

    void OnMouseDown()
    {
        if (CompareTag("Bomb"))
        {
            GameOverManager.TriggerGameOver(); // panggil Game Over
            Destroy(gameObject); // hancurkan bom
            return;
        }

        firstY = transform.position.y;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (CompareTag("Bomb")) return;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        if (CompareTag("Bomb")) return;

        transform.position = new Vector3(transform.position.x, firstY, transform.position.z);
    }
}
