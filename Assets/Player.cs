using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    #region Klasy
    public MovementClass MovementC;
    public AnimClass AnimC;
    public SoundsClass SoundsC;
    #endregion
    public Rigidbody2D rb;
    public Animator anim;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SoundsC.audiosource = GetComponent<AudioSource>();
        AnimC.pAnim = anim;
        SoundsC.player = this;
        MovementC.player = this;
    }
    public void Update()
    {
        SoundsC.SoundHandler();
        MovementC.MovementVoid();
        rb.velocity = MovementC.Movement_dir;
        AnimC.AnimacjeVoid();
        AnimC.pVelocity = MovementC.Movement_dir;
    }
    public IEnumerator FootStep()
    {
        SoundsC.isRunning = true;
        SoundsC.audiosource.Play();
        yield return new WaitForSeconds(SoundsC.interval);
        SoundsC.isRunning = false;

    }
    [System.Serializable]
    public class SoundsClass
    {
        [HideInInspector] public AudioSource audiosource;
        public AudioClip[] Footstep;
        public float interval;
        public bool isRunning = false;
        [HideInInspector] public Player player;
        public void SoundHandler()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (!isRunning)
                {
                    float randomV = Random.value;
                    if (randomV < 0.33f) audiosource.clip = Footstep[0];
                    else if (randomV < 0.66f) audiosource.clip = Footstep[1];
                    else audiosource.clip = Footstep[2];
                    player.StartCoroutine(player.FootStep());
                }
            }
            else if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
            {
                if (!isRunning)
                {
                    isRunning = false;
                    player.StopCoroutine(player.FootStep());
                }
            }
        }
    }
    [System.Serializable]
    public class AnimClass
    {
        [HideInInspector] public Animator pAnim;
        [HideInInspector] public Vector2 pVelocity;
        public void AnimacjeVoid()
        {
            if (pVelocity.y > 0 && MathF.Abs(pVelocity.x) < 0.5f) //w
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
        public bool isRunning, IsWalking, IsDodging;
        public float WalkSpeed = 5.0f;
        public float RunSpeed = 10.0f;
        [Header("Dodge")]
        public float DodgeSpeed = 20.0f;
        public float dodgeDuration;
        [HideInInspector] public Player player;
        [HideInInspector] public float Mov_x;
        [HideInInspector] public float Mov_y;
        public Vector2 Movement_dir;
        public void MovementVoid()
        {
            Mov_x = Input.GetAxis("Horizontal");
            Mov_y = Input.GetAxis("Vertical");
            if (IsDodging) return;
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.StartCoroutine(player.Dodge());
                    Movement_dir = new Vector2(Mov_x, Mov_y) * DodgeSpeed;
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Movement_dir = new Vector2(Mov_x, Mov_y) * RunSpeed;
                    isRunning = true;
                    IsWalking = false;
                }
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    Movement_dir = new Vector2(Mov_x, Mov_y) * WalkSpeed;
                    IsWalking = true;
                    isRunning = false;
                }
            }
        }
    }
    public IEnumerator Dodge()
    {
        MovementC.IsDodging = true;
        yield return new WaitForSeconds(MovementC.dodgeDuration);
        MovementC.IsDodging = false;
    }
}