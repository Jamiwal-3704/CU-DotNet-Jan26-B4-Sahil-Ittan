using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

namespace SocialMediaApplication
{
    class Person
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public Person(int id, string name) 
        {
            UserName= name ; 
            Id = id; 
        }
        public List<Person> friendList = new List<Person>();

        // BEHAVIOUR 
        public void AddFriend(Person friend)
        {
            if(!friendList.Contains(friend))
            {
                friendList.Add(friend);
                // bi directional connecting or adding friend to the list
                friend.friendList.Add(this);
            }
        }

        public void removeFriend(Person person)
        {
            if(friendList.Contains(person))
            {
                friendList.Remove(person);
                Console.WriteLine($"{person.UserName} has been removed from {UserName}'s friend list.");
            }
        }
    }

    class SocialNetwork
    {
        private List<Person> members = new List<Person>();
        public void AddMember(Person member)
        {
            members.Add(member);
        }

        public void ShowNetwork()
        {
            foreach(var member in members)
            {
                Console.Write(member.UserName + "->");
                List<string> friendNames = new List<string>();
                foreach (var friend in member.friendList)
                {
                    friendNames.Add(friend.UserName);
                }
                Console.WriteLine(string.Join(", ", friendNames));
            }
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SocialNetwork network = new SocialNetwork();

            Person aman = new Person(1,"Aman");
            Person bhaskar = new Person(2,"bhaskar");
            Person chetan = new Person(3,"Chetan");
            Person divakar = new Person(4,"Divakar");

            network.AddMember(aman);
            network.AddMember(bhaskar);
            network.AddMember(chetan);
            network.AddMember(divakar);

            aman.AddFriend(bhaskar);
            aman.AddFriend(chetan);
            bhaskar.AddFriend(chetan);
            chetan.AddFriend(divakar);
            chetan.AddFriend(aman);
            divakar.AddFriend(chetan);
            network.ShowNetwork();


            // Removing a friend
            aman.removeFriend(bhaskar);
            chetan.removeFriend(divakar);
            Console.WriteLine();
            Console.WriteLine("After removing friends:");
            network.ShowNetwork();

        }
    }
}
