namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Areas.Admin.Models.Partners;
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Partners;

    public class PartnerService : IPartnerService
    {
        private readonly FitnessProgramDbContext context;

        public PartnerService(FitnessProgramDbContext context) 
            => this.context = context;

        public AllPartnersQueryModel GetAll(int currPage, int postPerPage)
        {
            var totalPosts = context.Partners.Count();

            var maxPage = (int)Math.Ceiling((double)totalPosts / postPerPage);

            if (currPage > maxPage)
            {
                if (maxPage == 0)
                {
                    maxPage = 1;
                }
                currPage = maxPage;
            }

            var posts = context.Partners
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

            var result = new AllPartnersQueryModel
            {
                Partners = posts,
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
