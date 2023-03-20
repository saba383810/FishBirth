using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace shu
{
//CapsuleCollider��Rigidbody��ǉ�
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
    {

        public Vector3 movePos;

        //�ړ��X�s�[�h
        float speed = 2.5f;

        //�����]���̃X�s�[�h
        float angleSpeed = 200;

        //
        //[SerializeField]bool jumpSwitch = true;
        //����ɐڂ��Ă��邩�ǂ���
        [SerializeField] bool onGround = true;

        //Animator�����[����ϐ�
        Animator animator;

        //Rigidbody������
        Rigidbody rb;

        //Capsule Collider������
        CapsuleCollider caps;

        void Start()
        {
            //Animator��Component���擾����
            animator = GetComponent<Animator>();

            //Rigidbody�R���|�[�l���g���擾
            rb = GetComponent<Rigidbody>();
            //Rigidbody��Constraints��3�Ƃ��`�F�b�N�����
            //����ɉ�]���Ȃ��悤�ɂ���
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            //CapsuleCollider�R���|�[�l���g���擾
            caps = GetComponent<CapsuleCollider>();
            //CapsuleCollider�̒��S�̈ʒu�����߂�
            caps.center = new Vector3(0, 0.76f, 0);
            //CapsuleCollider�̔��a�����߂�
            caps.radius = 0.23f;
            //CapsuleCollider�̍��������߂�
            caps.height = 1.6f;
        }

        void Update()
        {

            //WS�L�[�A�����L�[�ňړ�����
            float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

            //�O�i�̌��
            //��ނ͑O�i��3����1�̃X�s�[�h�ɂȂ�
            if (z > 0)
            {
                transform.position += transform.forward * z;
            }
            else
            {
                transform.position += transform.forward * z / 3;
            }

            //AD�L�[�A�����L�[�ŕ�����ւ���
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * angleSpeed;
            transform.Rotate(Vector3.up * x);

            //z�̒l�ɂ���đҋ@�A�O�i�A��ނ��؂�ւ��
            animator.SetFloat("Blend", z * 100);

            //�X�y�[�X�L�[�ŃW�����v
            if (Input.GetButton("Jump") && onGround)
            {
                //�^��Ɍ������ė͂�^����
                rb.AddForce(transform.up * 500);
                //�W�����v�̃A�j���[�V�������o��
                animator.SetBool("Jumping", true);
                onGround = false;
            }
        }

        //Ground�^�O�̃I�u�W�F�N�g�ɐG��Ă�����
        //�W�����v�̃A�j���[�V����������
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Ground")
            {
                animator.SetBool("Jumping", false);
                onGround = true;
            }
        }
    }
}