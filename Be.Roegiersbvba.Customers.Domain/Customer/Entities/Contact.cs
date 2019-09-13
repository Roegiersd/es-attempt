namespace be.roegiersbvba.Customers.Domain
{
    public class Contact
    {
        public string Name { get; private set; }
        public string FirstName { get; private set; }
        public Address Address { get; private set; }

        public void ChangeFirstName(string newName)
        {
            //Raiseevent
        }

        public void ChangeLastName(string newName)
        {
            //Raiseeven.
        }
    }
}