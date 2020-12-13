using System;
using UnityEngine;

namespace SecondLayer
{
    [RequireComponent(typeof(Animator))]
    public class RobotAnimator : MonoBehaviour
    {
        public RythmObject headBob;
        public ParticleSystem particles;
        public ParticleSystem particlesLove;

        [Header("Changeable sprites")]
        public Sprite corpoHead;
        public Sprite corpoTorso;
        public Sprite funkyHappyHead;
        public Sprite corpoAngryHead;

        [Header("Body components")]
        public SpriteRenderer head;
        public SpriteRenderer torso;

        Animator animator;
        bool funky;


        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Init( bool isFunky )
        {
            funky = isFunky;
            
            if( !funky )
            {
                head.sprite = corpoHead;
                torso.sprite = corpoTorso;
            }
        }

        public void Show()
        {
            animator.SetTrigger( "show" );
        }

        public void Hit()
        {
            if( funky )
            {
                head.sprite = funkyHappyHead;
                animator.enabled = false;
                particlesLove.Play();
                headBob.enabled = true;
            }
            else
            {
                head.sprite = corpoAngryHead;
                animator.SetBool( "angry", true );
                //particles.Play();
            }
        }
    }
}
