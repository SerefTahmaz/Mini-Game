using UnityEngine;

namespace SimonSays.Managers
{
    public interface ISoundManager
    {
        public void PlayClip(AudioClip clip);
        public void SetActive(bool state);
        public void PlayPop();
        public void SuccessSound();
        public void FailSound();
        public void PlayClick();
        public void PlayMouseEnter();
        public void PlaySwoosh();
        public void PlayAmbient();
        public void PauseAmbient();
        public void PlayGameFail();
    }
}