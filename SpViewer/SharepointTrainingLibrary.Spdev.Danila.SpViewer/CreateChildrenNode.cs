//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    ///     Класс для вызова создания потомков узлам.
    /// </summary>
    public static class CreateChildrenNode
    {
        /// <summary>
        ///     Функция предназначена для создания нового узла с его потомками по типу родителя, который реализует интерфейс
        ///     IModelSharePoint.
        /// </summary>
        /// <param name="modelSharePoint"></param>
        /// <param name="mainDictionary"></param>
        /// <returns>TreeNode родителя с потомками</returns>
        public static TreeNode CreateNode(IModelSharePoint modelSharePoint,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (modelSharePoint == null)
            {
                throw new ArgumentNullException(nameof(modelSharePoint));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            string typeModel = modelSharePoint.SpViewerType.ToString();
            switch (typeModel)
            {
                case SpEntityTypeName.TYPE_WEB_APPLICATION:
                {
                    var presenter = new SpViewerWebApplicationPresenter();
                    var modelSharePointPresenter = (SpViewerWebApplicationModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_WEB_SITE:
                {
                    var presenter = new SpViewerSitePresenter();
                    var modelSharePointPresenter = (SpViewerSiteModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_WEB:
                {
                    var presenter = new SpViewerWebPresenter();
                    var modelSharePointPresenter = (SpViewerWebModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_LIST:
                case SpEntityTypeName.TYPE_DOCUMENT_LIBRARY:
                case SpEntityTypeName.TYPE_PICTURE_LIBRARY:
                {
                    var presenter = new SpViewerListPresenter();
                    var modelSharePointPresenter = (SpViewerListModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_CONTENT_TYPE:
                {
                    var presenter = new SpViewerContentTypePresenter();
                    var modelSharePointPresenter = (SpViewerContentTypeModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_FIELD:
                {
                    var presenter = new SpViewerFieldPresenter();
                    var modelSharePointPresenter = (SpViewerFieldModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_VIEW:
                {
                    var presenter = new SpViewerViewPresenter();
                    var modelSharePointPresenter = (SpViewerViewModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_SERVICE:
                {
                    var presenter = new SpViewerServicePresenter();
                    var modelSharePointPresenter = (SpViewerServiceModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_SERVER:
                {
                    var presenter = new SpViewerServerPresenter();
                    var modelSharePointPresenter = (SpViewerServerModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_CONTENT_DATABASE:
                {
                    var presenter = new SpViewerContentDatabasePresenter();
                    var modelSharePointPresenter = (SpViewerContentDatabaseModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_FEATURE:
                {
                    var presenter = new SpViewerFeaturePresenter();
                    var modelSharePointPresenter = (SpViewerFeatureModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_ROLE_ASSIGNMENT:
                {
                    var presenter = new SpViewerRolePresenter();
                    var modelSharePointPresenter = (SpViewerRoleModel) modelSharePoint;
                    return presenter.GetNodeWithChildren(modelSharePointPresenter, ref mainDictionary);
                }
                case SpEntityTypeName.TYPE_PRINCIPAL:
                case SpEntityTypeName.TYPE_USER:
                case SpEntityTypeName.TYPE_GROUP:
                {
                    return
                        null; //Надо просто обработать, все сделано в презентере SpWebPresenter (Специфичная сущность)
                }
            }

            var error = new TreeNode("Error SharePointEntity");
            return error;
        }
    }
}