namespace Problem_2_Extended_Database
{
    public class Person:IPerson
    {
        private string name;
        private long id;

        public Person(string name, long id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get => name; private set => name = value; }
        public long Id { get => id; private set => id = value; }

        public bool Equals(Person other)
        {
            if (this.Name==other.Name&&this.Id==other.Id)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Name: {Name} ; Id: {Id}";
        }
    }
}
