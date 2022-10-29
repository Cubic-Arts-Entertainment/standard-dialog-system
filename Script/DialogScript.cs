using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{

    // Adicione esse script em um objeto que tenha um trigger de colisão ativo.
    // Esse o sistema de lista para adicionar o dialogo que ocorrerá.
    // Essa lista seguirá a ordem que estiver no inspecionar.

    private bool col;
    private bool dialog;
    private int currentDialog = 0;

    public KeyCode input = KeyCode.E;
    public GameObject HUD;
    public Text text;
    public GameObject player;

    [TextArea]
    [SerializeField]
    private List<string> dialogs;

    private void Update()
    {
        if(Input.GetKeyDown(input) && col)
            if(dialog)
                NextDialog();
            else
            {
                print("começou a conversa");
                StartDialog();
            }
                
    }

    private void StartDialog()
    {
        HUD.SetActive(true);
        NextDialog();
        player.gameObject.GetComponent<FirstPersonController>().playerCanMove = false;
        player.gameObject.GetComponent<FirstPersonController>().enableHeadBob = false;
        player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dialog = true;
    }

    private void NextDialog()
    {
        if(currentDialog < dialogs.Count)
        {
            text.text = dialogs[currentDialog];
            currentDialog++;
        }
        else if(currentDialog == dialogs.Count)
        {
            ExitDialog();
        }  
    }

    private void ExitDialog()
    {
        dialog = false;
        player.gameObject.GetComponent<FirstPersonController>().playerCanMove = true;
        player.gameObject.GetComponent<FirstPersonController>().enableHeadBob = true;
        player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        HUD.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        col=true;
    }

    private void OnTriggerExit(Collider other)
    {
        col=false;
    }
}
