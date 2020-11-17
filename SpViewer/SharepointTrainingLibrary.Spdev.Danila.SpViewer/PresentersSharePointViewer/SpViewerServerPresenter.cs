//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint.Administration;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Server сущности.
    /// </summary>
    public class SpViewerServerPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerServerModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerServerModel spViewerServerModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerServerModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerServerModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerServer = spViewerServerModel.SharePointEntity as SPServer;
            var treeNodeServer = new TreeNode(spViewerServerModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerServerModel);
            treeNodeServer.Tag = spViewerServerModel.GetType() + Separator + viewerServer.Id + Separator + this.g;
            treeNodeServer = GetCategoryNode(treeNodeServer, spViewerServerModel);
            return treeNodeServer;
        }
    }
}