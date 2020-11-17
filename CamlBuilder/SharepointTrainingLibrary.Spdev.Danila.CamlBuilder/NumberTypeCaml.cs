//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class NumberTypeCaml : IntegerTypeCaml
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public NumberTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public NumberTypeCaml(Guid guidName) : base(guidName)
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
            if (values.Length == 1)
            {
                xElement.Add(this.ValueElement(TypeValue.Number, values[0].ToString()));
            }
            else
            {
                foreach (int value in values)
                {
                    xElement.Add(this.ValueElement(TypeValue.Number, value.ToString()));
                }
            }

            return xElement;
        }
    }
}