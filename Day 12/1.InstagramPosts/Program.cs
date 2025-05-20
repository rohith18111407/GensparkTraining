using System;

public class Post
{
    public string caption {  get; set; }
    public int likes { get; set; }

    public Post(string caption, int likes)
    {
        this.caption = caption;
        this.likes = likes;
    }
}

public class InstagramPosts
{
    public static void GetDetails()
    {
        Console.WriteLine("\nPls enter the number of users: ");
        int userCount;
        while(!int.TryParse(Console.ReadLine(), out userCount) || userCount<=0)
        {
            Console.WriteLine("Invalid Input. pls enter a valid positve integer");
        }

        Post[][] instagramData = new Post[userCount][];

        for(int i = 0; i < userCount; i++)
        {
            Console.WriteLine($"\nUser {i+1}: PLs enter number of posts: ");
            int postCount;
            while (!int.TryParse(Console.ReadLine(), out postCount) || postCount < 0)
            {
                Console.WriteLine("\nInvalid Input. pls enter a valid positve integer");
            }

            instagramData[i] = new Post[postCount];

            for(int j=0; j < postCount; j++)
            {
                Console.WriteLine($"\nPls enter caption for post {j+1}: ");
                string caption = Console.ReadLine();

                Console.WriteLine($"\nPls enter the likes count: ");
                int likes;
                while (!int.TryParse(Console.ReadLine(), out likes) || likes < 0)
                {
                    Console.WriteLine("\nInvalid Input. pls enter a valid positve integer");
                }

                instagramData[i][j] = new Post(caption, likes);
            }
        }

        DisplayPosts(instagramData);
    }

    public static void DisplayPosts(Post[][] instagramData)
    {
        Console.WriteLine("\n--- Displaying Instagram Posts ---");
        for (int i = 0; i < instagramData.Length; i++)
        {
            Console.WriteLine($"User {i + 1}:");
            for (int j = 0; j < instagramData[i].Length; j++)
            {
                Console.WriteLine($"Post {j + 1} - Caption: {instagramData[i][j].caption} | Likes: {instagramData[i][j].likes}");
            }
            if (instagramData[i].Length == 0)
            {
                Console.WriteLine("No posts.");
            }
        }
    }

    public static void Main(string[] args)
    {
        GetDetails();

        Console.ReadKey();
    }
}