// See https://aka.ms/new-console-template for more information
using AppAlfaSoft;
using System.Net.Http.Headers;

Mutex mutex = new Mutex(true, name: "{AppAlfaSoft}");

if (!mutex.WaitOne(TimeSpan.Zero, true))
{
    Thread.Sleep(TimeSpan.FromSeconds(60));
}

Console.Write("Enter the file path: ");

string path = Console.ReadLine();

while (string.IsNullOrEmpty(path))
{
    Console.WriteLine("Path not informed!");
    Console.Write("Enter the file path: ");
    path = Console.ReadLine();
}

string[] names = File.ReadAllLines(path);
CustomStopwatch sw = new();

foreach (var name in names)
{
    Console.WriteLine("User: " + name);
    CallApi(name);
    
}

Thread.Sleep(5000);

void CallApi(string nome)
{
    if (sw.StartAt != null)
        Wait5Seconds();

    using var client = new HttpClient();
    client.BaseAddress = new Uri("https://api.bitbucket.org/2.0/users/");
    client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
    client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

    var url = client.BaseAddress + nome + "/";
    Console.WriteLine("Calling Url: " + url);
    
    sw.Start();
    var response = client.GetAsync(nome + "/").Result;
    var resp = response.Content.ReadAsStringAsync().Result;

    Console.WriteLine("Response: " + resp);
    SaveLog(resp, nome);
}

void SaveLog(string response, string name)
{
    var path = String.Concat(Environment.CurrentDirectory, "\\Log.txt");

    FileStream fs = new(path,
                FileMode.Append);

    StreamWriter sw = new(fs);
    sw.WriteLine("User: " + name);
    sw.WriteLine("Date: " + DateTime.Now.ToString());
    sw.WriteLine("Respose" + response);
    sw.Flush();
    sw.Close();
    fs.Close();
}

void Wait5Seconds()
{
    var difference = (DateTime.Now - sw.StartAt);

    if (difference.Value.TotalMilliseconds < 5000)
    {
        int Milliseconds = Convert.ToInt32(difference.Value.TotalMilliseconds);
        Thread.Sleep(5000 - Milliseconds);
    }
}





