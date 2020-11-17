//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class UserTypeCaml : CamlField
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public UserTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public UserTypeCaml(Guid guidName) : base(guidName)
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

            if (values[0] is string)
            {
                xElement.Add(this.RefElement(this._fieldName != null ? TypeValue.Name : TypeValue.Id,
                    this._fieldName ?? this._guid.ToString()));
                foreach (string value in values)
                {
                    xElement.Add(this.ValueElement(TypeValue.User, value));
                }
            }

            if (values[0] is int[])
            {
                var arrayInt = (int[]) values[0];
                xElement.Add(this.RefElement(this._fieldName != null ? TypeValue.Name : TypeValue.Id,
                    this._fieldName ?? this._guid.ToString(), TypeValue.LookupId, "True"));
                foreach (int value in arrayInt)
                {
                    xElement.Add(this.ValueElement(TypeValue.User, value.ToString()));
                }
            }

            return xElement;
        }


        /// <summary>
        ///     Ниже приведены операции, которые работают с данным типом
        /// </summary>
        public XElement Includes(string values)
        {
            var xElement = new XElement(TypeValue.Includes);
            this.CreateConditionWithValues(xElement, values);

            return xElement;
        }

        public XElement NotIncludes(string values)
        {
            var xElement = new XElement(TypeValue.In);
            this.CreateConditionWithValues(xElement, values);

            return xElement;
        }

        public XElement Equals(string value)
        {
            var xElement = new XElement(TypeValue.Equals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement NotEquals(string value)
        {
            var xElement = new XElement(TypeValue.NotEquals);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement In(params string[] value)
        {
            var xElement = new XElement(TypeValue.In);
            this.CreateConditionWithValues(xElement, value);

            return xElement;
        }

        public XElement Includes(int values)
        {
            var xElement = new XElement(TypeValue.Includes);
            this.CreateConditionWithValues(xElement, values);

            return xElement;
        }

        public XElement NotIncludes(int values)
        {
            var xElement = new XElement(TypeValue.In);
            this.CreateConditionWithValues(xElement, values);

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