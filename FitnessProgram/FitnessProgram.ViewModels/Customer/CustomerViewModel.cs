namespace FitnessProgram.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; init; }

        public string FullName { get; init; }

        public string PhoneNumber { get; init; }

        public string Email { get; init; }

        public string Sex { get; init; }

        public int Age { get; init; }

        public string DesiredResults { get; init; }

        public bool IsApproved { get; init; }
    }
}
