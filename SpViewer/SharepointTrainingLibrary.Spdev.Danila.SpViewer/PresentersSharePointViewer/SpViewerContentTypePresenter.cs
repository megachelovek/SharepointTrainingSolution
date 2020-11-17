//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Content Type сущности.
    /// </summary>
    public class SpViewerContentTypePresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerContentTypeModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerContentTypeModel spViewerContentTypeModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerContentTypeModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerContentTypeModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerContentType = spViewerContentTypeModel.SharePointEntity as SPContentType;
            var treeNodeContentType = new TreeNode(spViewerContentTypeModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerContentTypeModel);
            treeNodeContentType.Tag = spViewerContentTypeModel.GetType() + Separator + viewerContentType.Id +
                                      Separator + this.g;
            treeNodeContentType = GetCategoryNode(treeNodeContentType, spViewerContentTypeModel);
            return treeNodeContentType;
        }
    }
}