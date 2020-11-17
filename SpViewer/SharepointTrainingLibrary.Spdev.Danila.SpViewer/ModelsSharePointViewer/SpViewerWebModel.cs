//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;

    public class SpViewerWebModel : IModelSharePoint
    {
        public SpViewerWebModel(SPWeb spWeb)
        {
            this.Name = spWeb?.Title ?? spWeb.Url;
            this.SpViewerType = spWeb.GetType();
            this.SharePointEntity = spWeb;
            this.InitCategoryDictionary();
        }

        public string Name { get; }

        public Type SpViewerType { get; }

        public object SharePointEntity { get; }

        public Dictionary<string, Type> CategoryDictionary => this.InitCategoryDictionary();

        /// <summary>
        ///     Список дочерних категорий модели
        /// </summary>
        public Dictionary<string, Type> InitCategoryDictionary()
        {
            var dictionaryCategory = new Dictionary<string, Type>(3);
            Assembly assem = typeof(Program).Assembly;
            dictionaryCategory.Add("Списки (Lists)", typeof(SPList));
            dictionaryCategory.Add("Разрешения (Role assignments)", typeof(SPRoleAssignment));
            dictionaryCategory.Add("Пользователи и группы (Principals)", typeof(SPPrincipal));
            return dictionaryCategory;
        }
    }
}