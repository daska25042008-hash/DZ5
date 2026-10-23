using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Student
{
    public string Surname;
    public string Name;
    public int BirthYear;
    public string Exam;
    public int Score;
    public Student(string surname,string name,int birthYear,string exam,int score)
    {
        Surname=surname;
        Name=name;
        BirthYear=birthYear;
        Exam=exam;
        Score=score;
    }
    public override string ToString()
    {
        return Surname+" "+Name+", год рождения: "+BirthYear+", экзамен: "+Exam+", баллы: "+Score;
    }
}
class Grandma
{
    public string Name;
    public int Age;
    public List<string> Diseases=new List<string>();
    public Dictionary<string,string> Medicines=new Dictionary<string,string>();
    public Grandma(string name,int age)
    {
        Name=name;
        Age=age;
    }
}
class Hospital
{
    public string Name;
    public List<string> Diseases;
    public int Capacity;
    public List<Grandma> Patients=new List<Grandma>();
    public Hospital(string name,List<string> diseases,int capacity)
    {
        Name=name;
        Diseases=diseases;
        Capacity=capacity;
    }
}
class Program
{
    static void Main()
    {
        Task1();
        Task2();
        Task3();
        Task4();
        Console.WriteLine("\nВсе четыре задачи выполнены.");
    }
    static void Task1()
    {
        Console.WriteLine("\nЗадача 1. Перемешивание 32 пар картинок.");
        string[] folders=
        {
            Path.Combine(Directory.GetCurrentDirectory(),"Images"),
            Path.Combine(Directory.GetCurrentDirectory(),"Project1","Images"),
            Path.Combine(AppContext.BaseDirectory,"Images")
        };
        string imageFolder="";
        string[] files=new string[0];
        foreach(string folder in folders)
        {
            if(!Directory.Exists(folder)) continue;
            string[] found=Directory.GetFiles(folder)
                .Where(file=>file.EndsWith(".jpg",StringComparison.OrdinalIgnoreCase)||
                             file.EndsWith(".jpeg",StringComparison.OrdinalIgnoreCase)||
                             file.EndsWith(".png",StringComparison.OrdinalIgnoreCase)||
                             file.EndsWith(".gif",StringComparison.OrdinalIgnoreCase)||
                             file.EndsWith(".webp",StringComparison.OrdinalIgnoreCase))
                .OrderBy(file=>file)
                .ToArray();
            if(found.Length>=32)
            {
                imageFolder=folder;
                files=found.Take(32).ToArray();
                break;
            }
        }
        if(files.Length!=32)
        {
            Console.WriteLine("Не найдено 32 изображения.");
            Console.WriteLine("Создайте папку Images и положите туда 32 картинки.");
            return;
        }
        List<string> pictures=new List<string>();
        foreach(string file in files)
        {
            pictures.Add(file);
            pictures.Add(file);
        }
        Console.WriteLine("Исходный список:");
        foreach(string picture in pictures)
            Console.Write(Path.GetFileName(picture)+" ");
        Random random=new Random();
        for(int i=pictures.Count-1;i>0;i--)
        {
            int j=random.Next(i+1);
            string temp=pictures[i];
            pictures[i]=pictures[j];
            pictures[j]=temp;
        }
        Console.WriteLine("\nПеремешанный список:");
        foreach(string picture in pictures)
            Console.Write(Path.GetFileName(picture)+" ");
        Console.WriteLine("\nВсего элементов: "+pictures.Count);
    }
    static void Task2()
    {
        Console.WriteLine("\nЗадача 2. Работа со студентами.");
        string file=Path.Combine(Directory.GetCurrentDirectory(),"students.txt");
        Dictionary<string,Student> students=new Dictionary<string,Student>(StringComparer.OrdinalIgnoreCase);
        if(!File.Exists(file))
        {
            string[] sample=
            {
                "Иванов;Иван;2008;Математика;245",
                "Петров;Пётр;2007;Физика;260",
                "Сидорова;Анна;2008;Информатика;270",
                "Смирнов;Алексей;2007;Физика;230",
                "Кузнецова;Мария;2008;Математика;280",
                "Попов;Дмитрий;2007;Физика;250",
                "Васильева;Елена;2008;Информатика;255",
                "Соколов;Артём;2007;Математика;220",
                "Михайлова;Ольга;2008;Физика;275",
                "Новиков;Илья;2007;Информатика;240"
            };
            File.WriteAllLines(file,sample);
            Console.WriteLine("Создан файл students.txt с примерами студентов.");
        }
        foreach(string line in File.ReadAllLines(file))
        {
            string[] data=line.Split(';');
            if(data.Length==5&&int.TryParse(data[2],out int year)&&int.TryParse(data[4],out int score))
            {
                Student student=new Student(data[0],data[1],year,data[3],score);
                students[data[0]+"|"+data[1]]=student;
            }
        }
        while(true)
        {
            Console.WriteLine("\nЗадача 2. Действия со студентами");
            Console.WriteLine("1. Новый студент");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Сортировать по баллам");
            Console.WriteLine("4. Показать всех студентов");
            Console.WriteLine("0. Перейти к следующей задаче");
            Console.Write("Выберите действие: ");
            string choice=Console.ReadLine()??"";
            if(choice=="0") break;
            if(choice=="1")
            {
                Console.Write("Фамилия: ");
                string surname=Console.ReadLine()??"";
                Console.Write("Имя: ");
                string name=Console.ReadLine()??"";
                Console.Write("Год рождения: ");
                if(!int.TryParse(Console.ReadLine(),out int year))
                {
                    Console.WriteLine("Неверный год.");
                    continue;
                }
                Console.Write("Вступительный экзамен: ");
                string exam=Console.ReadLine()??"";
                Console.Write("Баллы: ");
                if(!int.TryParse(Console.ReadLine(),out int score))
                {
                    Console.WriteLine("Неверные баллы.");
                    continue;
                }
                Student student=new Student(surname,name,year,exam,score);
                students[surname+"|"+name]=student;
                SaveStudents(file,students);
                Console.WriteLine("Студент добавлен.");
            }
            else if(choice=="2")
            {
                Console.Write("Фамилия: ");
                string surname=Console.ReadLine()??"";
                Console.Write("Имя: ");
                string name=Console.ReadLine()??"";
                if(students.Remove(surname+"|"+name))
                {
                    SaveStudents(file,students);
                    Console.WriteLine("Студент удалён.");
                }
                else Console.WriteLine("Студент не найден.");
            }
            else if(choice=="3")
            {
                foreach(Student student in students.Values.OrderBy(student=>student.Score))
                    Console.WriteLine(student);
            }
            else if(choice=="4")
            {
                foreach(Student student in students.Values)
                    Console.WriteLine(student);
            }
            else Console.WriteLine("Нет такого действия.");
        }
    }
    static void SaveStudents(string file,Dictionary<string,Student> students)
    {
        List<string> lines=new List<string>();
        foreach(Student student in students.Values)
            lines.Add(student.Surname+";"+student.Name+";"+student.BirthYear+";"+student.Exam+";"+student.Score);
        File.WriteAllLines(file,lines);
    }
    static void Task3()
    {
        Console.WriteLine("\nЗадача 3. Распределение бабушек по больницам.");
        Stack<Hospital> hospitals=new Stack<Hospital>();
        hospitals.Push(new Hospital("Специализированная больница",new List<string>{"диабет","суставы","сердце"},2));
        hospitals.Push(new Hospital("Поликлиника",new List<string>{"простуда","давление","кашель"},2));
        hospitals.Push(new Hospital("Городская больница",new List<string>{"сердце","давление","диабет"},3));
        Queue<Grandma> grandmas=new Queue<Grandma>();
        List<Grandma> allGrandmas=new List<Grandma>();
        Console.Write("Сколько бабушек поступает? ");
        if(!int.TryParse(Console.ReadLine(),out int count)||count<1)
        {
            Console.WriteLine("Введите положительное число.");
            return;
        }
        for(int i=0;i<count;i++)
        {
            Console.WriteLine("\nБабушка номер "+(i+1));
            Console.Write("Имя: ");
            string name=Console.ReadLine()??"";
            Console.Write("Возраст: ");
            if(!int.TryParse(Console.ReadLine(),out int age))
            {
                Console.WriteLine("Неверный возраст. Используем 70.");
                age=70;
            }
            Grandma grandma=new Grandma(name,age);
            Console.Write("Болезни через запятую или напишите нет: ");
            string input=Console.ReadLine()??"";
            if(!input.Equals("нет",StringComparison.OrdinalIgnoreCase)&&input.Trim()!="")
            {
                foreach(string item in input.Split(','))
                {
                    string disease=item.Trim();
                    if(disease=="") continue;
                    grandma.Diseases.Add(disease);
                    Console.Write("Лекарство от болезни "+disease+": ");
                    grandma.Medicines[disease]=Console.ReadLine()??"";
                }
            }
            grandmas.Enqueue(grandma);
            allGrandmas.Add(grandma);
        }
        List<Grandma> crying=new List<Grandma>();
        Dictionary<Grandma,string> destinations=new Dictionary<Grandma,string>();
        while(grandmas.Count>0)
        {
            Grandma grandma=grandmas.Dequeue();
            Hospital chosen=null!;
            bool found=false;
            foreach(Hospital hospital in hospitals)
            {
                if(hospital.Patients.Count>=hospital.Capacity) continue;
                if(grandma.Diseases.Count==0)
                {
                    chosen=hospital;
                    found=true;
                    break;
                }
                int treated=0;
                foreach(string disease in grandma.Diseases)
                {
                    if(hospital.Diseases.Any(item=>item.Equals(disease,StringComparison.OrdinalIgnoreCase)))
                        treated++;
                }
                if(treated>grandma.Diseases.Count/2.0)
                {
                    chosen=hospital;
                    found=true;
                    break;
                }
            }
            if(found)
            {
                chosen.Patients.Add(grandma);
                destinations[grandma]=chosen.Name;
                Console.WriteLine(grandma.Name+" принята в больницу: "+chosen.Name);
            }
            else
            {
                crying.Add(grandma);
                destinations[grandma]="Место не найдено";
                Console.WriteLine(grandma.Name+" осталась на улице плакать.");
            }
        }
        Console.WriteLine("\nСписок всех бабушек:");
        foreach(Grandma grandma in allGrandmas)
        {
            Console.WriteLine(grandma.Name+", возраст: "+grandma.Age);
            if(grandma.Diseases.Count==0) Console.WriteLine("Болезней нет.");
            foreach(string disease in grandma.Diseases)
                Console.WriteLine(disease+", лекарство: "+grandma.Medicines[disease]);
            Console.WriteLine("Результат: "+destinations[grandma]);
        }
        Console.WriteLine("\nСписок больниц:");
        foreach(Hospital hospital in hospitals)
        {
            double percent=(double)hospital.Patients.Count/hospital.Capacity*100;
            Console.WriteLine("\nБольница: "+hospital.Name);
            Console.WriteLine("Лечит: "+string.Join(", ",hospital.Diseases));
            Console.WriteLine("Пациентов: "+hospital.Patients.Count+" из "+hospital.Capacity);
            Console.WriteLine("Заполненность: "+percent.ToString("F1")+"%");
        }
    }
    static void Task4()
    {
        Console.WriteLine("\nЗадача 4. Поиск кратчайшего пути в графе.");
        Console.WriteLine("Вершины графа: A, B, C, D, E, F.");
        Dictionary<string,List<string>> graph=new Dictionary<string,List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            {"A",new List<string>{"B","C"}},
            {"B",new List<string>{"A","D","E"}},
            {"C",new List<string>{"A","D"}},
            {"D",new List<string>{"B","C","F"}},
            {"E",new List<string>{"B","F"}},
            {"F",new List<string>{"D","E"}}
        };
        Console.Write("Начальная вершина: ");
        string start=(Console.ReadLine()??"").ToUpper();
        Console.Write("Конечная вершина: ");
        string finish=(Console.ReadLine()??"").ToUpper();
        if(!graph.ContainsKey(start)||!graph.ContainsKey(finish))
        {
            Console.WriteLine("Такой вершины нет.");
            return;
        }
        Queue<string> queue=new Queue<string>();
        Dictionary<string,string> previous=new Dictionary<string,string>();
        queue.Enqueue(start);
        previous[start]="";
        while(queue.Count>0)
        {
            string current=queue.Dequeue();
            if(current==finish) break;
            foreach(string next in graph[current])
            {
                if(!previous.ContainsKey(next))
                {
                    previous[next]=current;
                    queue.Enqueue(next);
                }
            }
        }
        if(!previous.ContainsKey(finish))
        {
            Console.WriteLine("Путь не найден.");
            return;
        }
        List<string> path=new List<string>();
        string vertex=finish;
        while(vertex!="")
        {
            path.Add(vertex);
            vertex=previous[vertex];
        }
        path.Reverse();
        Console.WriteLine("Кратчайший путь:");
        Console.WriteLine(string.Join(" -> ",path));
        Console.WriteLine("Количество переходов: "+(path.Count-1));
    }
}
