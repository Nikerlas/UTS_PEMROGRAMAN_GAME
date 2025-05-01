using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GerakPindah : MonoBehaviour
{
    float speed = 3f;
    public Sprite[] sprites;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float firstY;
    // Start is called before the first frame update
    void Start()
    {
        int index  = Random.Range(0, sprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
        float move = ( speed * Time.deltaTime * -1f ) + transform.position.x;
        transform.position = new Vector3(move, transform.position.y);
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        firstY = transform.position.y;
    }
    
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(transform.position.x, curPosition.y, transform.position.z);
    }
    void OnMouseUp()
    {
        transform.position = new Vector3(transform.position.x, firstY, transform.position.z);
    }
}
