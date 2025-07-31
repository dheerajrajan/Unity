using Unity.VisualScripting;
using UnityEngine;

public class Paddler : MonoBehaviour
{
    public Rigidbody2D rigidbody {  get; private set; }

    public Vector2 direction { get; private set; }

    public float speed = 30f;
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddler()
    {
        this.transform.position = new Vector2(0f,this.transform.position.y);
        this.rigidbody.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;

        }
        else
        {
            this.direction=Vector2.zero;
        }

        

    }
    private void FixedUpdate()
    {
        if (this.direction != Vector2.zero)
        {
            this.rigidbody.AddForce(this.direction*this.speed);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball=collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Vector2 paddlePosoition=this.transform.position;
            Vector2 contactPoint=collision.GetContact(0).point;

            float offset=paddlePosoition.x-contactPoint.x;  
            float width=collision.otherCollider.bounds.size.x/2;
            float currenrtAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.linearVelocity);
        }
    }
}
