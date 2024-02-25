using AxieMixer.Unity;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Game
{
    public class AxieFigure : MonoBehaviour
    {
        public int id = 0;
        // public string genes;
        private SkeletonAnimation skeletonAnimation;
        // [SerializeField] SkeletonDataAsset[] avatar;
        public string currentAnimation, animationName;
        [SerializeField] private bool _flipX = false;
        public bool flipX
        {
            get
            {
                return _flipX;
            }
            set
            {
                _flipX = value;
                if (skeletonAnimation != null)
                {
                    skeletonAnimation.skeleton.ScaleX = (_flipX ? -1 : 1) * Mathf.Abs(skeletonAnimation.skeleton.ScaleX);
                }
            }
        }

        private void Awake()
        {
            skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();

            // Shouldn't be here, but it's useful
            // Mixer.Init();
            // Mixer.SpawnSkeletonAnimation(skeletonAnimation, id, genes);

            // skeletonAnimation.transform.localPosition = new Vector3(0f, -0.32f, 0f);
            // skeletonAnimation.transform.SetParent(transform, false);
            // skeletonAnimation.transform.localScale = new Vector3(1, 1, 1);
            // skeletonAnimation.skeleton.ScaleX = (_flipX ? -1 : 1) * Mathf.Abs(skeletonAnimation.skeleton.ScaleX);
            // skeletonAnimation.timeScale = 0.5f;
            // skeletonAnimation.skeleton.FindSlot("shadow").Attachment = null;
            // skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
            // skeletonAnimation.state.End += SpineEndHandler;
        }

        private void OnDisable()
        {
            if (skeletonAnimation != null)
            {
                // skeletonAnimation.state.End -= SpineEndHandler;
            }
        }

        public void DoJumpAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "action/move-forward", true);
        }
        public void DoRunAnim()
        {
            skeletonAnimation.timeScale = 1f;
            if (skeletonAnimation.AnimationName == "action/move-forward")
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, "action/move-forward", true);
        }
        public void DoIdleAnim()
        {
            skeletonAnimation.timeScale = 1f;
            if (skeletonAnimation.AnimationName == "action/idle/normal")
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, "action/idle/normal", true);
        }

        public void DoAttackMeleeAnim()
        {
            skeletonAnimation.timeScale = 1f;
            if (skeletonAnimation.AnimationName == "attack/ranged/cast-fly")
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false);
        }


        public void DoAttackRangedAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false);
        }

        public void DoBuffAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/tail-roll", false);
        }
        public void DoDieAnim()
        {
            skeletonAnimation.timeScale = 1f;
            if (skeletonAnimation.AnimationName == "defense/hit-die")
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, "defense/hit-die", false);
        }
        public void DoHurtAnim()
        {
            skeletonAnimation.timeScale = 1f;
            if (skeletonAnimation.AnimationName == "defense/hit-by-normal")
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, "defense/hit-by-normal", false);
        }
        public void DoMeleeAnim()
        {
            skeletonAnimation.timeScale = 1f;
            if (skeletonAnimation.AnimationName == "attack/melee/normal-attack")
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/melee/normal-attack", false);
        }
        // public void ChangeCharracter()
        // {
        //     if (id == 3) id = 0;
        //     skeletonAnimation.skeletonDataAsset = avatar[id];
        //     skeletonAnimation.Initialize(true);
        //     Debug.Log("Da chay");
        //     id++;
        // }
        // // public void SetAnimation(string animationName, bool loop, float timeScale)
        // {
        //     if (animationName.Equals(currentAnimation))
        //     {
        //         return;
        //     }
        //     skeletonAnimation.timeScale = timeScale;
        //     skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
        //     currentAnimation = animationName;
        // }


        // private void SpineEndHandler(TrackEntry trackEntry)
        // {
        //     string animation = trackEntry.Animation.Name;
        //     if (animation == "action/move-forward")
        //     {
        //         skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
        //         skeletonAnimation.timeScale = 0.5f;
        //     }
        // }
    }
}
