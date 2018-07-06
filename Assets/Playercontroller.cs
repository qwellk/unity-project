using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {

    // Use this for initialization
    public float speed = 1;
    public float jumpspeed = 2;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            float nx = GetComponent<Rigidbody2D>().velocity.x;
            float spx = Mathf.SmoothDamp(nx,speed, ref nx, 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(spx, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float nx = GetComponent<Rigidbody2D>().velocity.x;
            float spx = Mathf.SmoothDamp(nx, -speed, ref nx, 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(spx, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (GetComponent<Rigidbody2D>().velocity.y > 0.05 * jumpspeed)
            {
                float spy = GetComponent<Rigidbody2D>().velocity.y + jumpspeed;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, spy);
            }
        }
    }
}
