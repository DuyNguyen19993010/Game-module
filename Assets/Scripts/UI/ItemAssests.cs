using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssests : MonoBehaviour
{
    public static ItemAssests Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public Sprite fortunePouch;
    public Sprite crimsonAsh;
    public Sprite homingAsh;
    public Sprite shiroinu;
    public Sprite kuroinu;
}
