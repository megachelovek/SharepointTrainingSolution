//  SharePointTraining.Spdev 2019

namespace SharePointTraining.Spdev.Danila.SharePoint.StudentDictionary.StudentLibrary
{
    using System;
    using System.Reflection;

    using Microsoft.SharePoint;

    public class StudentList
    {
        public StudentList(SPWeb spWeb)
        {
            this._spWeb = spWeb;
        }

        private SPWeb _spWeb { get; }

        public string Name => "StudentsList";

        protected string Url => "Lists/" + this.Name;

        /// <summary>
        ///     Создание списка студентов.
        /// </summary>
        /// <param name="web"></param>
        private void CreateStudentList()
        {
            Guid guid = this._spWeb.Lists.Add(this.Name, "Список студентов", SPListTemplateType.GenericList);
            SPList list = this._spWeb.Lists[guid];
            list.ContentTypesEnabled = true;
            list.ContentTypes.Add(new StudentContentType(this._spWeb).EnsureStudentContentType());
            SPView view = list.Views["Все элементы"];
            foreach (StudentField studentField in Student.StudentFields)
            {
                view.ViewFields.Add(studentField.InternalName);
            }

            view.Update();
            list.Update();

            this.CreateEventReceiver(list);
            this.CreateDefaultStudent(list);
        }


        /// <summary>
        ///     Cоздает список студентов с SharePoint
        /// </summary>
        /// <returns></returns>
        public void Provision()
        {
            this.Ensure();
        }

        /// <summary>
        ///     Возвращает список студентов с SharePoint
        /// </summary>
        /// <returns></returns>
        private SPList Ensure()
        {
            string urlList = this._spWeb.ServerRelativeUrl + this.Url;
            try
            {
                return this._spWeb.GetList(urlList);
            }
            catch (Exception e)
            {
                this.CreateStudentList();
                return this.Ensure();
            }
        }

        /// <summary>
        ///     Добавление ресиверов на список студент
        /// </summary>
        /// <param name="spList"></param>
        private void CreateEventReceiver(SPList spList)
        {
            string assembly = Assembly.GetExecutingAssembly().FullName;
            string receiverName = typeof(StudentReceivers).FullName;
            spList.EventReceivers.Add(SPEventReceiverType.ItemUpdated, assembly, receiverName);
            spList.Update();
        }

        /// <summary>
        ///     Добаляет стандартного студента
        /// </summary>
        /// <param name="spList"></param>
        private void CreateDefaultStudent(SPList spList)
        {
            SPListItem listItem = spList.AddItem();
            listItem[Student.StudentName.InternalName] = "Mark";
            listItem[Student.Birthday.Title] = new DateTime(1927,11,27);
            listItem[Student.Perseverance.Title] = 9;
            listItem[Student.CodeQuality.Title] = 8;
            listItem[Student.Skills.Title] = 7;
            listItem.Update();
        }
    }
}