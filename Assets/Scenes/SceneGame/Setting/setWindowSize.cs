﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setWindowSize : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }
}
