//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class DateTimeTypeCaml : CamlField
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public DateTimeTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public DateTimeTypeCaml(Guid guidName) : base(guidName)
        {
            this._guid = guidName;
        }

        /// <summary>
        ///     Создает условие, привязанное конкретно к этому типу со своей спецификой
        /// </summary>
        /// <param name="xElement"></param>
        /// <param name="values"></param>
        /// <returns>Возвращает условие для выборки</returns>
        public XElement CreateConditionWithValues(XElement xElement, params DateTime[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            xElement.Add(this.RefElement(this._fieldName != null ? TypeValue.Name : TypeValue.Id,
                this._fieldName ?? this._guid.ToString()));
            foreach (DateTime value in values)
            {
                if (value.Hour == 0 && value.Minute == 0 && value.Second == 0)
                {
                    xElement.Add(this.ValueElement(TypeValue.DateTime, value.ToString()));
                }
                else
                {
                    xElement.Add(this.ValueElement(TypeValue.DateTime, value.ToString(), TypeValue.IncludeTimeValue,
                        TypeValue.True));
                }
            }

            return xElement;
        }

        /// <summary>
        ///     Ниже приведены операции, которые работают с данным типом
        /// </summary>
        public XElement GreatThen(DateTime dateTimeValue)
        {
            var xElement = new XElement(TypeValue.GreatThen);
            this.CreateConditionWithValues(xElement, dateTimeValue);

            return xElement;
        }

        public XElement GreatEqualsThen(DateTime dateTimeValue)
        {
            var xElement = new XElement(TypeValue.GreatOrEquals);
            this.CreateConditionWithValues(xElement, dateTimeValue);

            return xElement;
        }

        public XElement LittleThen(DateTime dateTimeValue)
        {
            var xElement = new XElement(TypeValue.LittleThen);
            this.CreateConditionWithValues(xElement, dateTimeValue);

            return xElement;
        }

        public XElement LittleEqualsThen(DateTime dateTimeValue)
        {
            var xElement = new XElement(TypeValue.LittleEqualsThen);
            this.CreateConditionWithValues(xElement, dateTimeValue);

            return xElement;
        }

        public XElement Equals(DateTime value)
        {
            var xElement = new XElement(TypeValue.Equals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement NotEquals(DateTime value)
        {
            var xElement = new XElement(TypeValue.NotEquals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement In(params DateTime[] value)
        {
            var xElement = new XElement(TypeValue.In);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }
    }
}