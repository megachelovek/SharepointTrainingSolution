//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint.Administration;

    public class SpViewerContentDatabaseModel : IModelSharePoint
    {
        public SpViewerContentDatabaseModel(SPContentDatabase spContentDatabase)
        {
            this.Name = spContentDatabase?.Name ?? "";
            this.SpViewerType = spContentDatabase.GetType();
            this.SharePointEntity = spContentDatabase;
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
            var dictionaryCategory = new Dictionary<string, Type>(0);
            return dictionaryCategory;
        }
    }
}