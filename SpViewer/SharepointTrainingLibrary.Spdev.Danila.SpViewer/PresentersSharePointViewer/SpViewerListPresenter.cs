//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для List сущности.
    /// </summary>
    public class SpViewerListPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerListModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerListModel spViewerListModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerListModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerListModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var spList = spViewerListModel.SharePointEntity as SPList;
            var treeNodeList = new TreeNode(spViewerListModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerListModel);
            treeNodeList.Tag = spViewerListModel.GetType() + Separator + spList.ID + Separator + this.g;
            treeNodeList = GetCategoryNode(treeNodeList, spViewerListModel);
            this.AddNodespContentType(mainDictionary, spList, treeNodeList);

            this.AddNodespFields(mainDictionary, spList, treeNodeList);

            this.AddNodespViews(mainDictionary, spList, treeNodeList);

            return treeNodeList;
        }

        private void AddNodespViews(Dictionary<Guid, IModelSharePoint> mainDictionary, SPList spList,
            TreeNode treeNodeList)
        {
            foreach (SPView spView in spList.Views)
            {
                var viewerView = new SpViewerViewModel(spView);
                var treeNodeView = new TreeNode(viewerView.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerView);
                treeNodeView.Tag = spView.GetType() + "|" + spView.ID + "|" + this.g;
                treeNodeList.Nodes[2].Nodes.Add(treeNodeView);
            }
        }

        private void AddNodespFields(Dictionary<Guid, IModelSharePoint> mainDictionary, SPList spList,
            TreeNode treeNodeList)
        {
            foreach (SPField spField in spList.Fields)
            {
                var viewerField = new SpViewerFieldModel(spField);
                var treeNodeField = new TreeNode(viewerField.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerField);
                treeNodeField.Tag = spField.GetType() + "|" + spField.Id + "|" + this.g;
                treeNodeList.Nodes[1].Nodes.Add(treeNodeField);
            }
        }

        private void AddNodespContentType(Dictionary<Guid, IModelSharePoint> mainDictionary, SPList spList,
            TreeNode treeNodeList)
        {
            foreach (SPContentType spContentType in spList.ContentTypes)
            {
                var viewerContentType = new SpViewerContentTypeModel(spContentType);
                var treeContentType = new TreeNode(spContentType.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerContentType);
                treeContentType.Tag = spContentType.GetType() + "|" + spContentType.Id + "|" + this.g;
                treeNodeList.Nodes[0].Nodes.Add(treeContentType);
            }
        }
    }
}