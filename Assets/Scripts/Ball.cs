using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    //public Transform GroundCheck; // 2��° ���
    [SerializeField] private Transform GroundCheck; // ������ ����� ���
    [SerializeField] private LayerMask WhatisGround;
    [SerializeField] private LayerMask WidthBooster;
    [SerializeField] private LayerMask HeightBooster;
    [SerializeField] private LayerMask UpBooster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 1��° ���
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        ObjectCheck();
        if(rb.position.y < -10)
        {
            Instantiate(gameObject,GameObject.FindGameObjectWithTag("Respawn").transform.position,Quaternion.identity); // Quaternion.identity ������ ��ü�� ����
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

        if (rb.gravityScale == 0) // ���߷� 
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
                rb.gravityScale = 3;
        }
        else // �߷� ����
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y); // A or D <- or -> �Է������� -1, 0, 1 ��ȯ
        }
    }
    void ObjectCheck()
    {
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WhatisGround))
        {// AddForce(���۽����� ��), velocity(�������� ���ӵ�), transform(��ǥ �����̵�) = position
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);// Impulse�� ���
            rb.gravityScale = 3;
        }
        Collider2D WidthBoosterChecker = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, WidthBooster);
        if (WidthBoosterChecker)
        {
            int dir = (int)WidthBoosterChecker.gameObject.transform.localScale.x;
            rb.position = WidthBoosterChecker.gameObject.transform.position + new Vector3(dir, 0, 0); // Ư����ǥ �����Ҷ��� Vector3 ����ؾ���
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
