// Copyright © iSys.Spdev 2019 All rights reserved.

namespace iSys.Spdev.Danila.Csom.Tests
{
    using System;
    using System.Collections.Generic;

    using Meta;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MetaInfoTests
    {
        [TestMethod]
        public void CsomSiteInfoTest()
        {
            var meta = new GetMetaInfo("http://malchikov-vm1/");
            WebMetaInfo result = meta.GetParametresWeb();
            Console.WriteLine(result.Id);
            Console.WriteLine(result.Title);
            Console.WriteLine(result.Description);
            Console.WriteLine(result.ServerRelativeUrl);
            Console.WriteLine("------");
        }

        [TestMethod]
        public void CsomSiteFieldsTest()
        {
            var meta = new GetMetaInfo("http://malchikov-vm1/");
            List<WebFieldMetaInfo> result = meta.GetFieldWeb();
            foreach (WebFieldMetaInfo webFieldMetaInfo in result)
            {
                Console.WriteLine(webFieldMetaInfo.Id);
                Console.WriteLine(this.ChangeNameIfExistInCollection(webFieldMetaInfo.Title,result));
                Console.WriteLine(webFieldMetaInfo.InternalName);
                Console.WriteLine(webFieldMetaInfo.Description);
                Console.WriteLine(webFieldMetaInfo.TypeAsString);
                Console.WriteLine("------");
            }
        }

        [TestMethod]
        public void CsomSiteContentTypesTest()
        {
            var meta = new GetMetaInfo("http://malchikov-vm1/");
            List<WebContentTypeMetaInfo> result = meta.GetContentTypeSite();
            foreach (WebContentTypeMetaInfo webFieldMetaInfo in result)
            {
                Console.WriteLine(webFieldMetaInfo.Id.StringValue);
                Console.WriteLine(webFieldMetaInfo.Name);
                Console.WriteLine(webFieldMetaInfo.Description);
                Console.WriteLine("------");
            }
        }

        [TestMethod]
        public void CsomListTest()
        {
            var meta = new GetMetaInfo("http://malchikov-vm1/");
            List<ListMetaInfo> result = meta.GetListSite();
            foreach (ListMetaInfo ListMetaInfo in result)
            {
                Console.WriteLine(ListMetaInfo.Id);
                Console.WriteLine(ListMetaInfo.Title);
                Console.WriteLine(ListMetaInfo.BaseTemplate);
                Console.WriteLine(ListMetaInfo.Description);
                Console.WriteLine(ListMetaInfo.ParentWebUrl);
                Console.WriteLine("------");
            }
        }

        [TestMethod]
        public void CsomFieldsListTest()
        {
            var meta = new GetMetaInfo("http://malchikov-vm1/");
            List<ListFieldMetaInfo> result = meta.GetFieldsListsSite();

            foreach (ListFieldMetaInfo listFieldMetaInfo in result)
            {
                Console.WriteLine(listFieldMetaInfo.Id);
                Console.WriteLine(listFieldMetaInfo.Title);
                Console.WriteLine(listFieldMetaInfo.InternalName);
                Console.WriteLine(listFieldMetaInfo.Description);
                Console.WriteLine(listFieldMetaInfo.TypeAsString);
                Console.WriteLine("------");
            }
        }

        [TestMethod]
        public void CsomContentTypesListTest()
        {
            var meta = new GetMetaInfo("http://malchikov-vm1/");
            List<ListContentTypeMetaInfo> result = meta.GetListsContentTypesSite();
            foreach (ListContentTypeMetaInfo listsContentTypesMetaInfo in result)
            {
                Console.WriteLine(listsContentTypesMetaInfo.Id.StringValue);
                Console.WriteLine(listsContentTypesMetaInfo.Name);
                Console.WriteLine(listsContentTypesMetaInfo.Description);
                Console.WriteLine("------");
            }
        }

        [TestMethod]
        public void Test()
        {
            var tt = "qqq%,....%%wwww\" zzzz";
                Console.WriteLine(this.ReplaceWrongSymbols(tt));
        
        }

        private string ReplaceWrongSymbols(string wrongString)
        {
            var newString="";
            var wrongSymbols = new string[] { "(", ")", " ", "#", "№", ".", "?", "!", ",", "\"", "%" };
            foreach (string stringSymbol in wrongSymbols)
            {
                char symbol = Convert.ToChar(stringSymbol);
                wrongString = wrongString.Replace(symbol, '_');
            }
            return wrongString;
        }

        private int _countOfEqualTitle = 0;
        private string ChangeNameIfExistInCollection(string title, List<WebFieldMetaInfo> objCollection)
        {
            if (objCollection == null)
            {
                throw new ArgumentNullException(nameof(objCollection));
            }
            var currentCount = 0;
            foreach (WebFieldMetaInfo objTitle in objCollection)
            {
                if (objTitle.Title == title)
                {
                    currentCount++;
                    if (currentCount > 1)
                    {
                        this._countOfEqualTitle++;
                        return title + this._countOfEqualTitle;
                    }
                }
            }
            return title;
        }
    }
}
