namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Areas.Admin.Models.Partners;
    using FitnessProgram.Data.Models;
    using FitnessProgram.Models.Partners;

    public interface IPartnerService
    {
        public AllPartnersQueryModel GetAll(int currPage, int postPerPage);

        public void AddPartner(PartnerFormModel model);

        public void EditPartner(int partnerId, PartnerFormModel model);

        public void DeletePartner(Partner partner);

        public PartnerFormModel CreateEditModel(int partnerId);

        public Partner GetPartnerById(int id);
    }
}
