using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class KeyBindScript : MonoBehaviour
{
    public TMP_Text p1Jump, p1Crouch, p1Left, p1Right;

    public GameObject currentKey;

    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    public InputActionAsset IAA;
    public InputActionRebindingExtensions.RebindingOperation rebindOperation;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise button text
        //up.text = IAA.FindActionMap("Player1").FindAction("Jump").GetBindingDisplayString();
        //down.text = IAA.FindActionMap("Player1").FindAction("Crouch").GetBindingDisplayString();
        //left.text = IAA.FindActionMap("Player1").FindAction("Walk").GetBindingDisplayString();
        //right.text = IAA.FindActionMap("Player1").FindAction("Walk").GetBindingDisplayString();
    }

    // Rebind key
    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                string currentButton = EventSystem.current.currentSelectedGameObject.name;

                IAA.FindActionMap(findPlayer(currentButton)).FindAction(getAction(currentButton)).Disable();
                rebindOperation = IAA.FindActionMap(findPlayer(currentButton)).FindAction(getAction(currentButton)).PerformInteractiveRebinding(getActionIndex(currentButton)).WithControlsHavingToMatchPath("<Keyboard>").Start();
                rebindOperation.OnComplete(operation =>
                {
                    operation.Dispose();
                    IAA.FindActionMap(findPlayer(currentButton)).FindAction(getAction(currentButton)).Enable();
                });

                // Update button text to newly selected key and turn button colour back to original colour
                currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();  
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;              
            }
        }
    }

    // Highlight button to different colour upon selection
    public void changeKey(GameObject clicked)
    {   
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    // Check if button is for player 1 or player 2
    private string findPlayer(string str)
    {
        if (str.Contains("P1"))
        {
            return "Player1";
        } else
        {
            return "Player2";
        }
    }

    // Get current move to have rebinded
    private string getAction(string str)
    {
        if (str.Contains("Jump"))
        {
            return "Jump";
        } 
        else if (str.Contains("Crouch"))
        {
            return "Crouch";
        } 
        else if (str.Contains("Left"))
        {
            return "Walk";
        }
        else if (str.Contains("Right"))
        {
            return "Walk";
        }
        else if (str.Contains("Light"))
        {
            return "LightAttack";
        }
        else if (str.Contains("Med"))
        {
            return "MedAttack";
        }
        else if (str.Contains("Heavy"))
        {
            return "HeavyAttack";
        }
        else
        {
            return null;
        }
    }

    // Get index of move if action is composite
    private int getActionIndex(string str)
    {
        if (str.Contains("Left"))
        {
            return 1;
        }
        else if (str.Contains("Right"))
        {
            return 2;
        } else
        {
            return 0;
        }
    }
}
