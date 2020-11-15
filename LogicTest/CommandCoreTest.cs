using CommandCore.Factories;
using DataCore;
using DataCore.Entities;
using System;
using Xunit;

namespace LogicTest
{
    public class CommandCoreTest
    {
        [Fact]
        public void Test1()
        {
            var article = new ArticleDB();
            var factory = new ArticleFactory(article);

            var x = factory.CreateArticles();

            var g = x[0];

            Assert.Equal("Zivot savcu", g.Title);
        }
    }
}
