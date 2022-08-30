using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   [SerializeField] private Transform _groundCheck;
   [SerializeField] private LayerMask _groundLayer;

   private float _speed = 5f;

   private float _jumpForce = 2f;
   private float _jumpIteration = 1f;
   private int _jumpCount = 0;
   private int _jumpMaxCount = 2;
   private bool _jumpPressed;

   private bool _isGrounded;

   private Vector3 _direction;
   private Animator _animator;
   private Rigidbody2D _rigidbody;

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _rigidbody = GetComponent<Rigidbody2D>();
      Application.targetFrameRate = 70;
   }

   private void Update()
   {
      _isGrounded = CheckGround();
      _animator.SetBool("isJump", !_isGrounded);

      _direction.x = (int)Input.GetAxisRaw("Horizontal");

      if (Input.GetKey(KeyCode.Space) && (_isGrounded || (_jumpIteration > 1f) || (_jumpCount < _jumpMaxCount)))
         _jumpPressed = true;
      else
         _jumpPressed = false;
      if (Input.GetKeyUp(KeyCode.Space))
      {
         _jumpIteration = 1f;
         _jumpCount++;
      }
      if (_isGrounded && _jumpCount > 0)
         _jumpCount = 0;

      if (Input.GetMouseButtonDown(0))
         Attack();

   }

   private void FixedUpdate()
   {
      Move();
      Jump();
   }

   private void Move()
   {

      if (_direction.x != 0)
      {
         _animator.SetBool("isMove", _isGrounded);
         _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
         transform.localScale = new Vector3(_direction.x, transform.localScale.y, transform.localScale.z);
      }
      else
         _animator.SetBool("isMove", false);
   }

   private void Attack()
   {
      _animator.SetTrigger("attack");
   }

   private void Jump()
   {
      if (_jumpPressed)
         if (++_jumpIteration < 10)
            //_rigidbody.AddForce(transform.up * _jumpForce / _jumpIteration);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce + _jumpIteration);

   }

   private bool CheckGround()
   {
      Collider2D[] collider = Physics2D.OverlapCircleAll(_groundCheck.position, 0.3f, _groundLayer);
      return collider.Length > 0;
   }


}
