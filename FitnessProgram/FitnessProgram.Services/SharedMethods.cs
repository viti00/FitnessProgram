namespace FitnessProgram.Services
{
    public class SharedMethods
    {
        public static int CalcMaxPage(int totalPosts, int postPerPage)
        {
            var maxPage = (int)Math.Ceiling((double)totalPosts / postPerPage);

            return maxPage;
        }

        public static int GetCurrPage(int currPage, ref int maxPage)
        {
            if (currPage > maxPage)
            {
                if (maxPage == 0)
                {
                    maxPage = 1;
                }
                currPage = maxPage;
            }
            if(currPage == 0)
            {
                currPage = 1;
            }

            return currPage;
        }
    }
}
