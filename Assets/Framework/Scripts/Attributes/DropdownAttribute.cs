using UnityEngine;

namespace Framework
{
    public class DropdownAttribute : PropertyAttribute
    {
        public readonly object[] dropdownValues;

        public DropdownAttribute(params object[] dropdownValues)
        {
            this.dropdownValues = dropdownValues;
        }
    }
}
