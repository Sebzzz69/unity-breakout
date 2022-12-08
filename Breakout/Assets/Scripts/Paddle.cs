using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    public Vector2 direction { get; private set; }
    

    [Header("Properties")]
    [SerializeField] float mass;
    [SerializeField] float drag;
    [SerializeField] float gravity;

    public float speed = 30f;
    public float maxBounceAngle = 75f;

    [Header("Keybinds")]
    [SerializeField] KeyCode rightInput = KeyCode.D;
    [SerializeField] KeyCode rightArrowInput = KeyCode.RightArrow;

    [SerializeField] KeyCode leftInput = KeyCode.A;
    [SerializeField] KeyCode leftArrowInput = KeyCode.LeftArrow;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        this.rigidbody.drag = this.drag;
        this.rigidbody.mass = this.mass;
        this.rigidbody.gravityScale = this.gravity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }

    private void Update()
    {
        UserInput();
    }
    private void FixedUpdate()
    {
        MovePaddle();
    }

    private void UserInput()
    {
        if(Input.GetKey(rightInput) || Input.GetKey(rightArrowInput))
        {
            this.direction = Vector2.right;
        } 
        else if (Input.GetKey(leftInput) || Input.GetKey(leftArrowInput))
        {
            this.direction = Vector2.left;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }

    private void MovePaddle()
    {
        if (this.direction != Vector2.zero)
        {
            this.rigidbody.AddForce(this.direction * speed);
        }
    }


}

