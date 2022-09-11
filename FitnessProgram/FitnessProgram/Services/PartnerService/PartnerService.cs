namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Areas.Admin.Models.Partners;
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Partners;
    using Microsoft.Extensions.Caching.Memory;
    using static SharedMethods;

    public class PartnerService : IPartnerService
    {
        private readonly FitnessProgramDbContext context;
        private readonly IMemoryCache cache;

        public PartnerService(FitnessProgramDbContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public AllPartnersQueryModel GetAll(int currPage, int postPerPage, bool isAdministrator)
        {
            const string partnersCache = "PartnersCache";

            int totalPosts;

            List<Partner> partners;

            List<PartnersViewModel> currPagePartners;

            if (isAdministrator)
            {
                totalPosts = context.Partners.Count();

                currPagePartners = context.Partners
                .Skip((currPage - 1) * postPerPage)
                .Take(postPerPage)
                .Select(x => new PartnersViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image == null ? "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png" : x.Image,
                    Url = x.Url,
                    PromoCode = x.PromoCode,
                    Description = x.Description,

                })
                .ToList();
            }
            else
            {
                partners = cache.Get<List<Partner>>(partnersCache);
                if (partners == null)
                {
                    partners = context.Partners
                    .ToList();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    cache.Set(partnersCache, partners, cacheOptions);
                }

                totalPosts = partners.Count();

                currPagePartners = partners
                .Skip((currPage - 1) * postPerPage)
                .Take(postPerPage)
                .Select(x => new PartnersViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image == null ? "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png" : x.Image,
                    Url = x.Url,
                    PromoCode = x.PromoCode,
                    Description = x.Description,

                })
                .ToList();

            }
            var maxPage = CalcMaxPage(totalPosts, postPerPage);

            currPage = GetCurrPage(currPage, maxPage);

            var result = new AllPartnersQueryModel
            {
                Partners = currPagePartners,
                CurrentPage = currPage,
                MaxPage = maxPage
            };

            return result;
        }

        public void AddPartner(PartnerFormModel model)
        {
            var partner = new Partner
            {
                Name = model.Name,
                Description = model.Description,
                Image = model.Image,
                Url = model.Url,
                PromoCode = model.PromoCode
            };

            context.Partners.Add(partner);
            context.SaveChanges();
        }

        public PartnerFormModel CreateEditModel(int partnerId)
        {
            var partner = GetPartnerById(partnerId);

            var model = new PartnerFormModel
            {
                Name = partner.Name,
                Description = partner.Description,
                Image = partner.Image,
                Url = partner.Url,
                PromoCode = partner.PromoCode
            };

            return model;
        }

        public void EditPartner(int partnerId, PartnerFormModel model)
        {
            var partner = GetPartnerById(partnerId);

            partner.Name = model.Name;
            partner.Description = model.Description;
            partner.Image = model.Image;
            partner.Url = model.Url;
            partner.PromoCode = model.PromoCode;

            context.SaveChanges();
        }
        public void DeletePartner(Partner partner)
        {
            context.Partners.Remove(partner);
            context.SaveChanges();
        }

        public Partner GetPartnerById(int id)
            => context.Partners.FirstOrDefault(x => x.Id == id);
    }
}
