//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class BooleanTypeCaml : CamlField
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public BooleanTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public BooleanTypeCaml(Guid guidName) : base(guidName)
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
            foreach (bool value in values)
            {
                xElement.Add(this.ValueElement(TypeValue.Boolean, value.ToString()));
            }

            return xElement;
        }

        public XElement Equals(bool value)
        {
            var xElement = new XElement(TypeValue.Equals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement NotEquals(bool value)
        {
            var xElement = new XElement(TypeValue.NotEquals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement In(params bool[] value)
        {
            var xElement = new XElement(TypeValue.In);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }
    }
}