using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSays.Managers
{
    public interface IInputManager
    {
        public Action OnInputDown {get; set;}
    }
}