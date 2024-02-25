using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Game
{
    public class MonsterManager : MonoBehaviour, IHealth
    {
        public AxieFigure figure;
        GameObject gameController;
        public float HP;
        protected Rigidbody2D rb;
        protected Animator anim;
        protected Collider2D coli;

        protected GameObject player;
        public GameObject dieObj;
        protected bool isDie = false;
        public GameObject hurtEffect, popupText;


        Color color;
        void Awake()
        {
            // gameController = GameObject.FindGameObjectWithTag("GameController");
            // gameController.GetComponent<GameController>().numEnemy++;

            rb = gameObject.GetComponent<Rigidbody2D>();
            anim = gameObject.GetComponent<Animator>();
            coli = gameObject.GetComponent<Collider2D>();

            player = Player.instance.gameObject;

            // color = sprRen.material.color;

            // enabled = false;

            figure = gameObject.GetComponentInChildren<AxieFigure>();

        }

        private void LateUpdate()
        {
            if (HP <= 0 && isDie == false) Die();
        }


        IEnumerator RedBlinking(float sec)
        {
            while (color.g > 0)
            {
                yield return new WaitForSeconds(sec);
                color.g -= 0.1f;
                color.b -= 0.1f;
                if (color.g < 0) color.g = 0;
                if (color.b < 0) color.b = 0;
                // sprRen.material.color = color;
            }
            while (color.g < 1)
            {
                yield return new WaitForSeconds(sec);
                color.g += 0.1f;
                color.b += 0.1f;
                if (color.g > 1) color.g = 1;
                if (color.b > 1) color.b = 1;
                // sprRen.material.color = color;
            }
        }
        public void Die()
        {

            figure?.DoDieAnim();
            gameObject.GetComponent<LootBag>().InstantiateLoot(transform.position);


            isDie = true;
            GameController.instance.numEnemy--;
            //rb.gravityScale = 1;
            //sprRen.flipY = true;
            //coli.isTrigger = true;
            Destroy(gameObject, 0.5f);
        }

        //Cac ham duoi nay dung de ngung hoat dong cua Enemy khi ra khoi man hinh camera
        void OnBecameVisible()
        {
            enabled = true;
        }
        void OnBecameInvisible()
        {
            enabled = false;
        }
        void OnDestroy()
        {
            GameObject dieObject;
            dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
            Destroy(dieObject, 1);
            // GameObject dieObject;
            // dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
            // Destroy(dieObject, 1);
        }

        public void TakeDamage(int damageAmount)
        {
            HP -= damageAmount;
            GameObject damagePopup = Instantiate(popupText, transform.position, Quaternion.identity) as GameObject;
            damagePopup.transform.GetChild(0).GetComponent<TextMesh>().text = damageAmount.ToString();
            figure?.DoHurtAnim();
            CreateDustEffect();
            StartCoroutine(EndHurt(0.5f));
            AudioManager.instance.PlaySFX("AttackHit");
        }
        void CreateDustEffect()
        {
            Instantiate(hurtEffect, transform.position, Quaternion.identity);
        }
        IEnumerator EndHurt(float sec)
        {
            yield return new WaitForSeconds(sec);
            figure?.DoRunAnim();
        }
    }
}
