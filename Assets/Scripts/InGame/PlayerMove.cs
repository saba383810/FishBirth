using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 _movingDirection;
    public float moveSpeed;
    public Rigidbody rb;
    private bool _jumpNow = true;
    public float jumpPower; //調整必要 例850
    private Vector3 _latestPos;
    public float gravityPower; //調整必要　例 - 1000

    private float _inputVertical;
    private float _inputHorizontal;
    private bool _isAnimationTransition;
    
    private int _jumpCnt;
    private bool _isPlaying = false;

    private const int JumpCntMax = 15;
    
    [SerializeField] private Animator animator;
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int AnimationNo = Animator.StringToHash("AnimationNo");
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update() 
    {
        if(!_isPlaying) return;
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
        
        Jump();
        if(_jumpNow) Gravity();
        else PlayerAction();
    }

    void FixedUpdate() 
    {
        if(!_isPlaying) return;
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
            _jumpCnt = 0;
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
    

    /// <summary>
    ///    ジャンプのチェック
    /// </summary>
    void Jump() 
    {
        if(_jumpCnt >= JumpCntMax) return;
        
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Force);
            animator.SetBool(IsJump,true);
            _jumpCnt++;
            _jumpNow = true;
        }
        else
        {
            _jumpCnt = JumpCntMax;
        }
    }
    
    /// <summary>
    ///    重力をかける
    /// </summary>
    void Gravity() 
    {
        rb.AddForce(new Vector3(0, gravityPower, 0));
    }
    
    /// <summary>
    ///    プレイヤーの攻撃アニメーションのチェック
    /// </summary>
    private void PlayerAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetAnimationNo(1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SetAnimationNo(2);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SetAnimationNo(3);
        }
        else
        {
            SetAnimationNo(0);
        }
    }

    public async void SetAnimationNo(int val)
    {
        var currentNo = animator.GetInteger(AnimationNo);
        
        if(currentNo == val) return;
        if(_isAnimationTransition) return;
        _isAnimationTransition = true;
        animator.SetInteger(AnimationNo,val);
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        _isAnimationTransition = false;
    }

    public void SetPlaying(bool val)
    {
        _isPlaying = val;
    }
}
