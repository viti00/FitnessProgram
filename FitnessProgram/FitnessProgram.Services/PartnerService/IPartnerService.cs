namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Data.Models;
    using FitnessProgram.ViewModels.Partner;

    public interface IPartnerService
    {
        public AllPartnersQueryModel GetAll(int currPage, int postPerPage, bool isAdministrator);

        public void AddPartner(PartnerFormModel model);

        public void EditPartner(int partnerId, PartnerFormModel model);

        public void DeletePartner(Partner partner);

        public PartnerFormModel CreateEditModel(int partnerId);

        public Partner GetPartnerById(int id);
    }
}
