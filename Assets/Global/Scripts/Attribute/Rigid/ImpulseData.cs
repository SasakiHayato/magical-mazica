using UnityEngine;

public partial class RigidOperator : MonoBehaviour
{
    /// <summary>
    /// 衝撃を管理するデータクラス
    /// </summary>
    class ImpulseData
    {
        public ImpulseData(RigidMasterData.ImpulseDirectionType directionType)
        {
            DirectionType = directionType;
        }

        float _timer = 0;
        float _inetiaTimer = 0;

        public float Value { get; private set; } = 0;
        public bool OnImpulse { get; private set; } = false;
        public bool IsMoveLock { get; private set; } = false;

        public RigidMasterData.ImpulseDirectionType DirectionType { get; private set; }

        public void Setup(float value, bool isMoveLock)
        {
            _timer = 0;
            _inetiaTimer = 0;
            Value = value;

            IsMoveLock = isMoveLock;
            OnImpulse = true;
        }

        public float GetValue()
        {
            if (!OnImpulse) return 0;

            _timer += Time.fixedDeltaTime * RigidMasterData.GravityScale;

            float gravity = RigidMasterData.Gravity * -1;
            float currentValue = Mathf.Abs(Value) * _timer - gravity * _timer * _timer * 0.5f;

            if (currentValue <= 0)
            {
                Initalize();
            }

            return currentValue * Mathf.Sign(Value);
        }

        public float OnInertia(float rate)
        {
            _inetiaTimer += Time.fixedDeltaTime * rate;

            float value = Mathf.Lerp(Value, 0, _inetiaTimer);

            if (value == 0)
            {
                Initalize();
            }

            return value;
        }

        public void Initalize()
        {
            OnImpulse = false;
            IsMoveLock = false;
            _timer = 0;
            _inetiaTimer = 0;
            Value = 0;
        }
    }
}