using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 _movingDirection;
    public float moveSpeed;
    public Rigidbody rb;
    private bool _jumpNow;
    public float jumpPower; //調整必要 例850
    private Vector3 _latestPos;
    public float gravityPower; //調整必要　例 - 1000

    private float _inputVertical;
    private float _inputHorizontal;
    
    [SerializeField] private Animator animator;
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update() 
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");
        
        _movingDirection = new Vector3(_inputVertical, 0, _inputHorizontal);
        _movingDirection.Normalize();//斜めの距離が長くなるのを防ぎます
        if (Mathf.Abs(_movingDirection.x) > 0.5f || Mathf.Abs(_movingDirection.y) > 0.5f || Math.Abs(_movingDirection.z) > 0.5f)
        {
            animator.SetBool(IsRun,true);
        }
        else
        {
            animator.SetBool(IsRun,false);
        }

        Gravity();
        
        Jump();
    }

    void FixedUpdate() 
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        var moveForward = cameraForward * _inputVertical + Camera.main.transform.right * _inputHorizontal;
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(_latestPos.x, 0, _latestPos.z);
        _latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (_movingDirection == new Vector3(0, 0, 0)) return;
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
            transform.rotation = rot;
        }
        
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _jumpNow = false;
            animator.SetBool(IsJump,false);
        }
    }
    
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _jumpNow = true;
            animator.SetBool(IsJump,true);
        }
    }
    

    void Jump() 
    {
        if (_jumpNow) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpPower,ForceMode.Impulse);
            animator.SetBool(IsJump,true);
            _jumpNow = true;
        }
    }
    void Gravity() 
    {
        rb.AddForce(new Vector3(0, gravityPower, 0));
    }
    
}
