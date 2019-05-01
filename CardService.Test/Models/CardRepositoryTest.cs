using System;
using System.Collections.Generic;
using CardService.Models;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CardService.Test {
    public class ContextRepositoryTest {

        private IConfiguration getConfiguration() {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        [Fact]
        public void ConstructorTest() {
            DBContext.GetInstance(getConfiguration());
        }

        [Fact]
        public void InsertOneTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var card = new CardCollection();
            card.Id = "5cc2091f2568e23d0ce2947a";
            card.Names.Ita = "nome italiano";
            card.Names.Eng = "english name";
            var cardRepository = new Repository<CardCollection>();

            var insertTask = cardRepository.InsertOne(card);
            var cardResult = insertTask.Result;
        }

        [Fact]
        public void GetAllTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<CardCollection>();

            var insertTask = cardRepository.GetAll();
            var cardsResult = insertTask.Result;
        }

        [Fact]
        public void FindTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<CardCollection>();

            var insertTask = cardRepository.Find(x => x.Names.Ita == "nome italiano");
            var cardsResult = insertTask.Result;
        }

        [Fact]
        public void GetPagingTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<CardCollection>();

            var insertTask = cardRepository.GetPage(2);
            var cardsResult = insertTask.Result;
        }

        [Fact]
        public void UpdateOneTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<CardCollection>();

            var card = new CardCollection();
            card.Names.Ita = "nome italiano edit";
            card.Names.Eng = "english name edit";

            var task = cardRepository.UpdateOne("5cc2091f2568e23d0ce2947a", card);
            task.Wait();
        }

        [Fact]
        public void DeleteOneTest(){
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<CardCollection>();

            var card = new CardCollection();
            card.Id = "5cc2091f2568e23d0ce2947a";

            var task = cardRepository.DeleteOne(card);
            task.Wait();
        }

    }
}