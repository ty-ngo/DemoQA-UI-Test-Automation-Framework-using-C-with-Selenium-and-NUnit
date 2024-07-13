namespace final.Models
{
    public class Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string mobileNumber { get; set; }
        public string dateOfBirth { get; set; }
        public string[] subjects { get; set; }
        public string[] hobbies { get; set; }
        public string picturePath {  get; set; }
        public string currentAddress { get; set; }
        public string state { get; set; }
        public string city { get; set; }

        public Student() { }
    }
}
