namespace FitnessProgram.Services.PartnerService
{
    using FitnessProgram.Areas.Admin.Models.Partners;
    using FitnessProgram.Data.Models;

    public interface IPartnerService
    {
        public void AddPartner(PartnerFormModel model);

        public void EditPartner(int partnerId, PartnerFormModel model);

        public PartnerFormModel CreateEditModel(int partnerId);

        public Partner GetPartnerById(int id);
    }
}
