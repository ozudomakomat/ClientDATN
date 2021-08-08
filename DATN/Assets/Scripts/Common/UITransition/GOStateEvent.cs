﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GOStateEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnable;

    private void OnEnable()
    {
        onEnable?.Invoke();
    }
}
