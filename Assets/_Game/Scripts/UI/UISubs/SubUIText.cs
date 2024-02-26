using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI.UISubs
{

    [RequireComponent(typeof(TMP_Text))]
    public class SubUIText : SubUIBase<string, TMP_Text>
    {
        public override void SetValue(string value)
        {
            component.text = value;
        }
    }
}