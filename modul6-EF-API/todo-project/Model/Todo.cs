namespace Model
{
    public class Todo
    {
        public int todoId{get;set;}
        public string? name{get;set;}
        public string? category{get;set;}
        public User? user{get;set;}
    }
}