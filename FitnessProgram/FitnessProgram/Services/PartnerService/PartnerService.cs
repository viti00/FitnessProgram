namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Areas.Admin.Models.Partners;
    using FitnessProgram.Data;
    using FitnessProgram.Data.Models;

    public class PartnerService : IPartnerService
    {
        private readonly FitnessProgramDbContext context;

        public PartnerService(FitnessProgramDbContext context) 
            => this.context = context;

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

        public Partner GetPartnerById(int id)
            => context.Partners.FirstOrDefault(x => x.Id == id);
    }
}
