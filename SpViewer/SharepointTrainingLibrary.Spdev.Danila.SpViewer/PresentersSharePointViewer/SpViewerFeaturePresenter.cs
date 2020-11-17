//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Feature сущности.
    /// </summary>
    public class SpViewerFeaturePresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerFeatureModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerFeatureModel spViewerFeatureModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerFeatureModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerFeatureModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerFeature = spViewerFeatureModel.SharePointEntity as SPFeature;
            var treeNodeFeature = new TreeNode(spViewerFeatureModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerFeatureModel);
            treeNodeFeature.Tag = spViewerFeatureModel.GetType() + Separator + viewerFeature.DefinitionId + Separator +
                                  this.g;
            treeNodeFeature = GetCategoryNode(treeNodeFeature, spViewerFeatureModel);
            return treeNodeFeature;
        }
    }
}