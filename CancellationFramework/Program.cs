class Program
{
    static async Task Main(string[] args)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        
        Console.WriteLine("Press any key to cancel the task...");

        Task longRunningTask = LongRunningOperation(cts.Token);

        // Wait for user input to cancel the task
        Console.ReadKey();
        cts.Cancel(); // Signal cancellation

        try
        {
            await longRunningTask; // Wait for the task to complete (or be canceled)
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Task was canceled.");
        }
    }

    private static async Task LongRunningOperation(CancellationToken ctsToken)
    {
        try
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"Working ... {i + 1} seconds");
                await Task.Delay(1000, ctsToken);
            }
            Console.WriteLine("Task completed successfully.");
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine("Task was canceled.");
        }
    }
}