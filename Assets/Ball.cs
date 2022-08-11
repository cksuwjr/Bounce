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
        Debug.Log(rb.velocity);
       
        if(rb.gravityScale == 0) // 무중력 
        {
            if (Input.GetAxisRaw("Horizontal") != 0) 
                rb.gravityScale = 3;
        }
        else // 중력 있음
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y); // A or D <- or -> 입력했을때 -1, 0, 1 반환
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) //콜리더끼리 충돌 했을 때 호출
    {
        if(collision.gameObject.tag=="Ground")// tag 만들어서
        {// AddForce(갑작스러운 힘), velocity(지속적인 가속도), transform(좌표 순간이동) = position
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);// Impulse는 충격
            rb.gravityScale = 3;
        }
        if(collision.gameObject.tag=="R-B")
        {
            rb.position = collision.transform.position + new Vector3(collision.transform.localScale.x * 1, 0, 0); // 특정좌표 설정할때는 Vector3 사용해야함
            rb.gravityScale = 0;
            rb.velocity = new Vector2(collision.transform.localScale.x*8, 0);
        }
        if(collision.gameObject.tag=="U-B")
        {
            rb.position = collision.transform.position + new Vector3(0, 1, 0);
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 8);
        }
    }
}
