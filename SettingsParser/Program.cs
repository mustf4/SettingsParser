using SettingsParser;

string _configuration = @"UserName:admin;
        Password:""super%^&*333password;
        DNSName:SomeName;
        Username: myuser;

        TimeToLive:4;
        ClusterSize:2;
        PortNumber:2222;

        IsEnabled:true;
        EnsureTransaction:false;
        PersistentStorage:false;";

Parser parser = new Parser();
dynamic result = parser.Parse(_configuration);
Console.WriteLine($"UserName: {result.UserName}, type is: {result.UserName.GetType()}");
Console.WriteLine($"Password: {result.Password}, type is: {result.Password.GetType()}");
Console.WriteLine($"DNSName: {result.DNSName}, type is: {result.DNSName.GetType()}");
Console.WriteLine($"TimeToLive: {result.TimeToLive}, type is: {result.TimeToLive.GetType()}");
Console.WriteLine($"ClusterSize: {result.ClusterSize}, type is: {result.ClusterSize.GetType()}");
Console.WriteLine($"PortNumber: {result.PortNumber}, type is: {result.PortNumber.GetType()}");
Console.WriteLine($"IsEnabled: {result.IsEnabled}, type is: {result.IsEnabled.GetType()}");
Console.WriteLine($"EnsureTransaction: {result.EnsureTransaction}, type is: {result.EnsureTransaction.GetType()}");
Console.WriteLine($"PersistentStorage: {result.PersistentStorage}, type is: {result.PersistentStorage.GetType()}");
