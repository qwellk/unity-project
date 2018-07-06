using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {

    // Use this for initialization
    public float speed = 1;
    public float jumpspeed = 2;
    public float jumptime = 0.5f;
    public float boradthick = 0.5f;
    public float attackdis = 0.5f;

    private float localtime = 0;
    private bool ifjump = false;
    private bool onworld = true;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerinput();

        if (Input.GetKeyDown(KeyCode.X))
        {
            changeworld();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void playerinput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            float nx = GetComponent<Rigidbody2D>().velocity.x;
            float spx = Mathf.SmoothDamp(nx, speed, ref nx, 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(spx, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float nx = GetComponent<Rigidbody2D>().velocity.x;
            float spx = Mathf.SmoothDamp(nx, -speed, ref nx, 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(spx, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!ifjump)
            {
                float spy = GetComponent<Rigidbody2D>().velocity.y + ((onworld)?jumpspeed:-jumpspeed);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, spy);
                ifjump = true;
            }
        }

        if (ifjump)
        {
            if (localtime < jumptime)
            {
                localtime += Time.deltaTime;
            }
            else
            {
                localtime = 0;
                ifjump = false;
            }
        }
    }

    private void changeworld()
    {
        float ny = GetComponent<Transform>().position.y + ((onworld) ? -(boradthick + GetComponent<Renderer>().bounds.size.y) : (boradthick + GetComponent<Renderer>().bounds.size.y));
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, ny, GetComponent<Transform>().position.z);
        GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;

        onworld = !onworld;
    }

    private void attack()
    {
        Vector2 attpoint = new Vector2(GetComponent<Transform>().position.x+attackdis* GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attpoint,new Vector2(1f,0.2f),0);
        foreach (Collider2D x in colliders)
        {
            print(x.transform.gameObject.layer);
            if (x.transform.gameObject.layer == 8)
            {
                print("des");
                Destroy(x.transform.gameObject);
            }
        }
    }
}
