//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    public class SpViewerContentTypeModel : IModelSharePoint
    {
        public SpViewerContentTypeModel(SPContentType spContentType)
        {
            this.Name = spContentType?.Name ?? "";
            this.SpViewerType = spContentType.GetType();
            this.SharePointEntity = spContentType;
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