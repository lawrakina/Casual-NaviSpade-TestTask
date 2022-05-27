using UnityEngine;


namespace Code.Player{
    public class AnimatorParameters{
        private readonly Animator _animator;
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int Death = Animator.StringToHash("Death");
        public bool Run{
            set => _animator.SetBool(IsRun, value);
        }
        public bool Die{
            set => _animator.SetBool(Death, value);
        }

        public AnimatorParameters(Animator animator){
            _animator = animator;
        }
    }
}