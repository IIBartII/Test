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
        rb.velocity = MovementC.Movement_spd;
        AnimC.AnimacjeVoid();
        AnimC.pVelocity = MovementC.Movement_spd;
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
        [HideInInspector] public Player player;
        [HideInInspector] public float Mov_x;
        [HideInInspector] public float Mov_y;
        public Vector2 Movement_spd;
        public bool inputdetect;
        [Header("Walking")]
        public bool IsWalking;
        public float WalkSpeed = 3.0f;
        [Header("Running")]
        public bool isRunning;
        public float RunSpeed = 5.0f;
        [Header("Dodge")]
        public bool IsDodging = false;
        public bool canDodge = true;
        public float DodgeSpeed = 10.0f;
        public float dodgeDuration;
        public float dodgeCooldown;
        public void MovementVoid()
        {
            Mov_x = Input.GetAxis("Horizontal");
            Mov_y = Input.GetAxis("Vertical");
            inputdetect = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
            if (IsDodging) return;
            else
            {
                if (!IsDodging && canDodge && Input.GetKeyDown(KeyCode.Space) && (Movement_spd.x != 0f || Movement_spd.y != 0f))
                {
                    IsDodging = true;
                    canDodge = false;
                    player.StartCoroutine(player.Dodge());
                }
                else if (inputdetect && Input.GetKey(KeyCode.LeftShift))
                {
                    Movement_spd = new Vector2(Mov_x, Mov_y) * RunSpeed;
                    isRunning = true;
                    IsWalking = false;
                }
                else if (inputdetect && !Input.GetKey(KeyCode.LeftShift))
                {
                    Movement_spd = new Vector2(Mov_x, Mov_y) * WalkSpeed;
                    IsWalking = true;
                    isRunning = false;
                }
                else
                {
                    Movement_spd = new Vector2(Mov_x, Mov_y) * WalkSpeed;
                    isRunning = false;
                    IsWalking = false;
                }
            }
        }
    }
    public IEnumerator Dodge()
    {
        MovementC.Movement_spd = new Vector2(MovementC.Mov_x, MovementC.Mov_y) * MovementC.DodgeSpeed;
        yield return new WaitForSeconds(MovementC.dodgeDuration);
        MovementC.IsDodging = false;
        yield return new WaitForSeconds(MovementC.dodgeCooldown);
        MovementC.canDodge = true;
    }
}