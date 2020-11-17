//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для View сущности.
    /// </summary>
    public class SpViewerViewPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerViewModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerViewModel spViewerViewModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerViewModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerViewModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerView = spViewerViewModel.SharePointEntity as SPView;
            var treeNodeView = new TreeNode(spViewerViewModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerViewModel);
            treeNodeView.Tag = spViewerViewModel.GetType() + Separator + viewerView.ID + Separator + this.g;
            treeNodeView = GetCategoryNode(treeNodeView, spViewerViewModel);
            return treeNodeView;
        }
    }
}