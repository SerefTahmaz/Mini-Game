using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{
    public Action OnInputDown {get; set;}
}
