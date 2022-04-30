using Codere.MX.FeedCorner.UnitTests.Common;
using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Domain.Exceptions;
using GoalSystem.Inventory.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codere.MX.FeedCorner.UnitTests.Infrastructure
{
    [TestClass]
    public class UnitTestRepositoryFeedsFromJson
    {
        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task Load_OK()
        {
            var feeds = Helpers.GetFeeds();

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.GetAll());
            Assert.AreEqual(ex.Result.Message, "The argument '_feeds' must not be null", ex.Result.Message);

            await repositoryFeedsFromJson.Load(feeds);
            var allFeeds = await repositoryFeedsFromJson.GetAll();
            Assert.IsNotNull(allFeeds);
            Console.WriteLine($"The lenght of Feeds is {allFeeds.Count}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetAll_OK()
        {
            var feeds = Helpers.GetFeeds();

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.GetAll());
            Assert.AreEqual(ex.Result.Message, "The argument '_feeds' must not be null", ex.Result.Message);

            await repositoryFeedsFromJson.Load(feeds);
            var allFeeds = await repositoryFeedsFromJson.GetAll();
            Assert.IsNotNull(allFeeds);
            Console.WriteLine($"The lenght of Feeds is {allFeeds.Count}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetAll_KO_with_feeds_null()
        {
            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.GetAll());
            Assert.AreEqual(ex.Result.Message, "The argument '_feeds' must not be null", ex.Result.Message);

            ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.Load(null));
            Assert.AreEqual(ex.Result.Message, "The argument 'feeds' must not be null", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetAll_KO_with_feeds_empty()
        {
            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.GetAll());
            Assert.AreEqual(ex.Result.Message, "The argument '_feeds' must not be null", ex.Result.Message);

            ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.Load(new List<Feed>()));
            Assert.AreEqual(ex.Result.Message, "The argument 'feeds' must not be empty", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetById_OK()
        {
            /*
                Must return ->
                {
                    'code': 'PT78utMi7b',
                    'stadium': 'Sultanes',
                    'isOnUse': false
                }
             */
            var feeds = Helpers.GetFeeds();
            var code = "PT78utMi7b";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var result = await repositoryFeedsFromJson.GetById(code);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Code, code);
            Console.WriteLine($"The Feed found is [{JsonConvert.SerializeObject(result)}]");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetById_OK_with_code_not_found()
        {
            var feeds = Helpers.GetFeeds();
            var id = "fake";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var result = await repositoryFeedsFromJson.GetById(id);
            Assert.IsNull(result);
            Console.WriteLine($"The Feed with id [{id}] not found");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task Update_OK()
        {
            var feeds = Helpers.GetFeeds();
            var id = "PT78utMi7b";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            await repositoryFeedsFromJson.UpdateValid(id, true);
            var findFeed = await repositoryFeedsFromJson.GetById(id);
            Assert.IsNotNull(findFeed);
            Assert.AreEqual(findFeed.IsValid, true);
            Console.WriteLine($"The Feed modified is [{JsonConvert.SerializeObject(findFeed)}]");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task Update_KO_with_code_fake()
        {
            var feeds = Helpers.GetFeeds();
            var id = "fake";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.UpdateValid(id, true));
            Assert.AreEqual(ex.Result.Message, $"Feed [{id}] not found", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task Delete_OK()
        {
            var feeds = Helpers.GetFeeds();
            var id = "PT78utMi7b";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            await repositoryFeedsFromJson.Delete(id);
            var findFeed = await repositoryFeedsFromJson.GetById(id);
            Assert.IsNull(findFeed);
            Console.WriteLine($"The Feed delete is [{id}]");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task Delete_KO_with_code_fake()
        {
            var feeds = Helpers.GetFeeds();
            var id = "fake";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.Delete(id));
            Assert.AreEqual(ex.Result.Message, $"Feed [{id}] not found", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetByStadium_OK()
        {
            var feeds = Helpers.GetFeeds();
            var stadium = "Marichis";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var result = await repositoryFeedsFromJson.GetBySatdium(stadium);
            Assert.IsNotNull(result);
            Console.WriteLine($"In Feeds there are {result.Count} [{stadium}]");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetByStadium_KO_with_stadium_fake()
        {
            var feeds = Helpers.GetFeeds();
            var stadium = "fake";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.GetBySatdium(stadium));
            Assert.AreEqual(ex.Result.Message, $"The value '{stadium}' is not in '_feeds'");
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task AddRange_OK()
        {
            var feeds = Helpers.GetFeeds();
            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var feedsNew = @"
                [
                    {
                        ""code"": ""PT78utMi7b"",
                        ""stadium"": ""Marichis"",
                        ""IsValid"": false
                    },
                    {
                        ""code"": ""G7pvhtms41"",
                        ""stadium"": ""Marichis"",
                        ""IsValid"": false
                    }
                ]";
            var feedsFromJson = JsonConvert.DeserializeObject<List<Feed>>(feedsNew);
            var countFeedsOld = await repositoryFeedsFromJson.Count;
            await repositoryFeedsFromJson.AddRange(feedsFromJson);
            var countFeedsNew = await repositoryFeedsFromJson.Count;

            Assert.AreEqual(countFeedsOld + feedsFromJson.Count, countFeedsNew);
            Console.WriteLine($"Feeds have {countFeedsNew} items");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task RemoveRange_OK()
        {
            var feeds = Helpers.GetFeeds();
            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);
            var stadium = "Marichis";

            var countFeedsOld = await repositoryFeedsFromJson.Count;
            var feedsStadium = await repositoryFeedsFromJson.GetBySatdium(stadium);
            var countFeedsStadium = feedsStadium.Count;
            await repositoryFeedsFromJson.RemoveRange(stadium);
            var countFeedsNew = await repositoryFeedsFromJson.Count;

            Assert.AreEqual(countFeedsOld - countFeedsStadium, countFeedsNew);
            Console.WriteLine($"Feeds have {countFeedsNew} items");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task UpdateFeedsByStadium_OK()
        {
            var feeds = Helpers.GetFeeds();
            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var stadium = "Marichis";

            var countFeedsOld = await repositoryFeedsFromJson.Count;
            Console.WriteLine($"Feeds have {countFeedsOld} items");
            var feedsStadium = await repositoryFeedsFromJson.GetBySatdium(stadium);
            var countFeedsStadium = feedsStadium.Count;
            await repositoryFeedsFromJson.RemoveRange(stadium);
            var countFeedsNew = await repositoryFeedsFromJson.Count;

            Assert.AreEqual(countFeedsOld - countFeedsStadium, countFeedsNew);
            Console.WriteLine($"Feeds have {countFeedsNew} items");

            var feedsNew = @"
                [
                    {
                        ""code"": ""PT78utMi7b"",
                        ""stadium"": ""Marichis"",
                        ""IsValid"": false
                    },
                    {
                        ""code"": ""G7pvhtms41"",
                        ""stadium"": ""Marichis"",
                        ""IsValid"": false
                    }
                ]";
            var feedsFromJson = JsonConvert.DeserializeObject<List<Feed>>(feedsNew);
            countFeedsOld = await repositoryFeedsFromJson.Count;
            await repositoryFeedsFromJson.AddRange(feedsFromJson);
            countFeedsNew = await repositoryFeedsFromJson.Count;

            Assert.AreEqual(countFeedsOld + feedsFromJson.Count, countFeedsNew);
            Console.WriteLine($"Feeds have {countFeedsNew} items");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetAtByStadium_OK()
        {
            /*
                Must return ->
                    {
                        "code": "G7pvhtms41",
                        "stadium": "Sultanes",
                        "IsValid": false
                    }
            */
            var feeds = Helpers.GetFeeds();
            var stadium = "Sultanes";
            var code = "G7pvhtms41";

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var result = await repositoryFeedsFromJson.GetAtBySatdium(stadium, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Code, code);
            Console.WriteLine($"The Feed found is [{JsonConvert.SerializeObject(result)}]");
        }

        [TestMethod, TestCategory("RepositoryFeedsFromJson")]
        public async Task GetAtByStadium_KO_with_position_over_the_count_items()
        {
            /*
                Must return ->
                    {
                        "code": "G7pvhtms41",
                        "stadium": "Sultanes",
                        "IsValid": false
                    }
            */
            var feeds = Helpers.GetFeeds();
            var stadium = "Sultanes";
            var position = 1000;

            var repositoryFeedsFromJson = new RepositoryItemInventoryInMemory();
            await repositoryFeedsFromJson.Load(feeds);

            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryFeedsFromJson.GetAtBySatdium(stadium, position));
            Assert.AreEqual(ex.Result.Message, $"The list has not [{position + 1}] items. The first element start in the position 0");
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }
    }
}
