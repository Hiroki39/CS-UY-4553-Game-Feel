using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    float speed = 400;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        rb.AddForce(Vector3.right * xSpeed * Time.deltaTime);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 1;
        mousePosition.y = (float)-5.72;
        transform.position = mousePosition;
    }
}
