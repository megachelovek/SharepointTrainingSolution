//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder.CamlTypes
{
    using System;
    using System.Xml.Linq;

    public class LookupTypeCaml : UserTypeCaml
    {
        private readonly string _fieldName;

        private readonly Guid _guid;

        public LookupTypeCaml(string fieldName) : base(fieldName)
        {
            this._fieldName = fieldName;
        }

        public LookupTypeCaml(Guid guid) : base(guid)
        {
            this._guid = guid;
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
                    xElement.Add(this.ValueElement(TypeValue.Lookup, value));
                }
            }

            if (values[0] is int)
            {
                xElement.Add(this.RefElement(this._fieldName != null ? TypeValue.Name : TypeValue.Id,
                    this._fieldName ?? this._guid.ToString(), TypeValue.LookupId, "True"));
                foreach (int value in values)
                {
                    xElement.Add(this.ValueElement(TypeValue.Lookup, value.ToString()));
                }
            }

            return xElement;
        }
    }
}