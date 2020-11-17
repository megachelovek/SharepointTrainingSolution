//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class CamlField
    {
        public CamlField(string fieldName)
        {
            this.FieldName = fieldName;
        }

        public CamlField(Guid guid)
        {
            this.Guid = guid;
        }

        private Guid Guid { get; }

        private string FieldName { get; }

        public XElement IsNull()
        {
            var xElement = new XElement(TypeValue.IsNull);
            this.CreateConditionWithoutValues(xElement);
            return xElement;
        }

        public XElement IsNotNull()
        {
            var xElement = new XElement(TypeValue.IsNotNull);
            this.CreateConditionWithoutValues(xElement);

            return xElement;
        }


        public virtual XElement CreateConditionWithValues(XElement xElement, params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            xElement.Add(new XElement(TypeValue.FieldRef,
                new XAttribute(this.FieldName != null ? TypeValue.Name : TypeValue.Id,
                    this.FieldName ?? this.Guid.ToString())));
            foreach (object value in values)
            {
                xElement.Add(new XElement(TypeValue.Value, new XAttribute(TypeValue.Type, TypeValue.Value),
                    value.ToString()));
            }

            return xElement;
        }

        /// <summary>
        ///     Создание запроса без значений
        /// </summary>
        /// <param name="xElement"></param>
        /// <returns></returns>
        public XElement CreateConditionWithoutValues(XElement xElement)
        {
            xElement.Add(new XElement(TypeValue.FieldRef, new XAttribute(TypeValue.Name, this.FieldName)));
            return xElement;
        }

        /// <summary>
        ///     Создание Ref тэга
        /// </summary>
        /// <param name="fieldTypeName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public XElement RefElement(string fieldTypeName, string fieldName, params string[] specialAttribute)
        {
            XElement xElement;
            if (specialAttribute.Length > 0)
            {
                xElement = new XElement(TypeValue.FieldRef, new XAttribute(fieldTypeName, fieldName),
                    new XAttribute(specialAttribute[0], specialAttribute[1]));
            }
            else
            {
                xElement = new XElement(TypeValue.FieldRef, new XAttribute(fieldTypeName, fieldName));
            }

            return xElement;
        }

        /// <summary>
        ///     Создание Value тэга
        /// </summary>
        /// <param name="fieldTypeValue"></param>
        /// <param name="value"></param>
        /// <param name="specialAttribute"></param>
        /// <returns></returns>
        public XElement ValueElement(string fieldTypeValue, string value, params string[] specialAttribute)
        {
            XElement xElement;
            if (specialAttribute.Length > 0)
            {
                xElement = new XElement(new XElement(TypeValue.Value, new XAttribute(TypeValue.Type, fieldTypeValue),
                    new XAttribute(specialAttribute[0], specialAttribute[1]), value));
            }
            else
            {
                xElement = new XElement(new XElement(TypeValue.Value, new XAttribute(TypeValue.Type, fieldTypeValue),
                    value));
            }

            return xElement;
        }
    }
}