using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   [SerializeField] private Transform _groundCheck;
   [SerializeField] private LayerMask _groundLayer;

   private float _speed = 5f;

   private float _jumpForce = 200f;
   private float _jumpIteration = 1f;
   private float _jumpValue = 200f;

   private bool _isGrounded;

   private Vector3 _direction;
   private Animator _animator;
   private Rigidbody2D _rigidbody;

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _rigidbody = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      _isGrounded = CheckGround();
      _animator.SetBool("isJump", !_isGrounded);

      if (Input.GetKey(KeyCode.Space) && (_isGrounded || (_jumpIteration > 1f)))
         Jump();
      if (Input.GetKeyUp(KeyCode.Space))
         _jumpIteration = 1f;

      if (Input.GetMouseButtonDown(0))
         Attack();

   }

   private void FixedUpdate()
   {
      _direction.x = (int)Input.GetAxisRaw("Horizontal");
      if (_direction.x != 0)
      {
         Move();
      }
      else
         _animator.SetBool("isMove", false);
   }

   private void Move()
   {
      _animator.SetBool("isMove", _isGrounded);
      _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
      transform.localScale = new Vector3(_direction.x, transform.localScale.y, transform.localScale.z);
   }

   private void Attack()
   {
      _animator.SetTrigger("attack");
   }

   private void Jump()
   {
      if (++_jumpIteration < _jumpValue)
         _rigidbody.AddForce(transform.up * _jumpForce / _jumpIteration);

   }

   private bool CheckGround()
   {
      Collider2D[] collider = Physics2D.OverlapCircleAll(_groundCheck.position, 0.3f, _groundLayer);
      return collider.Length > 0;
   }


}
