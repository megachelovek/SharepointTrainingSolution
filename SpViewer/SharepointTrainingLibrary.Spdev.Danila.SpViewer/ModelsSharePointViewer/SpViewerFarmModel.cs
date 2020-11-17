//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.SharePoint.Administration;

    /// <summary>
    ///     Модель фермы SharePoint.
    /// </summary>
    public class SpViewerFarmModel : IModelSharePoint
    {
        public SpViewerFarmModel()
        {
            this.Name = SPFarm.Local?.Name ?? "";
            this.SpViewerType = SPFarm.Local.GetType();
            this.SharePointEntity = SPFarm.Local;
            this.InitCategoryDictionary();
        }

        public string Name { get; }

        public Type SpViewerType { get; }

        public object SharePointEntity { get; }

        public Dictionary<string, Type> CategoryDictionary => this.InitCategoryDictionary();


        /// <summary>
        ///     Список дочерних категорий фермы
        /// </summary>
        public Dictionary<string, Type> InitCategoryDictionary()
        {
            var dictionaryCategory = new Dictionary<string, Type>(3);
            Assembly assem = typeof(Program).Assembly;
            dictionaryCategory.Add("Веб приложения (Web applications)", typeof(SPWebApplication));
            dictionaryCategory.Add("Сервисы (Services)", typeof(SPService));
            dictionaryCategory.Add("Серверы (Servers)", typeof(SPServer));
            return dictionaryCategory;
        }
    }
}