// Copyright © iSys.Spdev 2019 All rights reserved.

namespace iSys.Spdev.Danila.SharePoint.StudentDictionary.StudentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Microsoft.SharePoint;

    /// <summary>
    ///     Описание полей списка "студент", их создание и заполнение.
    /// </summary>
    public static class Student
    {
        #region Перечисление XML представлений

        /// <summary>
        ///     XML представление каждого поля из списка Студент
        /// </summary>
        internal static class XmLfields
        {
            public static XElement XStudentName()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Name"),
                    new XAttribute("DisplayName", "Student Name"),
                    new XAttribute("Name", "StudentName"),
                    new XAttribute("ID", "{220BEA49-06B2-4327-8313-A106A7FBB527}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "Text"),
                    new XAttribute("Description", string.Empty)
                );
            }

            public static XElement XStudentBirthday()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Birthday"),
                    new XAttribute("DisplayName", "Student Birthday"),
                    new XAttribute("Name", "StudentBirthday"),
                    new XAttribute("ID", "{4AE8C73E-54B8-438D-959B-CE7D0A1EEBC4}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "DateTime"),
                    new XAttribute("Description", string.Empty)
                );
            }

            public static XElement XStudentCharacteristic()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Characteristic"),
                    new XAttribute("DisplayName", "Student Characteristic"),
                    new XAttribute("Name", "StudentCharacteristic"),
                    new XAttribute("ID", "{9C3A79C3-F1DA-4A03-8FB3-121B5CCACBC8}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "Note"),
                    new XAttribute("Description", string.Empty)
                );
            }

            public static XElement XStudentPerseverance()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Perseverance"),
                    new XAttribute("DisplayName", "Student Perseverance"),
                    new XAttribute("Name", "StudentPerseverance"),
                    new XAttribute("ID", "{70C5943A-2FD2-46B3-968A-810B6169B089}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "Integer"),
                    new XAttribute("Description", "От 1 до 10")
                );
            }

            public static XElement XStudentCodeQuality()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Code quality"),
                    new XAttribute("DisplayName", "Student Code quality"),
                    new XAttribute("Name", "StudentCodeQuality"),
                    new XAttribute("ID", "{8D22403A-AC3A-4AB7-B6F9-C09BE881370A}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "Integer"),
                    new XAttribute("Description", "От 1 до 10")
                );
            }

            public static XElement XStudentSkills()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Skills"),
                    new XAttribute("DisplayName", "Student Skills"),
                    new XAttribute("Name", "StudentSkills"),
                    new XAttribute("ID", "{BEFD09EF-19E4-428C-A0D0-85E7EEFCE1EC}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "Integer"),
                    new XAttribute("Description", "От 1 до 10")
                );
            }

            public static XElement XStudentResult()
            {
                return new XElement("Field",
                    new XAttribute("Title", "Student Result"),
                    new XAttribute("DisplayName", "Student Result"),
                    new XAttribute("Name", "StudentResult"),
                    new XAttribute("ID", "{6E3B4970-33AE-4F8B-9244-B4CB5D404B67}"),
                    new XAttribute("Group", "Fields from student list"),
                    new XAttribute("Type", "Number"),
                    new XAttribute("Description", string.Empty)
                );
            }
        }

        #endregion

        #region Создание полей студента

        public static StudentField StudentName = new StudentField("Student Name", SPFieldType.Text,
            new Guid("{220BEA49-06B2-4327-8313-A106A7FBB527}"), XmLfields.XStudentName(), "StudentName");

        public static StudentField Birthday = new StudentField("Student Birthday", SPFieldType.DateTime,
            new Guid("{4AE8C73E-54B8-438D-959B-CE7D0A1EEBC4}"), XmLfields.XStudentBirthday(), "StudentBirthday");

        public static StudentField Characteristic = new StudentField("Student Characteristic", SPFieldType.Note,
            new Guid("{9C3A79C3-F1DA-4A03-8FB3-121B5CCACBC8}"), XmLfields.XStudentCharacteristic(),
            "StudentCharacteristic");

        public static StudentField Perseverance = new StudentField("Student Perseverance", SPFieldType.Integer, "от 1 до 10",
            new Guid("{70C5943A-2FD2-46B3-968A-810B6169B089}"), XmLfields.XStudentPerseverance(),
            "StudentPerseverance");

        public static StudentField CodeQuality = new StudentField("Student Code quality", SPFieldType.Integer, "от 1 до 10",
            new Guid("{8D22403A-AC3A-4AB7-B6F9-C09BE881370A}"), XmLfields.XStudentCodeQuality(), "StudentCodeQuality");

        public static StudentField Skills = new StudentField("Student Skills", SPFieldType.Integer, "от 1 до 10",
            new Guid("{BEFD09EF-19E4-428C-A0D0-85E7EEFCE1EC}"), XmLfields.XStudentSkills(), "StudentSkills");

        public static StudentField Result = new StudentField("Student Result", SPFieldType.Number,
            new Guid("{6E3B4970-33AE-4F8B-9244-B4CB5D404B67}"), XmLfields.XStudentResult(), "StudentResult");

        public static List<StudentField> StudentFields = new List<StudentField>
            {StudentName, Birthday, Characteristic, Perseverance, CodeQuality, Skills, Result};

        #endregion

        #region Операции с полями студента

        public static SPField EnsureField(SPWeb spWeb, StudentField studentField)
        {
            try
            {
                return spWeb.AvailableFields[studentField.FieldId];
            }
            catch (ArgumentException)
            {
                return CreateField(spWeb, studentField);
            }
        }

        private static SPField CreateField(SPWeb spWeb, StudentField studentField)
        {
            spWeb.Fields.AddFieldAsXml(studentField.XField.ToString(SaveOptions.DisableFormatting));
            return EnsureField(spWeb, studentField);
        }

        #endregion
    }
}