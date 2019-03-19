using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueLeft1 : MonoBehaviour {

    Rigidbody2D rb;
    public static bool flag;
    float nextTime = 0;
    float v = 0f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        flag = false;
    }

    void Update()
    {
        if (!flag)
        {

            v = 0;
            if (Input.GetKey("w"))
                v = 1;

            if (Input.GetKey("s"))
                v = -1;

            //            transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * Time.deltaTime * 50f);
            transform.Rotate(Vector3.back * v * Time.deltaTime * 50f);
            nextTime = Time.time + 0.5f;
            // rb.AddTorque(-Input.GetAxis("Horizontal") * Time.deltaTime * 100f);
        }
        else
        {
            if (Time.time > nextTime)
            {
                flag = false;
            }
            else
            {
                transform.Rotate(Vector3.back * Time.deltaTime * 10f);
            }
        }
    }
}
