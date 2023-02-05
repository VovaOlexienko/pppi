using Newtonsoft.Json;

namespace lr3;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Main method...");

        //Використаємо async та await для асинхроного виконання двух довгих http запитів 
        Console.WriteLine("Get data from json placeholder...");
        GetDataFromJsonPlaceholder();

        //За допомогою Thread зсимулюємо роботу магазину з покупцями та постачальниками
        Console.WriteLine("Starting the shop simulation...");
        SimulateShop();
        
        //За допомогою Thread порахуємо та виведемо суму чисел
        Console.WriteLine("Starting numbers calculations...");
        CalculateSumOfNumbers();

        Console.WriteLine("End of Main method...");
    }

    private static void CalculateSumOfNumbers()
    {
        new Thread(() =>
        {
            var sum = 0;
            for (var i = 1; i <= 100; i++)
            {
                sum += i;
            }

            Console.WriteLine("Sum of numbers from 1 to 100: " + sum);
        }).Start();
    }

    private static void SimulateShop()
    {
        var store = 10;
        var semaphore = new Semaphore(1, 1);
        var supplier = new Thread(() => Supply(ref store, semaphore));
        var customer1 = new Thread(() => Buy(ref store, semaphore));
        var customer2 = new Thread(() => Buy(ref store, semaphore));

        supplier.Start();
        customer1.Start();
        customer2.Start();

        supplier.Join();
        customer1.Join();
        customer2.Join();
    }

    private static void Supply(ref int store, Semaphore semaphore)
    {
        for (var i = 0; i < 5; i++)
        {
            semaphore.WaitOne();
            store += 10;
            Console.WriteLine("Supplied 10 items, store now has: " + store);
            semaphore.Release();
            Thread.Sleep(100);
        }
    }

    private static void Buy(ref int store, Semaphore semaphore)
    {
        for (var i = 0; i < 5; i++)
        {
            semaphore.WaitOne();
            store -= 5;
            Console.WriteLine("Bought 5 items, store now has: " + store);
            semaphore.Release();
            Thread.Sleep(150);
        }
    }

    private static async void GetDataFromJsonPlaceholder()
    {
        var post = await GetPostData();
        var comments = await GetCommentData();
        Console.WriteLine("Post from jsonPlaceholder: " + JsonConvert.SerializeObject(post));
        Console.WriteLine("Comment from jsonPlaceholder: " + JsonConvert.SerializeObject(comments[0]));
    }

    private static async Task<Post> GetPostData()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
        return JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
    }

    private static async Task<List<Comment>> GetCommentData()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/1/comments");
        return JsonConvert.DeserializeObject<List<Comment>>(await response.Content.ReadAsStringAsync());
    }
}

class Post
{
    public string UserId { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

class Comment
{
    public string PostId { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}