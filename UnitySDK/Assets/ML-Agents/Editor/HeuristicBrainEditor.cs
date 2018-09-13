﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.Linq;

namespace MLAgents
{
/*
 This code is meant to modify the behavior of the inspector on Brain Components.
 Depending on the type of brain that is used, the available fields will be modified in the inspector accordingly.
*/
//    
    [CustomEditor(typeof(HeuristicBrain))]
    public class ScriptableBrainEditor : BrainEditor
    {

        public override void OnInspectorGUI()
        {
            HeuristicBrain brain = (HeuristicBrain) target;
            base.OnInspectorGUI();
            
            brain.decisionScript = EditorGUILayout.ObjectField(
                "Decision Script", brain.decisionScript, typeof(MonoScript), true) as MonoScript;

            /*If the monoscript is not a decision then do not validate it*/

            if (brain.decisionScript != null)
            {
                var decisionInstance = (CreateInstance(brain.decisionScript.name) as Decision);
                if (decisionInstance == null)
                {
                    brain.decisionScript = null;
                    Debug.LogError(
                        "Instance of " + brain.decisionScript.name + " couldn't be created. " +
                        "The the script class needs to derive from ScriptableObject.");
                }
            }


            if (brain.decisionScript == null)
            {
                EditorGUILayout.HelpBox("You need to add a 'Decision' component to this Object",
                    MessageType.Error);
            }
        }
    }
}