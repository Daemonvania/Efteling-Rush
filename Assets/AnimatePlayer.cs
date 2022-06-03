using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
  public Animator _animator;
  
  public void AnimateDuck()
  {
    _animator.SetTrigger("Duck");
  }
}
