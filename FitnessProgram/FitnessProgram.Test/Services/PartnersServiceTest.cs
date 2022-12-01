namespace FitnessProgram.Test.Services
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.Services.PartnerService;
    using FitnessProgram.Test.Mocks;
    using FitnessProgram.ViewModels.Partner;

    public class PartnersServiceTest
    {
        [Fact]
        public void GetPartnerByIdShoudReturnCorrectResultsWhenIdExists()
        {
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();

            var result = partnerService.GetPartnerById(1);

            Assert.NotNull(result);
            Assert.IsType<Partner>(result);
            Assert.Equal("Test Name", result.Name);
        }

        [Fact]
        public void GetPartnerByIdShoudReturnNullWhenIdNotExists()
        {
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();

            var result = partnerService.GetPartnerById(11);

            Assert.Null(result);
        }

        [Fact]
        public void AddPartnerShoudAddCorrectPartnerToDatabase()
        {
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            var partner =
                new PartnerFormModel { Name = "Above", Description = "Above the rest", PromoCode = "promo22", Url = "" };

            partnerService.AddPartner(partner);

            Assert.Equal(expected, data.Partners.Count());
        }

        [Fact]
        public void CreateEditModelShoudReturnCorrectFilledFieldsForGivenIdIfExists()
        {
            var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            var partner =
                new PartnerFormModel { Name = "Above", Description = "Above the rest", PromoCode = "promo22", Url = "" };
            partnerService.AddPartner(partner);

            var model = partnerService.CreateEditModel(1);

            Assert.NotNull(model);
            Assert.IsType<PartnerFormModel>(model);
            Assert.Equal("Above", model.Name);
            Assert.Equal("Above the rest", model.Description);
            Assert.Equal("promo22", model.PromoCode);
        }

        [Fact]
        public void CreateEditModelShoudReturnNullWhenGivenIdNotExists()
        {
            var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());

            var model = partnerService.CreateEditModel(1);

            Assert.Null(model);
        }

        [Fact]
        public void EditShoudMakeChangesCorrectPostWithCorrectData()
        {
            var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            var partner =
            new PartnerFormModel { Name = "Above", Description = "Above the rest", PromoCode = "promo22", Url = "" };
            partnerService.AddPartner(partner);
            var newPartner =
                new PartnerFormModel { Name = "Above the rest", Description = "Above the rest", PromoCode = "promo22", Url = "" };

            partnerService.EditPartner(1, newPartner);

            Assert.NotEqual(partner.Name, partnerService.GetPartnerById(1).Name);
            Assert.Equal(newPartner.Name, partnerService.GetPartnerById(1).Name);
        }

        [Fact]
        public void DeleteShoudRemoveCorrectPostIfPostExists()
        {
            int expected = 10;
            var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();
            var partner =
            new PartnerFormModel { Name = "Above", Description = "Above the rest", PromoCode = "promo22", Url = "" };
            partnerService.AddPartner(partner);

            partnerService.DeletePartner(partnerService.GetPartnerById(11));

            Assert.Equal(expected, data.Partners.Count());
            Assert.Null(partnerService.GetPartnerById(11));
        }

        [Fact]
        public void DeleteShoudNotRemoveCorrectPostIfPostNotExists()
        {
            int expected = 11;
            var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();
            var partner =
            new PartnerFormModel { Name = "Above", Description = "Above the rest", PromoCode = "promo22", Url = "" };
            partnerService.AddPartner(partner);

            partnerService.DeletePartner(partnerService.GetPartnerById(12));

            Assert.Equal(expected, data.Partners.Count());
            Assert.NotNull(partnerService.GetPartnerById(11));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(0, 2)]
        [InlineData(22, 2)]
        public void GetAllShoudReturnCorrectMaxPage(int currPage, int expected)
        {
            const int postPerPage = 6;
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();

            var model =
                partnerService.GetAll(currPage, postPerPage, new AllPartnersQueryModel { CurrentPage = currPage }, false);

            Assert.NotNull(model);
            Assert.IsType<AllPartnersQueryModel>(model);
            Assert.Equal(expected, model.MaxPage);
        }

        [Fact]
        public void GetAllShoudReturnCorrectMaxPageWhenThereIsNoPosts()
        {
            const int currPage = 1;
            const int postPerPage = 6;
            int expected = 1;
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());

            var model =
               partnerService.GetAll(currPage, postPerPage, new AllPartnersQueryModel { CurrentPage = currPage }, false);

            Assert.NotNull(model);
            Assert.IsType<AllPartnersQueryModel>(model);
            Assert.Equal(expected, model.MaxPage);
        }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 4)]
        [InlineData(3, 4)]
        [InlineData(0, 6)]
        public void GetAllShoudShowCorrectNumberOfPostsForGivenPage(int currPage, int expected)
        {
            const int postPerPage = 6;
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();

            var model =
                partnerService.GetAll(currPage, postPerPage, new AllPartnersQueryModel { CurrentPage = currPage }, false);

            Assert.NotNull(model);
            Assert.Equal(expected, model.Partners.Count());
        }

        [Fact]
        public void TheCacheIsNotAvailableForUserWithAdminRole()
        {
            const int postPerPage = 6;
            int maxPageExpected = 4;
            int pageTwoExpected = 6;
            int expectedCacheCount = 0;
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var partnerService = new PartnerService(data, cache);
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();

            var model =
                partnerService.GetAll(2, postPerPage, new AllPartnersQueryModel { CurrentPage = 2 }, true);
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();
            model = partnerService.GetAll(2, postPerPage, new AllPartnersQueryModel { CurrentPage = 2 }, true);

            Assert.NotNull(model);
            Assert.IsType<AllPartnersQueryModel>(model);
            Assert.Equal(maxPageExpected, model.MaxPage);
            Assert.Equal(pageTwoExpected, model.Partners.Count());
            Assert.Equal(expectedCacheCount, cache.Count);
        }
        [Fact]
        public void TheCacheIsAvailableForNoAdminUsers()
        {
            const int postPerPage = 6;
            int maxPageExpected = 2;
            int pageTwoExpected = 4;
            int expectedCacheCount = 1;
            using var data = DatabaseMock.Instance;
            var cache = GetMemoryCache();
            var partnerService = new PartnerService(data, cache);
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();

            var model =
                partnerService.GetAll(2, postPerPage, new AllPartnersQueryModel { CurrentPage = 2 }, false);
            data.Partners.AddRange(GetPartners());
            data.SaveChanges();
            model =
                partnerService.GetAll(2, postPerPage, new AllPartnersQueryModel { CurrentPage = 2 }, false);

            Assert.NotNull(model);
            Assert.IsType<AllPartnersQueryModel>(model);
            Assert.Equal(maxPageExpected, model.MaxPage);
            Assert.Equal(pageTwoExpected, model.Partners.Count());
            Assert.Equal(expectedCacheCount, cache.Count);
        }

        [Theory]
        [InlineData(1, "Test", 6)]
        [InlineData(1, "test", 6)]
        [InlineData(1, "TEST", 6)]
        [InlineData(1, "Above", 1)]
        [InlineData(3, "Above", 1)]
        public void GetAllShoudReturnCorrectPostsWithGivenSerchTerm(int currPage, string searchTerm, int expectedCount)
        {
            const int postPerPage = 6;
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.Partners.Add(new Partner
            {
                Name = "Above",
                Description = "Above the rest",
                PromoCode = "22",
                Url = "",
                Photo = new PartnerPhoto
                {
                    Bytes = new byte[1024],
                    Description = "",
                    FileExtension = ""
                }
            });
            data.SaveChanges();

            var model =
                partnerService.GetAll(currPage, postPerPage, new AllPartnersQueryModel { CurrentPage = currPage, SearchTerm = searchTerm }, false);

            Assert.IsType<AllPartnersQueryModel>(model);
            Assert.Equal(expectedCount, model.Partners.Count());
            Assert.True(model.Partners.FirstOrDefault().Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                || model.Partners.FirstOrDefault().Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        [Theory]
        [InlineData(1, Sorting.Default, "Above")]
        [InlineData(1, Sorting.DateAscending, "Test Name")]
        public void SortingShoudShowCorrectlySortedPosts(int currPage, Sorting sorting, string expectedNameFirstElement)
        {
            const int postPerPage = 6;
            using var data = DatabaseMock.Instance;
            var partnerService = new PartnerService(data, GetMemoryCache());
            data.Partners.AddRange(GetPartners());
            data.Partners.Add(new Partner
            {
                Name = "Above",
                Description = "Above the rest",
                PromoCode = "22",
                Url = "",
                CreatedOn = DateTime.Now,
                Photo = new PartnerPhoto
                {
                    Bytes = new byte[1024],
                    Description = "",
                    FileExtension = ""
                }
            });
            data.SaveChanges();

            var model =
                partnerService.GetAll(currPage, postPerPage, new AllPartnersQueryModel { CurrentPage = currPage, Sorting = sorting }, false);

            Assert.NotNull(model);
            Assert.IsType<AllPartnersQueryModel>(model);
            Assert.Equal(expectedNameFirstElement, model.Partners.First().Name);
        }
    }
}
