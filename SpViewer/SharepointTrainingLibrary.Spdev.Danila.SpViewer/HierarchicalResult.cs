// Copyright © iSys.Spdev 2018 All rights reserved.

namespace iSys.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    using Microsoft.SharePoint.Administration;

    public class HierarchicalResult
    {
        private const char Separator = '|';

        /// <summary>
        ///     Возвращает список значений параметров узла, который был передан в качестве параметра
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="objectDictionary"></param>
        /// <returns></returns>
        public static List<Tuple<string, string, string>> GetParametresFromObject(TreeNode treeNode,
            Dictionary<Guid, IModelSharePoint> objectDictionary)
        {
            if (treeNode == null)
            {
                throw new ArgumentNullException(nameof(treeNode));
            }

            if (objectDictionary == null)
            {
                throw new ArgumentNullException(nameof(objectDictionary));
            }

            var dict = new List<Tuple<string, string, string>>();
            string type = treeNode.Tag.ToString().Split(Separator)[0];
            Assembly assem = typeof(Program).Assembly;
            Type typeObject = assem.GetType(type);
            try
            {
                var temp = (IModelSharePoint) GetObjectFromDictionary(treeNode, objectDictionary);
                dict = DictionaryFromType(temp.SharePointEntity, typeObject);
                dict.Sort();
            }
            catch (InvalidCastException)
            {
                object temp = GetObjectFromDictionary(treeNode, objectDictionary);
                dict = DictionaryFromType(temp, typeObject);
                dict.Sort();
            }

            return dict;
        }

        /// <summary>
        ///     Возвращает значения параметров объекта SharePoint (имя параметра, значение, тип)
        /// </summary>
        /// <param name="atype"></param>
        /// <param name="typeObject"></param>
        /// <returns>
        ///     <typeparam name="Tuple"> Список в виде (имя параметра, значение, тип) </typeparam>
        /// </returns>
        public static List<Tuple<string, string, string>> DictionaryFromType(object atype, Type typeObject)
        {
            if (atype == null)
            {
                return new List<Tuple<string, string, string>>();
            }

            if (typeObject == null)
            {
                typeObject = atype.GetType();
            }

            PropertyInfo[] props = typeObject.GetProperties();
            var dict = new List<Tuple<string, string, string>>();
            foreach (PropertyInfo prp in props)
            {
                try
                {
                    object value = prp.GetValue(atype, new object[] { });

                    if (value != null)
                    {
                        var tup = new Tuple<string, string, string>(prp.Name, value.ToString(),
                            prp.PropertyType.ToString());
                        dict.Add(tup);
                    }
                    else
                    {
                        var tup = new Tuple<string, string, string>(prp.Name, "", prp.PropertyType.ToString());
                        dict.Add(tup);
                    }
                }
                catch (Exception)
                {
                    var tup = new Tuple<string, string, string>(prp.Name, "Нет данных", prp.GetType().ToString());
                    dict.Add(tup);
                }
            }

            return dict;
        }

        /// <summary>
        ///     Возвращает объект из списка объектов по идентификатору вершины
        /// </summary>
        public static object GetObjectFromDictionary(TreeNode node, Dictionary<Guid, IModelSharePoint> objectDictionary)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (objectDictionary == null)
            {
                throw new ArgumentNullException(nameof(objectDictionary));
            }

            string guidObj = node.Tag.ToString().Split('|')[2];
            var g = new Guid(guidObj);
            IModelSharePoint objIModelSharePoint = objectDictionary[g];
            return objIModelSharePoint;
        }

        /// <summary>
        ///     Создает узел категорий для этой сущности
        /// </summary>
        public static TreeNode GetCategoryNode(TreeNode treeNode, IModelSharePoint obj)
        {
            if (treeNode == null)
            {
                throw new ArgumentNullException(nameof(treeNode));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            foreach (KeyValuePair<string, Type> category in obj.CategoryDictionary)
            {
                var categoryNode = new TreeNode(category.Key);
                categoryNode.BackColor = Color.DarkSeaGreen;
                treeNode.Nodes.Add(categoryNode);
            }

            return treeNode;
        }

        /// <summary>
        ///     Заменяет один узел на другой.
        ///     Примечение: в дальнейшем можно добавить дополнительную логику, поэтому создан как отдельный метод.
        /// </summary>
        /// <param>Узел.</param>
        /// <param name="mainNode"></param>
        /// <param name="newNode"></param>
        public static void ReplaceNode(TreeNode mainNode, TreeNode newNode)
        {
            if (mainNode == null)
            {
                throw new ArgumentNullException(nameof(mainNode));
            }

            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }

            mainNode.Parent.Nodes.Add(newNode);
            mainNode.Remove();
        }
    }
}