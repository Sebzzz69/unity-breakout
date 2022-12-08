using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    [Header("Properties")]
    [SerializeField] float speed = 500;
    [SerializeField] float gravity;
    [SerializeField] float drag;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.rigidbody.gravityScale = this.gravity;
        this.rigidbody.drag = this.drag;

        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * speed);
    }


}
