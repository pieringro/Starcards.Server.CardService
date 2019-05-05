using System;
using System.Collections.Generic;
using CardService.Models;
using Microsoft.Extensions.Configuration;
using Xunit;
using SimpleMongoDBWrapper;

namespace CardService.Test {
    public class CardRepositoryTest {

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
        public void InsertOneCardTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var card = new Card();
            card.Id = "5cc2091f2568e23d0ce2947a";
            card.Name.Ita = "nome italiano";
            card.Name.Eng = "english name";
            var cardRepository = new Repository<Card>();

            var insertTask = cardRepository.InsertOne(card);
            var cardResult = insertTask.Result;
        }

        [Fact]
        public void InsertOneMonsterCardTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var card = new MonsterCard();
            card.Id = "5cc2091f2568e23d0ce2947a";
            card.Name.Ita = "nome italiano";
            card.Name.Eng = "english name";
            var cardRepository = new Repository<MonsterCard>();

            var insertTask = cardRepository.InsertOne(card);
            var cardResult = insertTask.Result;
        }

        [Fact]
        public void GetAllTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<Card>();

            var insertTask = cardRepository.GetAll();
            var cardsResult = insertTask.Result;
        }

        [Fact]
        public void FindTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<Card>();

            var insertTask = cardRepository.Find(x => x.Name.Ita == "nome italiano");
            var cardsResult = insertTask.Result;
        }

        [Fact]
        public void GetPagingTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<Card>();

            var insertTask = cardRepository.GetPage(2);
            var cardsResult = insertTask.Result;
        }

        [Fact]
        public void UpdateOneTest() {
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<Card>();

            var card = new Card();
            card.Name.Ita = "nome italiano edit";
            card.Name.Eng = "english name edit";

            var task = cardRepository.UpdateOne("5cc2091f2568e23d0ce2947a", card);
            task.Wait();
        }

        [Fact]
        public void DeleteOneTest(){
            var context = DBContext.GetInstance(getConfiguration());
            var cardRepository = new Repository<Card>();

            var card = new Card();
            card.Id = "5cc2091f2568e23d0ce2947a";

            var task = cardRepository.DeleteOne(card);
            task.Wait();
        }

    }
}