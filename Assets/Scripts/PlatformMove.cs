using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    float initY;
    float initZ;
    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = initZ;
        mousePosition.y = initY;
        transform.position = mousePosition;
    }
}
