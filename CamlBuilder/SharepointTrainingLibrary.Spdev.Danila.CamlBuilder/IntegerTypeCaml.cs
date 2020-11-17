//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class IntegerTypeCaml : CamlField
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public IntegerTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public IntegerTypeCaml(Guid guidName) : base(guidName)
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
                xElement.Add(this.ValueElement(TypeValue.Integer, values[0].ToString()));
            }
            else
            {
                foreach (int value in values)
                {
                    xElement.Add(this.ValueElement(TypeValue.Integer, value.ToString()));
                }
            }


            return xElement;
        }

        /// <summary>
        ///     Ниже приведены операции, которые работают с данным типом
        /// </summary>
        public XElement GreatThen(int intValue)
        {
            var xElement = new XElement(TypeValue.GreatThen);
            this.CreateConditionWithValues(xElement, intValue);

            return xElement;
        }

        public XElement GreatEqualsThen(int intValue)
        {
            var xElement = new XElement(TypeValue.GreatOrEquals);
            this.CreateConditionWithValues(xElement, intValue);

            return xElement;
        }

        public XElement LittleThen(int intValue)
        {
            var xElement = new XElement(TypeValue.LittleThen);
            this.CreateConditionWithValues(xElement, intValue);

            return xElement;
        }

        public XElement LittleEqualsThen(int intValue)
        {
            var xElement = new XElement(TypeValue.LittleEqualsThen);
            this.CreateConditionWithValues(xElement, intValue);

            return xElement;
        }

        public XElement Equals(int value)
        {
            var xElement = new XElement(TypeValue.Equals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement NotEquals(int value)
        {
            var xElement = new XElement(TypeValue.NotEquals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement In(params int[] value)
        {
            var xElement = new XElement(TypeValue.In);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }
    }
}