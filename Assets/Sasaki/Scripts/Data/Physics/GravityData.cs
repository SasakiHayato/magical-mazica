using UnityEngine;

namespace CustomPhysics.Data
{
    /// <summary>
    /// �d�̓f�[�^
    /// </summary>

    [System.Serializable]
    public class GravityData
    {
        [SerializeField] Vector2 _dir; 
        [SerializeField] float _scale;

        float _timer;

        /// <summary>
        /// �d��
        /// </summary>
        public Vector2 Gravity => Process();

        Vector2 Process()
        {
            float gravity = Physics2D.gravity.y;
            _timer += Time.fixedDeltaTime * _scale;

            float v = gravity * _timer;
            float x = _dir.x * v;
            float y = _dir.y * v;

            if (x <= 0)
            {
                x = 1;
            }

            if (y <= 0)
            {
                y = gravity;
            }

            return new Vector2(x, y);
        }

        /// <summary>
        /// ������
        /// </summary>
        public void Initalize()
        {
            _timer = 0;
        }
    }
}