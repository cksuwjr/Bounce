using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-4, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(4, rb.velocity.y);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision) //콜리더끼리 충돌 했을 때 호출
    {
        if(collision.gameObject.tag=="Ground")// tag 만들어서
        {// AddForce(갑작스러운 힘), velocity(지속적인 가속도), transform(좌표 순간이동)
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);// Impulse는 충격
        }
       
    }
}
