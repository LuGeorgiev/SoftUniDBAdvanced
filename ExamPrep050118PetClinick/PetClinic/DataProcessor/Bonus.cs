namespace PetClinic.DataProcessor
{
    using System;
    using System.Linq;
    using PetClinic.Data;

    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            string succesfulUpdate = "{0}'s profession updated from {1} to {2}.";
            string failToUpdate = "Vet with phone number {0} not found!";

            var vet = context.Vets.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
            if (vet==null)
            {
                return string.Format(failToUpdate, phoneNumber);
            }

            var oldProffesion = vet.Profession;
            vet.Profession = newProfession;
            context.Vets.Update(vet);
            context.SaveChanges();

            return string.Format(succesfulUpdate, vet.Name, oldProffesion, newProfession);
        }
    }
}
