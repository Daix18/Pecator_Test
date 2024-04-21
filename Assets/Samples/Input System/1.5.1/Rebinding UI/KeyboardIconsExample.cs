using System;
using UnityEngine.UI;


namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class KeyboardIconsExample : MonoBehaviour
    {
        public KeyboardMouseIcons keyboardMouseIcons;

        protected void OnEnable()
        {
            var rebindUIComponents = transform.GetComponentsInChildren<RebindActionUI>();
            foreach (var component in rebindUIComponents)
            {
                component.updateBindingUIEvent.AddListener(OnUpdateBindingDisplay);
                component.UpdateBindingDisplay();
            }
        }

        protected void OnUpdateBindingDisplay(RebindActionUI component, string bindingDisplayString, string deviceLayoutName, string controlPath)
        {
            if (string.IsNullOrEmpty(deviceLayoutName) || string.IsNullOrEmpty(controlPath))
                return;

            var icon = keyboardMouseIcons.GetSprite(controlPath);

            var textComponent = component.bindingText;
            var imageGO = textComponent.transform.parent.Find("ActionBindingIcon");
            var imageComponent = imageGO.GetComponent<Image>();

            if (icon != null)
            {
                textComponent.gameObject.SetActive(false);
                imageComponent.sprite = icon;
                imageComponent.gameObject.SetActive(true);
            }
            else
            {
                textComponent.gameObject.SetActive(true);
                imageComponent.gameObject.SetActive(false);
            }
        }

        [Serializable]
        public struct KeyboardMouseIcons
        {
            public Sprite key0;
            public Sprite key1;
            public Sprite key2;
            public Sprite key3;
            public Sprite key4;
            public Sprite key5;
            public Sprite key6;
            public Sprite key7;
            public Sprite key8;
            public Sprite key9;
            public Sprite keyA;
            public Sprite keyAlt;
            public Sprite keyArrowDown;
            public Sprite keyArrowLeft;
            public Sprite keyArrowRight;
            public Sprite keyArrowUp;
            public Sprite keyAsterisk;
            public Sprite keyB;
            public Sprite keyBackspaceAlt;
            public Sprite keyBackspace;
            public Sprite keyBracketLeft;
            public Sprite keyBracketRight;
            public Sprite keyC;
            public Sprite keyCtrl;
            public Sprite keyD;
            public Sprite keyE;
            public Sprite keyF;
            public Sprite keyF1;
            public Sprite keyF2;
            public Sprite keyF3;
            public Sprite keyF4;
            public Sprite keyF5;
            public Sprite keyF6;
            public Sprite keyF7;
            public Sprite keyF8;
            public Sprite keyF9;
            public Sprite keyF10;
            public Sprite keyF11;
            public Sprite keyF12;
            public Sprite keyG;
            public Sprite keyH;
            public Sprite keyI;
            public Sprite keyJ;
            public Sprite keyK;
            public Sprite keyL;
            public Sprite keyM;
            public Sprite keyMinus;
            public Sprite leftMouse;
            public Sprite rightMouse;
            public Sprite middleMouse;
            public Sprite keyN;
            public Sprite keyO;
            public Sprite keyP;
            public Sprite keyQ;
            public Sprite keyR;
            public Sprite keyS;
            public Sprite keyShift;
            public Sprite keyShiftAlt;
            public Sprite keySpace;
            public Sprite keyT;
            public Sprite keyTab;
            public Sprite keyU;
            public Sprite keyV;
            public Sprite keyW;
            public Sprite keyX;
            public Sprite keyY;
            public Sprite keyZ;

            public Sprite GetSprite(string controlPath)
            {
                switch (controlPath)
                {
                    case "0": return key0;
                    case "1": return key1;
                    case "2": return key2;
                    case "3": return key3;
                    case "4": return key4;
                    case "5": return key5;
                    case "6": return key6;
                    case "7": return key7;
                    case "8": return key8;
                    case "9": return key9;
                    case "A": return keyA;
                    case "Alt": return keyAlt;
                    case "ArrowDown": return keyArrowDown;
                    case "ArrowLeft": return keyArrowLeft;
                    case "ArrowRight": return keyArrowRight;
                    case "ArrowUp": return keyArrowUp;
                    case "Asterisk": return keyAsterisk;
                    case "B": return keyB;
                    case "BackspaceAlt": return keyBackspaceAlt;
                    case "Backspace": return keyBackspace;
                    case "BracketLeft": return keyBracketLeft;
                    case "BracketRight": return keyBracketRight;
                    case "C": return keyC;
                    case "Ctrl": return keyCtrl;
                    case "D": return keyD;
                    case "E": return keyE;
                    case "F": return keyF;
                    case "F1": return keyF1;
                    case "F2": return keyF2;
                    case "F3": return keyF3;
                    case "F4": return keyF4;
                    case "F5": return keyF5;
                    case "F6": return keyF6;
                    case "F7": return keyF7;
                    case "F8": return keyF8;
                    case "F9": return keyF9;
                    case "F10": return keyF10;
                    case "F11": return keyF11;
                    case "F12": return keyF12;
                    case "G": return keyG;
                    case "H": return keyH;
                    case "I": return keyI;
                    case "J": return keyJ;
                    case "K": return keyK;
                    case "L": return keyL;
                    case "M": return keyM;
                    case "Minus": return keyMinus;
                    case "N": return keyN;
                    case "O": return keyO;
                    case "P": return keyP;
                    case "Q": return keyQ;
                    case "R": return keyR;
                    case "S": return keyS;
                    case "Shift": return keyShift;
                    case "ShiftAlt": return keyShiftAlt;
                    case "Space": return keySpace;
                    case "T": return keyT;
                    case "Tab": return keyTab;
                    case "U": return keyU;
                    case "V": return keyV;
                    case "W": return keyW;
                    case "X": return keyX;
                    case "Y": return keyY;
                    case "Z": return keyZ;
                    case "LeftMouse": return leftMouse;
                    case "RightMouse": return rightMouse;
                    case "MiddleMouse": return middleMouse;
                }
                return null;
            }
        }
    }
}