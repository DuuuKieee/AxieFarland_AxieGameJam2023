using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    public class Player : MonoBehaviour, IHealth
    {
        // Start is called before the first frame update

        private AxieFigure figure;

        [SerializeField] public float moveSpeed, dashSpeed, dashStopSpeed, timeAtack = 2, nextTimeAtack = 0;
        [SerializeField] float timeDashing;
        [SerializeField] GameObject aimObj, afterImageObj, attackObj;
        [SerializeField] Slider dashSlider;
        [SerializeField] TrailRenderer tr;
        Character stats;

        Rigidbody2D rb;
        Animator crossAnim;
        public Animator blackScene;

        float xdir, ydir, xdirRaw, ydirRaw, xdirRawDash, ydirRawDash; //2 bien cuoi: bien luu tru gia tri cuar Input.getAxitRaw khi bat dau Dash
        public bool isPressMoveKey, isAttacking, isDashing, isCanConotrol, isJumping, isHurting, isCanBeHurted, isConfuse, isDie, isWalking, isBugClass, isBirdClass, isBeastClass;

        public float HP, maxHP;
        static public float life;
        Vector3 DashDir;
        public static Player instance;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }

        }
        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            stats = gameObject.GetComponent<Character>();
            isCanConotrol = true;
            isCanBeHurted = true;
            isConfuse = false;
            isDie = false;
            figure = gameObject.GetComponentInChildren<AxieFigure>();
            crossAnim = aimObj.GetComponent<Animator>();

        }

        private void Update()
        {

            // if (HP <= 0 && isDie == false)
            // {

            //     Invoke("Die", 0.5f);
            //     isDie = true;
            // }
            xdir = Input.GetAxis("Horizontal");
            ydir = Input.GetAxis("Vertical");

            xdirRaw = Input.GetAxisRaw("Horizontal");
            ydirRaw = Input.GetAxisRaw("Vertical");
            if (xdir != 0 && !isAttacking || ydir != 0 && !isAttacking)
                figure?.DoRunAnim();
            else if (xdir == 0 && !isAttacking || ydir == 0 && !isAttacking) figure?.DoIdleAnim();
            else if (isAttacking) figure?.DoAttackMeleeAnim();
            if (isCanConotrol) Walking();
            if (Input.GetKeyDown(KeyCode.Space) && dashSlider.value >= dashSlider.maxValue)
            {
                xdirRawDash = xdirRaw;
                ydirRawDash = ydirRaw;
                if (!isBirdClass)
                {
                    dashSlider.gameObject.SetActive(true);
                    dashSlider.value = 0;
                }
                isDashing = true;
                Dash();
                StartCoroutine(AppearAfterImage(0.05f));
                StartCoroutine(StopDashing(timeDashing));
            }
            if (Input.GetMouseButtonDown(0) && !isDie && Time.time > nextTimeAtack)
            {
                Attacking();
                nextTimeAtack = Time.time + timeAtack;
                StartCoroutine(StopAtacking(1f));
            }
            // if (Input.GetKeyDown(KeyCode.E) && isChange)
            // {
            //     isChange = false;
            //     figure.ChangeCharracter();
            //     StartCoroutine(Changed(2f));
            // }




        }

        void Walking()
        {
            if (Time.timeScale != 0)
            {
                // if (!isBugClass)
                // {
                transform.position += new Vector3(xdir * moveSpeed * Time.deltaTime, ydir * moveSpeed * Time.deltaTime);
                if (xdir > 0) figure.flipX = true;
                else if (xdir < 0) figure.flipX = false; // Không lật ngang đối tượng
                // }
                // else
                // {
                //     transform.position += new Vector3(-xdir * (moveSpeed + 3) * Time.deltaTime, -ydir * (moveSpeed + 3) * Time.deltaTime);
                //     if (xdir < 0) figure.flipX = true;
                //     else if (xdir > 0) figure.flipX = false; // Không lật ngang đối tượng
                // }
            }
        }
        void Dash()
        {
            isCanConotrol = false;
            AudioManager.instance.PlaySFX("Dash");
            tr.emitting = true;
            // if (!isBirdClass)
            // {
            //     DashDir = aimObj.transform.position - transform.position;
            //     isCanConotrol = false;
            //     rb.velocity = DashDir * dashSpeed;
            // }
            // else
            // {
            rb.velocity = new Vector2(xdirRawDash, ydirRawDash).normalized * dashSpeed;
            // }

        }
        IEnumerator StopDashing(float sec)
        {
            yield return new WaitForSeconds(sec);

            isCanConotrol = true;

            if (isDashing)
            {
                if (isConfuse) rb.velocity = -new Vector2(xdirRawDash, ydirRawDash).normalized * dashStopSpeed;
                else
                    rb.velocity = new Vector2(xdirRawDash, ydirRawDash).normalized * dashStopSpeed;
            }
            tr.emitting = false;
            isDashing = false;

        }
        IEnumerator AppearAfterImage(float sec)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(afterImageObj, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(sec);
            }
        }
        void Attacking()
        {
            isAttacking = true;
            AudioManager.instance.PlaySFX("Attack");
            crossAnim.Play("AttackCrosshair");
            Instantiate(attackObj, aimObj.transform.position, Quaternion.identity);
        }
        IEnumerator StopAtacking(float sec)
        {
            yield return new WaitForSeconds(sec);

            isAttacking = false;

        }
        public void TakeDamage(int damageAmount)
        {
            if (isHurting == false && isCanBeHurted)
            {
                //if (isConfuse) EndConfuse();
                AudioManager.instance.PlaySFX("Hurt");

                stats.currentHp -= damageAmount;
                stats.UpdateStats();
                isDashing = false;
                isHurting = true;
                isCanBeHurted = false;

                CameraController.instance.Shake();
                if (stats.currentHp <= 0)
                {
                    blackScene.gameObject.SetActive(true);
                    blackScene.Play("End");
                    AudioManager.instance.PlaySFX("Lose");
                    isDie = true;
                    Invoke("Die", 1f);
                }
                StartCoroutine(EndHurt(0.75f));
                print("Player Hurt");
            }
        }
        public IEnumerator EndHurt(float sec)
        {
            yield return new WaitForSeconds(sec);

            isHurting = false;
            isCanBeHurted = true;
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            // if (coll.tag == "Enemy")
            // {
            //     TakeDamage(1);
            // }
            if (coll.tag == "UndefeatEnemy")
            {
                if (rb.velocity == Vector2.zero) rb.velocity = Vector2.down;
                rb.velocity = -rb.velocity.normalized * 5;
                TakeDamage(1);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {

                rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 3;
                if (collision.gameObject.GetComponent<MonsterManager>().HP > 0) TakeDamage(1);

            }
            if (collision.gameObject.tag == "UndefeatEnemy")
            {
                rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 3;
                TakeDamage(1);
            }
            if (collision.gameObject.tag == "GruzMother")
            {
                rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 3;
                TakeDamage(1);
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {

                rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 3;
                if (collision.gameObject.GetComponent<MonsterManager>().HP > 0) TakeDamage(1);

            }
            if (collision.gameObject.tag == "UndefeatEnemy")
            {
                rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 3;
                TakeDamage(1);
            }
            if (collision.gameObject.tag == "GruzMother")
            {
                rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 3;
                TakeDamage(1);
            }
        }
        void Die()
        {
            SceneManagement.instance.MenuScene();
        }

    }
}
