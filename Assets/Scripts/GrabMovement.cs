using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMovement : MonoBehaviour
{

    public GameObject player;

    public GameObject rightarm_container;
    public GameObject rightarm1;
    public GameObject rightarm2;
    public GameObject elbow;
    public GameObject grabber_right;

    public GameObject leftarm_container;
    public GameObject leftarm1;
    public GameObject leftarm2;
    public GameObject leftelbow;
    public GameObject grabber_left;


    //        public GameObject RightJoint1;
    //        public GameObject RightBone1;
    //        public GameObject RightJoint2;
    //        public GameObject RightBone2;


    bool Rightflag = false;
    bool Leftflag = false;

    Vector2 PrevPoint;

    FixedJoint2D fj;
    HingeJoint2D hj;

    Rigidbody2D rb;

    void Start()
    {
    }

    void RightDestroy()
    {
        //Right
        rb = grabber_right.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb);

        rb = rightarm2.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb.GetComponent<HingeJoint2D>());
        Destroy(rb);

        rb = rightarm1.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb.GetComponent<HingeJoint2D>());
        Destroy(rb);

        rb = elbow.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb);

        rb = rightarm_container.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb);

        rb = player.GetComponent<Rigidbody2D>();
        Destroy(rb);
    }

    void LeftDestroy()
    {
        //Left
        rb = grabber_left.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb);

        rb = leftarm2.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb.GetComponent<HingeJoint2D>());
        Destroy(rb);

        rb = leftarm1.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb.GetComponent<HingeJoint2D>());
        Destroy(rb);

        rb = leftelbow.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb);

        rb = leftarm_container.GetComponent<Rigidbody2D>();
        Destroy(rb.GetComponent<FixedJoint2D>());
        Destroy(rb);
    }

    void RightAdd()
    {
        //Right
        rb = player.AddComponent<Rigidbody2D>();
        rb.mass = 5f;
        rb.gravityScale = 10f;
        rb = rightarm_container.AddComponent<Rigidbody2D>();
        rb.mass = 0.0001f;
        fj = rightarm_container.AddComponent<FixedJoint2D>();
        fj.connectedBody = player.GetComponent<Rigidbody2D>();

        rightarm1.AddComponent<Rigidbody2D>();
        hj = rightarm1.AddComponent<HingeJoint2D>();
        hj.connectedBody = player.GetComponent<Rigidbody2D>();
        hj.anchor = new Vector2(-0.5f, -0.5f);

        rb = elbow.AddComponent<Rigidbody2D>();
        rb.mass = 0.0001f;
        fj = elbow.AddComponent<FixedJoint2D>();
        fj.connectedBody = rightarm1.GetComponent<Rigidbody2D>();

        rightarm2.AddComponent<Rigidbody2D>();
        hj = rightarm2.AddComponent<HingeJoint2D>();
        hj.connectedBody = rightarm1.GetComponent<Rigidbody2D>();
        hj.anchor = new Vector2(-0.9f, -0.1f);

        rb = grabber_right.AddComponent<Rigidbody2D>();
        rb.mass = 7f;
        fj = grabber_right.AddComponent<FixedJoint2D>();
        fj.connectedBody = rightarm2.GetComponent<Rigidbody2D>();
    }

    void LeftAdd()
    {
        //Left
        rb = leftarm_container.AddComponent<Rigidbody2D>();
        rb.mass = 0.0001f;
        fj = leftarm_container.AddComponent<FixedJoint2D>();
        fj.connectedBody = player.GetComponent<Rigidbody2D>();

        leftarm1.AddComponent<Rigidbody2D>();
        hj = leftarm1.AddComponent<HingeJoint2D>();
        hj.connectedBody = player.GetComponent<Rigidbody2D>();
        hj.anchor = new Vector2(1.86f, -1.03f);

        rb = leftelbow.AddComponent<Rigidbody2D>();
        rb.mass = 0.0001f;
        fj = leftelbow.AddComponent<FixedJoint2D>();
        fj.connectedBody = leftarm1.GetComponent<Rigidbody2D>();

        leftarm2.AddComponent<Rigidbody2D>();
        hj = leftarm2.AddComponent<HingeJoint2D>();
        hj.connectedBody = leftarm1.GetComponent<Rigidbody2D>();
        hj.anchor = new Vector2(-0.94f, -0.1f);

        rb = grabber_left.AddComponent<Rigidbody2D>();
        rb.mass = 7f;
        fj = grabber_left.AddComponent<FixedJoint2D>();
        fj.connectedBody = leftarm2.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!Rightflag)
            {
                RightDestroy();
                LeftDestroy();

                rightarm_container.transform.parent = null;
                elbow.transform.parent = null;
                rightarm1.transform.parent = null;
                rightarm2.transform.parent = null;
                grabber_right.transform.parent = null;

                rightarm2.transform.parent = grabber_right.transform;
                elbow.transform.parent = rightarm2.transform;
                rightarm1.transform.parent = elbow.transform;
                rightarm_container.transform.parent = rightarm1.transform;
                player.transform.parent = rightarm_container.transform;

                Rightflag = true;
            }
            else
            {
                RightAdd();
                LeftAdd();

                rightarm_container.transform.parent = null;
                elbow.transform.parent = null;
                rightarm1.transform.parent = null;
                rightarm2.transform.parent = null;
                grabber_right.transform.parent = null;
                player.transform.parent = null;

                grabber_right.transform.parent = rightarm2.transform;
                rightarm2.transform.parent = elbow.transform;
                elbow.transform.parent = rightarm1.transform;
                rightarm1.transform.parent = rightarm_container.transform;
                rightarm_container.transform.parent = player.transform;

                Rightflag = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!Leftflag)
            {
                RightDestroy();
                LeftDestroy();

                leftarm_container.transform.parent = null;
                leftelbow.transform.parent = null;
                leftarm1.transform.parent = null;
                leftarm2.transform.parent = null;
                grabber_left.transform.parent = null;

                leftarm2.transform.parent = grabber_left.transform;
                leftelbow.transform.parent = leftarm2.transform;
                leftarm1.transform.parent = leftelbow.transform;
                leftarm_container.transform.parent = leftarm1.transform;
                player.transform.parent = leftarm_container.transform;

                Leftflag = true;
            }
            else
            {
                RightAdd();
                LeftAdd();

                leftarm_container.transform.parent = null;
                leftelbow.transform.parent = null;
                leftarm1.transform.parent = null;
                leftarm2.transform.parent = null;
                grabber_left.transform.parent = null;
                player.transform.parent = null;

                grabber_left.transform.parent = leftarm2.transform;
                leftarm2.transform.parent = leftelbow.transform;
                leftelbow.transform.parent = leftarm1.transform;
                leftarm1.transform.parent = leftarm_container.transform;
                leftarm_container.transform.parent = player.transform;

                Leftflag = false;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;

                if (pos.x >= 1100 && pos.y <= 150)
                {
                    if (!Rightflag)
                    {
                        RightDestroy();
                        LeftDestroy();

                        rightarm_container.transform.parent = null;
                        elbow.transform.parent = null;
                        rightarm1.transform.parent = null;
                        rightarm2.transform.parent = null;
                        grabber_right.transform.parent = null;

                        rightarm2.transform.parent = grabber_right.transform;
                        elbow.transform.parent = rightarm2.transform;
                        rightarm1.transform.parent = elbow.transform;
                        rightarm_container.transform.parent = rightarm1.transform;
                        player.transform.parent = rightarm_container.transform;

                        Rightflag = true;
                    }
                    else
                    {
                        RightAdd();
                        LeftAdd();

                        rightarm_container.transform.parent = null;
                        elbow.transform.parent = null;
                        rightarm1.transform.parent = null;
                        rightarm2.transform.parent = null;
                        grabber_right.transform.parent = null;
                        player.transform.parent = null;

                        grabber_right.transform.parent = rightarm2.transform;
                        rightarm2.transform.parent = elbow.transform;
                        elbow.transform.parent = rightarm1.transform;
                        rightarm1.transform.parent = rightarm_container.transform;
                        rightarm_container.transform.parent = player.transform;

                        Rightflag = false;
                    }
                }

                if (pos.x <= 500 && pos.y <= 150)
                {
                    if (!Leftflag)
                    {
                        RightDestroy();
                        LeftDestroy();

                        leftarm_container.transform.parent = null;
                        leftelbow.transform.parent = null;
                        leftarm1.transform.parent = null;
                        leftarm2.transform.parent = null;
                        grabber_left.transform.parent = null;

                        leftarm2.transform.parent = grabber_left.transform;
                        leftelbow.transform.parent = leftarm2.transform;
                        leftarm1.transform.parent = leftelbow.transform;
                        leftarm_container.transform.parent = leftarm1.transform;
                        player.transform.parent = leftarm_container.transform;

                        Leftflag = true;
                    }
                    else
                    {
                        RightAdd();
                        LeftAdd();

                        leftarm_container.transform.parent = null;
                        leftelbow.transform.parent = null;
                        leftarm1.transform.parent = null;
                        leftarm2.transform.parent = null;
                        grabber_left.transform.parent = null;
                        player.transform.parent = null;

                        grabber_left.transform.parent = leftarm2.transform;
                        leftarm2.transform.parent = leftelbow.transform;
                        leftelbow.transform.parent = leftarm1.transform;
                        leftarm1.transform.parent = leftarm_container.transform;
                        leftarm_container.transform.parent = player.transform;

                        Leftflag = false;
                    }

                }
            }

            /*					if(pos.x<=500 && pos.y <=150)
                                {
                                    if (!Leftflag)
                                    {
                                        LeftJoint1.transform.parent = null;
                                        LeftJoint2.transform.parent = null;
                                        LeftBone1.transform.parent = null;
                                        LeftBone2.transform.parent = null;

                                        LeftBone1.transform.parent = LeftJoint2.transform;
                                        LeftJoint1.transform.parent = LeftBone1.transform;
                                        player.transform.parent = LeftJoint1.transform;

                                        Leftflag = true;
                                    }
                                    else
                                    {
                                        LeftJoint1.transform.parent = null;
                                        LeftJoint2.transform.parent = null;
                                        LeftBone1.transform.parent = null;
                                        LeftBone2.transform.parent = null;
                                        player.transform.parent = null;

                                        LeftBone2.transform.parent = LeftJoint2.transform;
                                        LeftJoint2.transform.parent = LeftJoint1.transform;
                                        LeftBone1.transform.parent = LeftJoint1.transform;
                                        LeftJoint1.transform.parent = player.transform;

                                        Leftflag = false;
                                    }
                                }

                            }
                        }*/
            //		}

        }
    }
}
