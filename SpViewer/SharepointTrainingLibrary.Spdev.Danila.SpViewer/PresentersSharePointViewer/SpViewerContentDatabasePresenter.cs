//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint.Administration;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для ContentDatabase сущности.
    /// </summary>
    public class SpViewerContentDatabasePresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerContentDatebaseModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerContentDatabaseModel spViewerContentDatebaseModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerContentDatebaseModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerContentDatebaseModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerContentDatebase = spViewerContentDatebaseModel.SharePointEntity as SPContentDatabase;
            var treeNodeContentDatebase = new TreeNode(spViewerContentDatebaseModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerContentDatebaseModel);
            treeNodeContentDatebase.Tag = spViewerContentDatebaseModel.GetType() + Separator +
                                          viewerContentDatebase.Id + Separator + this.g;
            treeNodeContentDatebase = GetCategoryNode(treeNodeContentDatebase, spViewerContentDatebaseModel);
            return treeNodeContentDatebase;
        }
    }
}