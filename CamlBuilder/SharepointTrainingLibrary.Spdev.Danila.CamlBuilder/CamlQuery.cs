//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.CamlBuilder
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class CamlQuery
    {
        /// <summary>
        ///     Реализует логическое соединение ИЛИ
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>Выражение с уже готовым ИЛИ</returns>
        public XElement Or(params XElement[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            XElement newXElement = conditions[0];
            var xElement = new XElement(TypeValue.Or);
            if (conditions.Length == 2)
            {
                xElement = this.Or(newXElement, conditions[1]);
                return xElement;
            }

            foreach (XElement condition in conditions.Skip(1))
            {
                newXElement = this.Or(newXElement, condition);
            }

            xElement = newXElement;
            return xElement;
        }

        public XElement Or(XElement condition1, XElement condition2)
        {
            var xElement = new XElement(TypeValue.Or);
            xElement.Add(condition1);
            xElement.Add(condition2);
            return xElement;
        }

        /// <summary>
        ///     Реализует логическое соединение И
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>Выражение с уже готовым И</returns>
        public XElement And(params XElement[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            XElement newXElement = conditions[0];
            XElement xElement = null;
            if (conditions.Length == 2)
            {
                xElement = this.And(newXElement, conditions[1]);
                return xElement;
            }

            foreach (XElement condition in conditions.Skip(1))
            {
                newXElement = this.And(newXElement, condition);
            }

            xElement = newXElement;
            return xElement;
        }

        public XElement And(XElement condition1, XElement condition2)
        {
            var xElement = new XElement(TypeValue.And);
            xElement.Add(condition1);
            xElement.Add(condition2);
            return xElement;
        }

        /// <summary>
        ///     Реализует запрос SELECT WHERE
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>Готовую выборку</returns>
        public XElement Where(params XElement[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            var xElement = new XElement(TypeValue.Where);
            if (conditions.Length == 1)
            {
                xElement.Add(conditions[0]);
            }
            else
            {
                xElement.Add(this.And(conditions));
            }

            return xElement;
        }

        /// <summary>
        ///     Реализует сортировку по полям
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>Готовую сортировку</returns>
        public XElement OrderBy(params OrderByItem[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            var xElement = new XElement(TypeValue.OrderBy);
            foreach (OrderByItem condition in conditions)
            {
                if (condition.Ascending)
                {
                    xElement.Add(new XElement(TypeValue.FieldRef, new XAttribute(TypeValue.Name, condition.FieldName)));
                }
                else
                {
                    xElement.Add(new XElement(TypeValue.FieldRef, new XAttribute(TypeValue.Name, condition.FieldName),
                        new XAttribute(TypeValue.Ascending, condition.Ascending)));
                }
            }

            return xElement;
        }

        /// <summary>
        ///     Реализует  перечисление полей
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>Готовое  перечисление полей</returns>
        public XElement ViewFields(params string[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            var xElement = new XElement(TypeValue.ViewFields);
            foreach (string condition in conditions)
            {
                xElement.Add(new XElement(TypeValue.FieldRef, new XAttribute(TypeValue.Name, condition)));
            }

            return xElement;
        }

        public class OrderByItem
        {
            public OrderByItem(string name)
            {
                this.FieldName = name;
                this.Ascending = true;
            }

            public OrderByItem(string name, bool ascending)
            {
                this.FieldName = name;
                this.Ascending = ascending;
            }

            public bool Ascending { get; }

            public string FieldName { get; }
        }
    }
}