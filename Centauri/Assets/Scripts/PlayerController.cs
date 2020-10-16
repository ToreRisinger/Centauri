using System.Collections.Generic;
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
        KeyCode.RightShift
    };

    private static List<ActionConfiguration> actionConfigurations = new List<ActionConfiguration> {
        new ActionConfiguration(EPlayerAction.UP, KeyCode.W, false),
        new ActionConfiguration(EPlayerAction.DOWN, KeyCode.S, false),
        new ActionConfiguration(EPlayerAction.RIGHT, KeyCode.D, false),
        new ActionConfiguration(EPlayerAction.LEFT, KeyCode.A, false),
        new ActionConfiguration(EPlayerAction.HOLD_DIRECTION, KeyCode.LeftControl, false),
        new ActionConfiguration(EPlayerAction.HOLD_POSITION, KeyCode.LeftShift, false)
    };

    private static bool[] movementInputs;
    private static HashSet<KeyCode> holdKeys;
    private static HashSet<KeyCode> pressedKeys;

    private static HashSet<KeyCode> tmpHoldKeys;
    private static HashSet<KeyCode> tmpPressedKeys;

    
    private static HashSet<EPlayerAction> actionActivations;

    private bool updateRanBetweenFixed = false;

    private void Awake()
    {
        movementInputs = new bool[7];

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

        movementInputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.LeftControl),
            Input.GetKey(KeyCode.RightControl),
            Input.GetKey(KeyCode.LeftShift)
        };
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
