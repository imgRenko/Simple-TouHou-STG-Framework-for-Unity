using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using XNode;
public class FunctionNodeBased : Node
{
  

    public virtual void OnEnter() { 
    
    }

}
public abstract class FunctionNode : FunctionNodeBased {
    public abstract override void OnEnter();
}