class WorkDay
{
    static void Main(string[] args)
    {
        Console.WriteLine("Количество занятых промежутков");
        int numberGaps = Convert.ToInt32(Console.ReadLine());
        TimeSpan[] massivBusy = new TimeSpan[numberGaps];
        for (int i = 0; i < numberGaps; i++)
        {
            Console.WriteLine("Введите время начала всех занятых промежутков ");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());
            massivBusy[i] = time;
        }
        int[] massivDuration = new int[numberGaps];
        for (int j = 0; j < numberGaps; j++)
        {
            Console.WriteLine("Длительность всех переменных");
            int duration = Convert.ToInt32(Console.ReadLine());
            massivDuration[j] = duration;
        }
        var massivB = massivBusy.ToArray();
        var massivD = massivDuration.ToArray();

        Console.WriteLine("Минимальное время для работы менеджера");
        TimeSpan consultationTime = new TimeSpan(0, int.Parse(Console.ReadLine()), 0);

        Console.WriteLine("Начало рабочего дня");
        TimeSpan beginWorkingDay = TimeSpan.Parse(Console.ReadLine());

        Console.WriteLine("Конец рабочего дня");
        TimeSpan endWorkingDay = TimeSpan.Parse(Console.ReadLine());
        Console.WriteLine(String.Join("\n", AvailablePeriods(massivBusy,massivDuration,beginWorkingDay, endWorkingDay, consultationTime)));
    }
    static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, TimeSpan consultationTime)
    {
        var result = new List<string>();
        var currentTime = beginWorkingTime;
        var indexStartInterval = 0;
        while (currentTime < endWorkingTime && indexStartInterval < startTimes.Length)
        {
            var duration = durations[indexStartInterval];
            if ((currentTime <= startTimes[indexStartInterval])
             && ((startTimes[indexStartInterval] - currentTime) >= consultationTime))
            {
                var timeSpan = currentTime + consultationTime;
                string exit = $"{currentTime.Hours:00}:{currentTime.Minutes:00}-{timeSpan.Hours:00}:{timeSpan.Minutes:00}";
                result.Add(exit);
                currentTime = currentTime.Add(consultationTime);
            }
            else
            {
                currentTime = startTimes[indexStartInterval] + new TimeSpan(0, duration, 0);
                indexStartInterval++;
            }

        }
        while (currentTime < endWorkingTime && (endWorkingTime - currentTime) >= consultationTime)
        {
            var timeSpan = currentTime + consultationTime;
            string exit = $"{currentTime.Hours:00}:{currentTime.Minutes:00}-{timeSpan.Hours:00}:{timeSpan.Minutes:00}";
            result.Add(exit);
            currentTime = currentTime.Add(consultationTime);
        }

        return result.ToArray();
    }
}
