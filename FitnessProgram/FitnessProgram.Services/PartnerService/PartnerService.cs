namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.Partner;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
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

        public AllPartnersQueryModel GetAll(int currPage, int postPerPage, AllPartnersQueryModel query,bool isAdministrator)
        {
            const string partnersCache = "PartnersCache";

            int totalPosts;

            List<Partner> partners;

            List<PartnersViewModel> currPagePartners;

            if (isAdministrator)
            {
                partners = GetPartners();
            }
            else
            {
                partners = cache.Get<List<Partner>>(partnersCache);
                if (partners == null)
                {
                    partners = GetPartners();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                    cache.Set(partnersCache, partners, cacheOptions);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                partners = partners
                    .Where(p => p.Name.Contains(query.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                           p.Description.Contains(query.SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            partners = query.Sorting switch
            {
                Sorting.Default => partners.OrderByDescending(x => x.CreatedOn).ToList(),
                Sorting.DateAscending => partners.OrderBy(x => x.CreatedOn).ToList(),
                _ => partners.OrderByDescending(x => x.CreatedOn).ToList()
            };

            totalPosts = partners.Count();

            var maxPage = CalcMaxPage(totalPosts, postPerPage);

            currPage = GetCurrPage(currPage, ref maxPage);

            currPagePartners = partners
            .Skip((currPage - 1) * postPerPage)
            .Take(postPerPage).ToList()
            .Select(x => new PartnersViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Photo = Convert.ToBase64String(x.Photo.Bytes),
                Url = x.Url,
                PromoCode = x.PromoCode,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
            })
           .ToList();

            var result = new AllPartnersQueryModel
            {
                Partners = currPagePartners,
                CurrentPage = currPage,
                MaxPage = maxPage,
                SearchTerm = query.SearchTerm,
                Sorting = query.Sorting
            };

            return result;
        }

        public void AddPartner(PartnerFormModel model)
        {
            var photo = CreatePhoto(model.File);

            var partner = new Partner
            {
                Name = model.Name,
                Description = model.Description,
                Photo = photo,
                Url = model.Url,
                PromoCode = model.PromoCode,
                CreatedOn = DateTime.Now
            };

            context.Partners.Add(partner);
            context.SaveChanges();
        }

        public PartnerFormModel CreateEditModel(int partnerId)
        {
            var partner = GetPartnerById(partnerId);

            if(partner == null)
            {
                return null;
            }

            var model = new PartnerFormModel
            {
                Name = partner.Name,
                Description = partner.Description,
                Url = partner.Url,
                PromoCode = partner.PromoCode
            };

            return model;
        }

        public void EditPartner(int partnerId, PartnerFormModel model)
        {
            var partner = GetPartnerById(partnerId);

            if(partner != null)
            {
                var photo = CreatePhoto(model.File);

                partner.Name = model.Name;
                partner.Description = model.Description;
                partner.Photo = photo;
                partner.Url = model.Url;
                partner.PromoCode = model.PromoCode;

                context.SaveChanges();
            }
        }
        public void DeletePartner(Partner partner)
        {
            if(partner != null)
            {
                context.Partners.Remove(partner);
                context.SaveChanges();
            }
        }

        public Partner GetPartnerById(int id)
            => context.Partners
            .Include(p=> p.Photo)
            .FirstOrDefault(x => x.Id == id);

        private PartnerPhoto CreatePhoto(IFormFile file)
        {
            PartnerPhoto photo = null;

            if (file != null)
            {
                Task.Run(async () =>
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        if (memoryStream.Length < 2097152)
                        {
                            var newPhoto = new PartnerPhoto()
                            {
                                Bytes = memoryStream.ToArray(),
                                Description = file.FileName,
                                FileExtension = Path.GetExtension(file.FileName),
                                Size = file.Length,
                            };
                            photo = newPhoto;
                        }

                    }
                }).GetAwaiter()
               .GetResult();
            }

            return photo;
        }

        private List<Partner> GetPartners()
            => context.Partners
               .Include(p => p.Photo)
               .ToList();
    }
}
