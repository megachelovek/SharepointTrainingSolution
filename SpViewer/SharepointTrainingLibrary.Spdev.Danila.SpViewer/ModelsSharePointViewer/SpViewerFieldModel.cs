//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    public class SpViewerFieldModel : IModelSharePoint
    {
        public SpViewerFieldModel(SPField spField)
        {
            this.Name = spField?.Title ?? "";
            this.SpViewerType = spField.GetType();
            this.SharePointEntity = spField;
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