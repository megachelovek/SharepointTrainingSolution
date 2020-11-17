//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint.Administration;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Service сущности.
    /// </summary>
    public class SpViewerServicePresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerServerModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerServiceModel spViewerServiceModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerServiceModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerServiceModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerService = spViewerServiceModel.SharePointEntity as SPFarm;
            var treeNodeService = new TreeNode(spViewerServiceModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerServiceModel);
            treeNodeService.Tag = spViewerServiceModel.GetType() + Separator + viewerService.Id + Separator + this.g;
            treeNodeService = GetCategoryNode(treeNodeService, spViewerServiceModel);
            return treeNodeService;
        }
    }
}