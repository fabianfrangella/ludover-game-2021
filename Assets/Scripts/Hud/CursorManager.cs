using System;
using UnityEngine;

namespace Hud
{
    public class CursorManager : MonoBehaviour
    {
        public Texture2D melee;
        public Texture2D defaultCursor;
        public Texture2D magic;

        private void Start()
        {
            SetCursor(defaultCursor);
        }

        public void SetMeleeCursor()
        {
            SetCursor(melee);
        }

        public void SetMagicCursor()
        {
            SetCursor(magic);
        }

        private static void SetCursor(Texture2D c)
        {
            Cursor.SetCursor(c, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}