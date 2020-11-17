//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    public class SpViewerListModel : IModelSharePoint
    {
        public SpViewerListModel(SPList spList)
        {
            this.Name = spList?.Title ?? "";
            this.SpViewerType = spList.GetType();
            this.SharePointEntity = spList;
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
            dictionaryCategory.Add("Типы содержимого (Content Types)", typeof(SPContentType));
            dictionaryCategory.Add("Поля (Fields)", typeof(SPField));
            dictionaryCategory.Add("Представления (Views)", typeof(SPView));
            return dictionaryCategory;
        }
    }
}