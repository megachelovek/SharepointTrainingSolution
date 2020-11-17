// Copyright © iSys.Spdev 2018 All rights reserved.

namespace iSys.Spdev.Danila.CamlBuilder.Test
{
    using System;
    using System.Xml.Linq;

    using CamlTypes;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CamlMainTest
    {
        [TestMethod]
        public void IntegerIsNotNullCamlTest()
        {
            XElement elemX = new IntegerTypeCaml("PhoneNumber").IsNotNull();
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void IntegerEqualsCamlTest()
        {
            XElement elemX = new IntegerTypeCaml("PhoneNumber").Equals(42342343);
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void ChoiceBeginsWithCamlTest()
        {
            XElement elemX = new ChoiceTypeCaml("Surname").BeginsWith("Kirillov");
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void UserEqualsCamlTest()
        {
            XElement elemX = new CamlQuery().Where(
                new CamlQuery().And(new UserTypeCaml("Name").Equals("Alex"),
                    new UserTypeCaml("Surname").Includes("a")
                )
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void CounterGreatEqualsThenCamlTest()
        {
            XElement elemX = new CamlQuery().Where(
                new CamlQuery().And(
                    new CounterTypeCaml(new Guid("fa564e0f-0c70-4ab9-b863-0177e6ddd247")).GreatEqualsThen(432),
                    new UserTypeCaml("Surname").Includes("o")
                )
            );
            Console.WriteLine(elemX.ToString());
        }

        //Тесты с примеров гита
        [TestMethod]
        public void ChoiceEqualsCamlTest() //Поле Status (тип Choice) равно "Согласование".
        {
            XElement elemX = new CamlQuery().Where(
                new ChoiceTypeCaml("Status").Equals("Согласование")
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void
            IntegerLittleEqualsThenCamlTest()
        {
            //Поле PageCount (тип Integer) больше 10 И  Поле Created(тип DateTime) меньше или равно 2018-12-01 12-30-00
            XElement elemX = new CamlQuery().Where(
                new IntegerTypeCaml("PageCount").GreatThen(10),
                new DateTimeTypeCaml("Created").LittleEqualsThen(new DateTime(2018, 12, 01, 12, 30, 00))
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void
            UserIncludesCamlTest()
        {
            //Поле AllowedUsers (тип User) включает "Иванов Иван", "Петров Петр"
            //ИЛИ Поле OperatingSystems(тип Lookup) не равно 1999 (тип ID)
            //И Поле Notes(тип Note) содержит "DocTrix".
            XElement elemX = new CamlQuery().Where(
                new CamlQuery().Or(
                    new UserTypeCaml("AllowedUsers").Includes("Иванов Иван"),
                    new LookupTypeCaml("OperatingSystems").NotEquals(1999)
                ),
                new NoteTypeCaml("Notes").Contains("DocTrix")
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void
            IdBeginsWithCamlTest()
        {
            //Поле Title (тип Text) c ID "fa564e0f-0c70-4ab9-b863-0177e6ddd247" начинается с "Договор".
            XElement elemX = new CamlQuery().Where(
                new TextTypeCaml(new Guid("fa564e0f-0c70-4ab9-b863-0177e6ddd247")).BeginsWith("Договор")
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void
            OrderCamlTest()
        {
            //Сортировка: поле Customer по возрастанию, поле Years по убыванию, поле Location по возрастанию.
            XElement elemX = new CamlQuery().OrderBy(
                new CamlQuery.OrderByItem("Customers"),
                new CamlQuery.OrderByItem("Years", false),
                new CamlQuery.OrderByItem("Location")
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void ViewFieldsCamlTest()
        {
            XElement elemX = new CamlQuery().ViewFields("Customers", "Years", "Location"
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void ViewUserInFieldsCamlTest()
        {
            XElement elemX = new CamlQuery().Where(
                new UserTypeCaml("AssignedTo").In(123, 456)
            );
            Console.WriteLine(elemX.ToString());
        }

        [TestMethod]
        public void WhereCamlTest()
        {
            XElement elemX = new CamlQuery().Where(
                new UserTypeCaml("AssignedTo").In(123, 456),
                new ChoiceTypeCaml("Status").In("Project", "Signed"),
                new DateTimeTypeCaml("1Created1").GreatEqualsThen(new DateTime(2018, 12, 12, 13, 30, 0)),
                new DateTimeTypeCaml("2Created2").GreatEqualsThen(DateTime.Now),
                new CounterTypeCaml(new Guid("6aa50293-23f7-46f7-873a-432f0c7e359c")).GreatEqualsThen(100),
                new CamlQuery().Or(
                    new TextTypeCaml("Title").Contains("Agreement"),
                    new LookupTypeCaml("Department").NotIncludes("1"),
                    new LookupTypeCaml("District").NotIncludes("LA")
                ),
                new TextTypeCaml("NoteTitle").Equals("MyNote"),
                new BooleanTypeCaml("IsConfidential").NotEquals(true),
                new NoteTypeCaml("Remarks").IsNull()
            );
            Console.WriteLine(elemX.ToString());
        }
    }
}