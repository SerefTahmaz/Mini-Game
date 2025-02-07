using System;

namespace SimonSays.Gameplay
{
    /// <summary>
    /// Interface for dealing with simon input events
    /// </summary>
    public interface ISimonInputHandler
    {
        /// <summary>
        /// Event when a simon button selected
        /// </summary>
        public Action<cSimonButton> OnInput
        {
            get;
            set;
        }
    }
}