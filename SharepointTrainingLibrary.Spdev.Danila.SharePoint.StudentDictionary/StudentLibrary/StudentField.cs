//  SharePointTraining.Spdev 2019

namespace SharePointTraining.Spdev.Danila.SharePoint.StudentDictionary.StudentLibrary
{
    using System;
    using System.Xml.Linq;

    using Microsoft.SharePoint;

    public class StudentField
    {
        public StudentField(string title, SPFieldType type, Guid guid, XElement xElement, string internalName)
        {
            this.InternalName = internalName;
            this.Title = title;
            this.Type = type;
            this.XField = xElement;
            this.FieldId = guid;
        }

        public StudentField(string title, SPFieldType type, string description, Guid guid, XElement xElement,
            string internalName)
        {
            this.Title = title;
            this.Type = type;
            this.Description = description;
            this.XField = xElement;
            this.InternalName = internalName;
            this.FieldId = guid;
        }

        public string Title { get; }

        public SPFieldType Type { get; }

        public string Description { get; }

        public XElement XField { get; }

        public Guid FieldId { get; }

        public string InternalName { get; }
    }
}