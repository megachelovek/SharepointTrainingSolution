//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Site сущности.
    /// </summary>
    public class SpViewerSitePresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerSiteModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerSiteModel spViewerSiteModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerSiteModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerSiteModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var spSite = spViewerSiteModel.SharePointEntity as SPSite;
            var treeNodeSite = new TreeNode(spSite.Url);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerSiteModel);
            treeNodeSite.Tag = spViewerSiteModel.GetType() + Separator + spSite.ID + Separator + this.g;
            treeNodeSite = GetCategoryNode(treeNodeSite, spViewerSiteModel);
            this.AddNodesAllWebs(mainDictionary, spSite, treeNodeSite);

            this.AddNodespFeature(mainDictionary, spSite, treeNodeSite);

            this.AddNodespContentType(mainDictionary, spSite, treeNodeSite);

            this.AddNodespField(mainDictionary, spSite, treeNodeSite);


            return treeNodeSite;
        }

        private void AddNodespField(Dictionary<Guid, IModelSharePoint> mainDictionary, SPSite spSite,
            TreeNode treeNodeSite)
        {
            foreach (SPField spField in spSite.RootWeb.Fields)
            {
                var viewerField = new SpViewerFieldModel(spField);
                var treeNodeField = new TreeNode(viewerField.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerField);
                treeNodeField.Tag = spField.GetType() + "|" + spField.Id + "|" + this.g;
                treeNodeSite.Nodes[3].Nodes.Add(treeNodeField);
            }
        }

        private void AddNodespContentType(Dictionary<Guid, IModelSharePoint> mainDictionary, SPSite spSite,
            TreeNode treeNodeSite)
        {
            foreach (SPContentType spContentType in spSite.RootWeb.ContentTypes)
            {
                var viewerContentType = new SpViewerContentTypeModel(spContentType);
                var treeContentType = new TreeNode(spContentType.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerContentType);
                treeContentType.Tag = spContentType.GetType() + "|" + spContentType.Id + "|" + this.g;
                treeNodeSite.Nodes[2].Nodes.Add(treeContentType);
            }
        }

        private void AddNodespFeature(Dictionary<Guid, IModelSharePoint> mainDictionary, SPSite spSite,
            TreeNode treeNodeSite)
        {
            foreach (SPFeature spFeature in spSite.Features)
            {
                var viewerFeature = new SpViewerFeatureModel(spFeature);
                var treeNodeFeature = new TreeNode(viewerFeature.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerFeature);
                treeNodeFeature.Tag = spFeature.GetType() + "|" + spFeature.DefinitionId + "|" + this.g;
                treeNodeSite.Nodes[1].Nodes.Add(treeNodeFeature);
            }
        }

        private void AddNodesAllWebs(Dictionary<Guid, IModelSharePoint> mainDictionary, SPSite spSite,
            TreeNode treeNodeSite)
        {
            foreach (SPWeb spWeb in spSite.AllWebs)
            {
                var viewerWeb = new SpViewerWebModel(spWeb);
                var treeWeb = new TreeNode(viewerWeb.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerWeb);
                treeWeb.Tag = spWeb.GetType() + "|" + spWeb.Url + "|" + this.g;
                treeNodeSite.Nodes[0].Nodes.Add(treeWeb);
            }
        }
    }
}