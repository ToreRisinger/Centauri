﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    class ActionConfiguration
    {
        public EPlayerAction action;
        public KeyCode key;
        public bool isActivation;

        public ActionConfiguration(EPlayerAction _action, KeyCode _key, bool _isActivation)
        {
            action = _action;
            key = _key;
            isActivation = _isActivation;
        }
    }

    private static List<KeyCode> keyList = new List<KeyCode> {
        KeyCode.W,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.LeftControl,
        KeyCode.RightControl,
        KeyCode.Space,
        KeyCode.KeypadEnter,
        KeyCode.LeftShift,
        KeyCode.RightShift,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow
    };

    private static List<ActionConfiguration> actionConfigurations = new List<ActionConfiguration> {
        new ActionConfiguration(EPlayerAction.UP, KeyCode.UpArrow, false),
        new ActionConfiguration(EPlayerAction.DOWN, KeyCode.DownArrow, false),
        new ActionConfiguration(EPlayerAction.RIGHT, KeyCode.RightArrow, false),
        new ActionConfiguration(EPlayerAction.LEFT, KeyCode.LeftArrow, false),
        new ActionConfiguration(EPlayerAction.HOLD_DIRECTION, KeyCode.LeftControl, false),
        new ActionConfiguration(EPlayerAction.HOLD_POSITION, KeyCode.LeftShift, false)
    };

    private static HashSet<KeyCode> holdKeys;
    private static HashSet<KeyCode> pressedKeys;

    private static HashSet<KeyCode> tmpHoldKeys;
    private static HashSet<KeyCode> tmpPressedKeys;

    
    private static HashSet<EPlayerAction> actionActivations;

    private bool updateRanBetweenFixed = false;

    private void Awake()
    {

        holdKeys = new HashSet<KeyCode>();
        pressedKeys = new HashSet<KeyCode>();

        tmpHoldKeys = new HashSet<KeyCode>();
        tmpPressedKeys = new HashSet<KeyCode>();

        actionActivations = new HashSet<EPlayerAction>();
    }

    private void FixedUpdate()
    {
        if(!updateRanBetweenFixed)
        {
            pressedKeys = new HashSet<KeyCode>();
        } 
        else
        {
            holdKeys = tmpHoldKeys;
            pressedKeys = tmpPressedKeys;
        }

        //Reset tmpMaps
        tmpHoldKeys = new HashSet<KeyCode>();
        tmpPressedKeys = new HashSet<KeyCode>();

        //Reset activations
        actionActivations = new HashSet<EPlayerAction>();

        foreach(ActionConfiguration actionConfiguration in actionConfigurations) 
        {
            if((actionConfiguration.isActivation && pressedKeys.Contains(actionConfiguration.key)) || (!actionConfiguration.isActivation && holdKeys.Contains(actionConfiguration.key)))
            {
                actionActivations.Add(actionConfiguration.action);
            }
        }

        updateRanBetweenFixed = false;
    }

    private void Update()
    {
        updateRanBetweenFixed = true;

        for(int i = 0; i < keyList.Count; i++)
        {
            KeyCode key = keyList[i];
            if (Input.GetKey(key))
            {
                if(!holdKeys.Contains(key))
                {
                    tmpPressedKeys.Add(key);
                }

                tmpHoldKeys.Add(key);
            }
        }
    }

    public static bool isKeyHold(KeyCode keyCode) 
    {
        return holdKeys.Contains(keyCode); 
    }

    public static bool isKeyPressed(KeyCode keyCode)
    {
        return pressedKeys.Contains(keyCode);
    }

    public static bool isActionTriggered(EPlayerAction action)
    {
        return actionActivations.Contains(action);
    }

    public static HashSet<EPlayerAction> GetActions()
    {
        return actionActivations;
    }
}
