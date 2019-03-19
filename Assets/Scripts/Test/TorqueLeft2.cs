using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueLeft2 : MonoBehaviour {

    Rigidbody2D rb;
    public static bool flag;
    float nextTime = 0;
    float v = 0f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        flag =false;
    }

    void Update () {
        if (!flag)
        {
            //             rb.AddTorque(-Input.GetAxis("Vertical") * Time.deltaTime * 100f);

            v = 0;
            if (Input.GetKey("a"))
                v = 1;

            if (Input.GetKey("d"))
                v = -1;


            if (transform.rotation.z <= -0.99f) flag = true;

//            transform.Rotate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * 50f);
            transform.Rotate(Vector3.back * v * Time.deltaTime * 50f);

            nextTime = Time.time + 0.5f;
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
