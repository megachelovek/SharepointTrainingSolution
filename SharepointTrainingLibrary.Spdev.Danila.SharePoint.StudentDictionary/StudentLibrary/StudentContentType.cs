// Copyright © iSys.Spdev 2019 All rights reserved.

namespace iSys.Spdev.Danila.SharePoint.StudentDictionary.StudentLibrary
{
    using Microsoft.SharePoint;

    public class StudentContentType
    {
        private readonly string _mainUrl = "Lists/StudentsList";

        public StudentContentType(SPWeb spWeb)
        {
            this._spWeb = spWeb;
        }

        private SPWeb _spWeb { get; }

        public SPContentTypeId _spContentTypeIdStudent => new SPContentTypeId("0x0100AD715DB5CC814E179B68F5805D222E7E");


        internal SPContentType EnsureStudentContentType()
        {
            var contentTypeStudent =
                new SPContentType(this._spContentTypeIdStudent, this._spWeb.ContentTypes, "Student");
            foreach (StudentField studentField in Student.StudentFields)
            {
                this.EnsureFieldInFieldCollection(contentTypeStudent, studentField);
            }

            return contentTypeStudent;
        }

        private void EnsureFieldInFieldCollection(SPContentType contentType, StudentField studentField)
        {
            var link = new SPFieldLink(Student.EnsureField(this._spWeb, studentField));
            if (contentType.FieldLinks[link.Id] == null)
            {
                contentType.FieldLinks.Add(link);
            }
        }
    }
}