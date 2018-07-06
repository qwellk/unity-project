using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour {

    // Use this for initialization
    public float boxx;
    public float boxy;
    public float acc = 3f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 size = new Vector2(boxx, boxy);
        float rotz = GetComponent<Transform>().localEulerAngles.z;
        Vector2 boxposition = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        Collider2D[] cols = Physics2D.OverlapBoxAll(boxposition, size, rotz);
        //print(boxposition);
        foreach (Collider2D c in cols)
        {
            //print(boxposition);
            if (c.GetComponent<Rigidbody2D>())
            {
                c.GetComponent<Rigidbody2D>().velocity = new Vector2(c.GetComponent<Rigidbody2D>().velocity.x, -c.GetComponent<Rigidbody2D>().velocity.y * acc);
            }

        }
    }
}
