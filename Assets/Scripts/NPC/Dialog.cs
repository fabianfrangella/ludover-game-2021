using System;
using System.Collections;
using UnityEngine;

namespace NPC
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] public Canvas dialog;
        private bool canInteract = false;

        private void Start()
        {
            dialog.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (canInteract && Input.GetKeyDown(KeyCode.F))
            {
                EnableDialog();
            } 
        }

        private void EnableDialog()
        {
            dialog.gameObject.SetActive(true);
        }

        private IEnumerator DisableDialog()
        {
            yield return new WaitForSeconds(2);
            dialog.gameObject.SetActive(false);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            canInteract = other.collider.CompareTag("Player");
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                canInteract = false;
                StartCoroutine(nameof(DisableDialog));
            }
        }
    }
}