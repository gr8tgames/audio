namespace Gr8tGames.Audio
{
    using UnityEngine;

    public class MinMaxAudioSettingAttribute : PropertyAttribute{

        public float min;
        public float max;

        public MinMaxAudioSettingAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }  
}
