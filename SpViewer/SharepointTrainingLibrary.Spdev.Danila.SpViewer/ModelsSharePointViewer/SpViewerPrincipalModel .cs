//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    public class SpViewerPrincipalModel : IModelSharePoint
    {
        public SpViewerPrincipalModel(SPPrincipal spPrincipal)
        {
            this.Name = spPrincipal?.Name ?? "";
            this.SpViewerType = spPrincipal.GetType();
            this.SharePointEntity = spPrincipal;
            this.InitCategoryDictionary();
        }

        public SpViewerPrincipalModel(SPGroup spGroup)
        {
            this.Name = spGroup?.Name ?? "";
            this.SpViewerType = spGroup.GetType();
            this.SharePointEntity = spGroup;
            this.InitCategoryDictionary();
        }

        public SpViewerPrincipalModel(SPUser spUser)
        {
            this.Name = spUser?.Name ?? "";
            this.SpViewerType = spUser.GetType();
            this.SharePointEntity = spUser;
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
            var dictionaryCategory = new Dictionary<string, Type>(6);
            dictionaryCategory.Add("Администраторы сайт-коллекции (Site admins)", typeof(SPPrincipal));
            dictionaryCategory.Add("Пользователи сайтов (Site users)", typeof(SPPrincipal));
            dictionaryCategory.Add("Пользователи (Users)", typeof(SPPrincipal));
            dictionaryCategory.Add("Все пользователи (All users)", typeof(SPPrincipal));
            dictionaryCategory.Add("Группы сайт-коллекции (Site groups)", typeof(SPPrincipal));
            dictionaryCategory.Add("Группы  (Groups)", typeof(SPPrincipal));
            return dictionaryCategory;
        }
    }
}