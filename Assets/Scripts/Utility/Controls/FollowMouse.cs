using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    private Rigidbody2D cellBody;
    Vector2 position = new Vector2(0, 0f);
    public bool facingRight;

    // Start is called before the first frame update
    


    void Start()
    {
        cellBody = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        //Flip();

      
    }

    void FixedUpdate()
    {
        cellBody.MovePosition(position);
    }

    void Flip()
    {
        if (Input.GetAxisRaw("Mouse X") > 0f && !facingRight || Input.GetAxisRaw("Mouse X") < 0f && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = -1 * transform.localScale;
            transform.localScale = theScale;
        }
    }
}
