using UnityEngine.UI;

namespace _Game.Scripts.UI.UISubs
{

    public class SubUISlider : SubUIBase<float, Slider>
    {
        public override void SetValue(float value)
        {
            component.value = value;
        }
    }
}