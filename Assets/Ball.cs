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
       
        if(rb.gravityScale == 0) // ���߷� 
        {
            if (Input.GetAxisRaw("Horizontal") != 0) 
                rb.gravityScale = 3;
        }
        else // �߷� ����
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y); // A or D <- or -> �Է������� -1, 0, 1 ��ȯ
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) //�ݸ������� �浹 ���� �� ȣ��
    {
        if(collision.gameObject.tag=="Ground")// tag ����
        {// AddForce(���۽����� ��), velocity(�������� ���ӵ�), transform(��ǥ �����̵�) = position
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);// Impulse�� ���
            rb.gravityScale = 3;
        }
        if(collision.gameObject.tag=="R-B")
        {
            rb.position = collision.transform.position + new Vector3(collision.transform.localScale.x * 1, 0, 0); // Ư����ǥ �����Ҷ��� Vector3 ����ؾ���
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
