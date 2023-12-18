using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    #region Klasy
    public MovementClass MovementC;
    public AnimClass AnimC;
    #endregion
    public Rigidbody2D rb;
    public Animator anim;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AnimC.pAnim = anim;
    }
    public void Update()
    {
        MovementC.MovementVoid();
        rb.velocity = MovementC.Movement_dir;
        AnimC.AnimacjeVoid();
        AnimC.pVelocity = MovementC.Movement_dir;
    }
    [System.Serializable]
    public class AnimClass
    {
        [HideInInspector] public Animator pAnim;
        [HideInInspector] public Vector2 pVelocity;
        public void AnimacjeVoid()
        {
            if (pVelocity.y > 0 && MathF.Abs(pVelocity.x) <0.5f) //w
            {
                pAnim.Play("Walk Top");
                pAnim.SetBool("Idle", false);
            }
            if (pVelocity.y < 0 && MathF.Abs(pVelocity.x) < 0.5f) //s
            {
                pAnim.Play("Walk Down");
                pAnim.SetBool("Idle", false);
            }
            if (pVelocity.x < 0 && MathF.Abs(pVelocity.y) < 0.5f) //a
            {
                pAnim.Play("Walk Left");
                pAnim.SetBool("Idle", false);
            }
            if (pVelocity.x > 0 && MathF.Abs(pVelocity.y) < 0.5f) //d
            {
                pAnim.Play("Walk Right");
                pAnim.SetBool("Idle", false);
            }
            if (pVelocity.magnitude <= 0.1 && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                pAnim.SetBool("Idle", true);
            }
        }
    }
    [System.Serializable]
    public class MovementClass
    {
        public float speed = 5.0f;
        [HideInInspector]
        public float Mov_x;
        [HideInInspector]
        public float Mov_y;
        public Vector2 Movement_dir;
        public void MovementVoid()
        {
            Mov_x = Input.GetAxis("Horizontal");
            Mov_y = Input.GetAxis("Vertical");
            Movement_dir = new Vector2(Mov_x, Mov_y) * speed;
        }
    }
}