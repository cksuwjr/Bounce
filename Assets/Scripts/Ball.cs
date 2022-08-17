using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    //public Transform GroundCheck; // 2번째 방식
    [SerializeField] private Transform GroundCheck; // 보안이 적용된 방법
    [SerializeField] private LayerMask WhatisGround;
    [SerializeField] private LayerMask WidthBooster;
    [SerializeField] private LayerMask HeightBooster;
    [SerializeField] private LayerMask UpBooster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 1번째 방식
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        ObjectCheck();
        if(rb.position.y < -10)
        {
            Instantiate(gameObject,GameObject.FindGameObjectWithTag("Respawn").transform.position,Quaternion.identity); // Quaternion.identity 독립된 객체로 생성
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && rb.gravityScale == 0)
        {
            rb.gravityScale = 3;
            rb.velocity = new Vector2(0, 0);
        }
    }
    void Move()
    {
        Debug.Log(rb.velocity);

        if (rb.gravityScale == 0) // 무중력 
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
                rb.gravityScale = 3;
        }
        else // 중력 있음
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y); // A or D <- or -> 입력했을때 -1, 0, 1 반환
        }
    }
    void ObjectCheck()
    {
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatisGround))
        {// AddForce(갑작스러운 힘), velocity(지속적인 가속도), transform(좌표 순간이동) = position
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);// Impulse는 충격
            rb.gravityScale = 3;
        }
        Collider2D WidthBoosterChecker = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WidthBooster);
        if (WidthBoosterChecker)
        {
            int dir = (int)WidthBoosterChecker.gameObject.transform.localScale.x;
            rb.position = WidthBoosterChecker.gameObject.transform.position + new Vector3(dir, 0, 0); // 특정좌표 설정할때는 Vector3 사용해야함
            rb.gravityScale = 0;
            rb.velocity = new Vector2(dir * 8, 0);
        }

        Collider2D HeightBoosterChecker = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, HeightBooster);
        if (HeightBoosterChecker)
        {
            int dir = (int)HeightBoosterChecker.gameObject.transform.localScale.y * 1;
            rb.position = HeightBoosterChecker.gameObject.transform.position + new Vector3(0, dir, 0);
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, dir * 8);
        }

        Collider2D UpBoosterChecker = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, UpBooster);
        if (UpBoosterChecker)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
    }
}
