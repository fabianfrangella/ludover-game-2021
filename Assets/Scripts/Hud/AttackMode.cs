using UnityEngine;
using UnityEngine.UI;

public class AttackMode : MonoBehaviour
{
    public Transform sword;
    public Transform wand;

    public bool isUsingMagic;
    private void Start()
    {
        isUsingMagic = false;
    }

    private void HighLightAttackMode()
    {
        if (!isUsingMagic)
        {
            wand.GetChild(0).GetComponent<Image>().enabled = false;
            sword.GetChild(0).GetComponent<Image>().enabled = true;
            return;
        }
        sword.GetChild(0).GetComponent<Image>().enabled = false;
        wand.GetChild(0).GetComponent<Image>().enabled = true;
    }


    public void SetSelectedSpell(string spell)
    {
        for (var i = 0; i < wand.childCount; i++)
        {
            var child = wand.GetChild(i);
            if (child.childCount > 0)
            {
                if (child.name.Equals(spell))
                {
                    child.GetChild(0).GetComponent<Image>().enabled = true;
                    continue;
                }
                child.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
        HighLightAttackMode();
    }
}
