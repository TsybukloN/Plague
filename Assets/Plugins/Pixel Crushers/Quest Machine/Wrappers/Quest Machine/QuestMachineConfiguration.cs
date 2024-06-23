// Copyright (c) Pixel Crushers. All rights reserved.

using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrushers.QuestMachine.Wrappers
{

    /// <summary>
    /// This wrapper class keeps references intact if you switch between the 
    /// compiled assembly and source code versions of the original class.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Quest Machine/Quest Machine Configuration")]
    public class QuestMachineConfiguration : PixelCrushers.QuestMachine.QuestMachineConfiguration
    {
        protected override void LateUpdate()
        {
            
        }
    }
}