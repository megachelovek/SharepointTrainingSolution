//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;

    public class SpViewerWebApplicationModel : IModelSharePoint
    {
        public SpViewerWebApplicationModel(SPWebApplication spWebApplication)
        {
            this.Name = spWebApplication?.Name ?? "";
            this.SpViewerType = spWebApplication.GetType();
            this.SharePointEntity = spWebApplication;
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
            dictionaryCategory.Add("Сайт-коллекции (Sites)", typeof(SPSite));
            dictionaryCategory.Add("Возможности (Features)", typeof(SPFeature));
            dictionaryCategory.Add("Базы данных содержимого (Content databases)", typeof(SPContentDatabase));
            return dictionaryCategory;
        }
    }
}