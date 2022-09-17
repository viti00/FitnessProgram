namespace FitnessProgram.Global
{
    public static class GlobalConstants
    {
        public class PostConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 30;

            public const int TextMinLegth = 2;
            public const int TextMaxLegth = 10000;
        }

        public class CommentConstants
        {
            public const int MessageMinLegth = 2;
            public const int MessageMaxLegth = 10000;
        }

        public class PartnerConstants
        {
            public const int NameMaxLegth = 30;

            public const int DescriptionMinLegth = 2;
            public const int DescriptionMaxLegth = 100;

            public const int PromoCodeMinLegth = 1;
            public const int PromoCodeMaxLegth = 15;
        }

        public class CustomerConstants
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;

            public const int PhoneNumberMinLength = 2;
            public const int PhoneNumberMaxLength = 15;

            public const int SexMinLength = 2;
            public const int SexMaxLength = 20;

            public const int DesiredResultMinLength = 2;
            public const int DesiredResultMaxLength = 10000;

            public const int AgeMinValue = 14;
            public const int AgeMaxValue = 90;
        }

        public class BestResultConstants
        {
            public const int StoryMinLegth = 10;
            public const int StoryMaxLegth = 10000;
        }

        public class UserConstants
        {
            public const int PasswordMinLegth = 6;
            public const int PasswordMaxLegth = 100;
        }
    }
}
