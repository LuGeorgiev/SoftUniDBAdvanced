namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Linq;

    public class Bonus
    {
        private static string Relesaed = "Prisoner {0} released";
        private static string Lifetime = "Prisoner {0} is sentenced to life";
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            var prisoner = context.Prisoners.FirstOrDefault(x => x.Id == prisonerId);
            bool hasReleseDate = prisoner.ReleaseDate != null;
            if (!hasReleseDate)
            {
                return string.Format(Lifetime, prisoner.FullName);
            }

            prisoner.ReleaseDate = DateTime.Today;
            prisoner.CellId = null;
            context.Prisoners.Update(prisoner);
            context.SaveChanges();

            return string.Format(Relesaed, prisoner.FullName);
        }
    }
}
