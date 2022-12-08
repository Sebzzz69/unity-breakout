using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    public Vector2 direction { get; private set; }
    public float speed = 30f;

    [Header("Properties")]
    [SerializeField] float mass;
    [SerializeField] float drag;
    [SerializeField] float gravity;

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

