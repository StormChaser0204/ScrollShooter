using System;
using UnityEngine;

namespace Engine.Utils {
    [Serializable]
    public class ValueComparer {
        private enum ComparsionType {
            Equal = 1,
            More = 2,
            MoreOrEqual = 3,
            Less = 4,
            LessOrEqual = 5
        }

        [SerializeField]
        private ComparsionType _comparsionType;

        [SerializeField]
        private float _value;

        public float Value => _value;

        public bool IsValidValue(float otherValue) {
            switch (_comparsionType) {
                case ComparsionType.Equal:
                    return Math.Abs(otherValue - _value) < float.Epsilon;
                case ComparsionType.More:
                    return otherValue > _value;
                case ComparsionType.MoreOrEqual:
                    return otherValue > _value || Math.Abs(otherValue - _value) < float.Epsilon;
                case ComparsionType.Less:
                    return otherValue < _value;
                case ComparsionType.LessOrEqual:
                    return otherValue < _value || Math.Abs(otherValue - _value) < float.Epsilon;
                default:
                    return false;
            }
        }

        public bool IsValidValue(int otherValue) {
            return IsValidValue((float)otherValue);
        }
    }
}