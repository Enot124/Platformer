using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   private float _directionX;
   private float _speed = 5f;
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
      if (Input.GetMouseButtonDown(0))
      {
         Attack();
      }
   }

   private void FixedUpdate()
   {
      _direction.x = Input.GetAxisRaw("Horizontal");
      if (_direction.x != 0)
      {
         Move();
      }
      else
         _animator.SetBool("isMove", false);
   }

   private void Move()
   {
      _rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);
      transform.localScale = new Vector3(_direction.x, transform.localScale.y, transform.localScale.z);
      _animator.SetBool("isMove", true);
   }

   private void Attack()
   {
      _animator.SetTrigger("attack");
   }


}
