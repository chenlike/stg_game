﻿using UnityEngine;
using System.Collections;
using Boss.zoufangzi;
using System;

public class Zoufangzi : Character
{


    void Start()
    {
        AddSpellCard("Boss.zoufangzi.Fei1",spellKeepTime:30f);
        Spell();
    }
    void Update()
    {

    }



}
