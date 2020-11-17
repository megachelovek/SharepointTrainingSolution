//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;

    public class SpViewerSiteModel : IModelSharePoint
    {
        public SpViewerSiteModel(SPSite spSite)
        {
            this.Name = spSite?.Url ?? "";
            this.SpViewerType = spSite.GetType();
            this.SharePointEntity = spSite;
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
            var dictionaryCategory = new Dictionary<string, Type>(4);
            Assembly assem = typeof(Program).Assembly;
            dictionaryCategory.Add("Сайты  (Webs)", typeof(SPWeb));
            dictionaryCategory.Add("Возможности (Features)", typeof(SPFeature));
            dictionaryCategory.Add("Типы содержимого (Content types)", typeof(SPContentType));
            dictionaryCategory.Add("Поля (Fields)", typeof(SPField));
            return dictionaryCategory;
        }
    }
}