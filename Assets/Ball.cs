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
    private void OnCollisionEnter2D(Collision2D collision) //�ݸ������� �浹 ���� �� ȣ��
    {
        if(collision.gameObject.tag=="Ground")// tag ����
        {// AddForce(���۽����� ��), velocity(�������� ���ӵ�), transform(��ǥ �����̵�)
            rb.AddForce(new Vector2(0, 14), ForceMode2D.Impulse);// Impulse�� ���
        }
       
    }
}
