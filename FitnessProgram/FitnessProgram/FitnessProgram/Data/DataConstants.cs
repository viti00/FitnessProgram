namespace FitnessProgram.Data
{
    public static class DataConstants
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
            public const int NameMaxLegth = 100;

            public const int DescriptionMinLegth = 2;
            public const int DescriptionMaxLegth = 10000;

            public const int PromoCodeMinLegth = 1;
            public const int PromoCodeMaxLegth = 15;
        }

        public class CustomerConstants
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 20;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;

            public const int DesiredResultMinLength = 2;
            public const int DesiredResultMaxLength = 10000;

            public const int AgeMinValue = 14;
            public const int AgeMaxValue = 90;

            public const string HeightMinValue = "90";
            public const string HeightMaxValue = "250";

            public const string WeightMinValue = "50";
            public const string WeightMaxValue = "400";
        }

        public class BestResultConstants
        {
            public const int StoryMinLegth = 10;
            public const int StoryMaxLegth = 10000;
        }
    }
}
