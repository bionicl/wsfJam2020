using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SecondLayer
{
    public class RobotController : MonoBehaviour
    {
        public TargetBehaviour targetBehaviour;
        public RobotAnimator robotAnimation;

        public float minTimeToComeOut = 0.2f;
        public float maxTimeToComeOut = 1f;
        
        void Start()
        {
            targetBehaviour.gotHit.AddListener( OnHit );
            StartCoroutine( WaitToComeOut() );
        }

        public void Init( bool isFunky )
        {
            robotAnimation.Init( isFunky );
            targetBehaviour.type = isFunky ? TargetType.Funky : TargetType.Rat;
            targetBehaviour.enabled = false;
        }

        void OnHit()
        {
            robotAnimation.Hit();
        }

        void ComeOut()
        {
            robotAnimation.Show();
            targetBehaviour.enabled = true;
        }

        IEnumerator WaitToComeOut()
        {
            yield return new WaitForSeconds( Random.Range( minTimeToComeOut, maxTimeToComeOut ) );
            ComeOut();
        }
    }
}
