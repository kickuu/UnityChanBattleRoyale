using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _smooth = 10.0f;
    [SerializeField] float _jumpForce = 250;

    Animator _animator;
    Rigidbody _rigidbody;
    AnimatorStateInfo currentState;
    float _horizontal;
    float _vertical;
    Vector3 _targetDir;
    bool _isGround;
    bool _isJump;
    bool _isSlide;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Jumping");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine("Slide");
        }
    }

    private void FixedUpdate()
    {
        _targetDir = new Vector3(_horizontal, 0, _vertical);

        if (_targetDir.sqrMagnitude > 0.1)
        {
            _animator.SetBool("Run", true);
            Quaternion rotation = Quaternion.LookRotation(_targetDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _smooth);
            transform.position += new Vector3(_horizontal, 0, _vertical).normalized * _moveSpeed * Time.deltaTime;
        }
        else if (_targetDir.sqrMagnitude < 0.1)
        {
            _animator.SetBool("Run", false);
            // Slide();
        }
    }

    IEnumerator Jumping()
    {
        if (_isGround && !_isJump && !_isSlide)
        {
            _animator.SetTrigger("Jump");
            _rigidbody.AddForce(0, _jumpForce, 0);
            _isGround = false;
            _isJump = true;
            yield return new WaitWhile(() => !_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
            yield return new WaitWhile(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
            _isJump = false;
        }
    }

    IEnumerator Slide()
    {
        if (_isGround)
        {
            _animator.SetTrigger("Slide");
            _isSlide = true;
            // _rigidbody.AddRelativeForce(Vector3.forward * 1000, ForceMode.Force);
            yield return new WaitWhile(() => !_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"));
            yield return new WaitWhile(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"));
            _isSlide = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
}
