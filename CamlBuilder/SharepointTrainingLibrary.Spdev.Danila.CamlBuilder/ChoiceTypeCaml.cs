//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class ChoiceTypeCaml : TextTypeCaml
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public ChoiceTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public ChoiceTypeCaml(Guid guidName) : base(guidName)
        {
            this._guid = guidName;
        }

        /// <summary>
        ///     Создает условие, привязанное конкретно к этому типу со своей спецификой
        /// </summary>
        /// <param name="xElement"></param>
        /// <param name="values"></param>
        /// <returns>Возвращает условие для выборки</returns>
        public override XElement CreateConditionWithValues(XElement xElement, params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            xElement.Add(this.RefElement(this._fieldName != null ? TypeValue.Name : TypeValue.Id,
                this._fieldName ?? this._guid.ToString()));
            foreach (string value in values)
            {
                xElement.Add(this.ValueElement(TypeValue.Choice, value));
            }

            return xElement;
        }
    }
}