using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Singleton
    private static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Animator m_Animator;
    public Rigidbody m_RigidBody;

    public  float m_Speed       = 4f;
    public  float m_MaxSpeed    = 8f;
    public  float m_JumpForce   = 20f;
    public  uint  m_PlayerLives = 3u;
    private readonly float m_Increasing  = 0.03f;
    private bool  m_IsGrounded;
    private bool  m_IsAlive;
    private bool  m_IsStoped;

    private Vector2 m_BoardsOffset = new Vector2(-4f, 4f);
    private const float m_MoveOffset = 4f;

    private const int m_GroundLayer   = 8;
    private const int m_ObstacleLayer = 9;

    private Vector3 m_MoveToRoadPos;

    void Start()
    {
        m_IsStoped = false;
        m_IsAlive = false;
        m_IsGrounded = true;
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_IsAlive)
        {
            if (!m_IsStoped)
                transform.Translate(transform.forward * m_Speed * Time.deltaTime);

            /**************************Left Right Movement***************************/
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveToRoad(transform.right);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveToRoad(-transform.right);
            }

            /**************************Jump*****************************************/
            if (Input.GetKeyDown(KeyCode.UpArrow) && m_IsGrounded)
            {
                m_Animator.SetBool("isGrounded", false);
                m_Animator.SetBool("isJumped", true);
                m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
                m_IsGrounded = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_RigidBody.AddForce(-Vector3.up * m_JumpForce, ForceMode.Impulse);
            }

            m_Speed = Mathf.Clamp(m_Speed + m_Increasing * Time.deltaTime, 1f, m_MaxSpeed);
        }
        else
        {
            if (!m_IsGrounded)
            {
                transform.Translate(transform.forward * m_Speed * Time.deltaTime);
                m_Speed = Mathf.Clamp(m_Speed + m_Increasing * Time.deltaTime, 1f, m_MaxSpeed);
            }
        }
    }

    public static void SetPlayActive()
    {
        instance.m_IsAlive = true;
        instance.m_Animator.SetBool("isGameOver", false);
    }

    private void MoveToRoad(Vector3 moveVector)
    {
        m_MoveToRoadPos = transform.position;
        
        float x = transform.position.x;
        x += (moveVector * m_MoveOffset).x;
        x = Mathf.Clamp(x, m_BoardsOffset.x, m_BoardsOffset.y);
        Vector3 nextPos = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = nextPos;
    }

    private void LoseLive()
    {
        UIManager.UpdateLives(--m_PlayerLives);

        if (m_PlayerLives == 0)
        {
            m_IsAlive = false;
            m_Animator.SetBool("isGameOver", true);
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == m_GroundLayer)
        {
            m_IsGrounded = true;
            m_Animator.SetBool("isJumped", false);
            m_Animator.SetBool("isGrounded", true);
        }
        else if (collision.gameObject.layer == m_ObstacleLayer)
        {
            LoseLive();
            collision.collider.gameObject.SetActive(false);
            Vector3 pos = gameObject.transform.position;
            if (pos.x > 2f)
                pos.x = m_BoardsOffset.y;
            else if (pos.x < -2f)
                pos.x = m_BoardsOffset.x;
            else pos.x = 0f;
            transform.position = pos;
        }
    }

}
