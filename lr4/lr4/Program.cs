using System.Reflection;

Console.WriteLine("----------------------------------2----------------------------------");
Person person = new Person("Vova Olexienko", 20, 1.83, true);
Type type = person.GetType();
Console.WriteLine("FullName = " + type.FullName);
Console.WriteLine("IsAbstract = " + type.IsAbstract);
Console.WriteLine("IsValueType = " + type.IsValueType);
Console.WriteLine("BaseType = " + type.BaseType);
Console.WriteLine("IsSealed = " + type.IsSealed);
Console.WriteLine("IsSubclassOf object = " + type.IsSubclassOf(typeof(object)));
TypeInfo typeInfo = type.GetTypeInfo();

Console.WriteLine("----------------------------------3----------------------------------");
MemberInfo[] members = typeInfo.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
foreach (MemberInfo member in members)
{
    Console.WriteLine(member.Name);
}

Console.WriteLine("----------------------------------4----------------------------------");
foreach (FieldInfo field in typeInfo.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
{
    Console.WriteLine(field.Name + " = " + field.GetValue(person));
}
FieldInfo ageField = typeInfo.GetField("age");
ageField.SetValue(person, 31);
Console.WriteLine("New age = " + person.age);

Console.WriteLine("----------------------------------5----------------------------------");
MethodInfo greetMethod = typeInfo.GetMethod("Greet");
greetMethod.Invoke(person, null);
MethodInfo growMethod = typeInfo.GetMethod("Grow");
growMethod.Invoke(person, null);
Console.WriteLine("New age = " + person.age);
MethodInfo addFriendMethod = typeInfo.GetMethod("AddFriend");
addFriendMethod.Invoke(person, new[] { "FirstFriend" });
Console.WriteLine("New friends = " + String.Join(" ", person.friends));

public class Person
{
    private bool isStudent;
    internal List<string> friends;
    protected string name;
    protected internal double height;
    public int age;

    public Person(string name, int age, double height, bool isStudent)
    {
        this.name = name;
        this.age = age;
        this.height = height;
        this.isStudent = isStudent;
        friends = new List<string>();
    }

    public void Greet()
    {
        Console.WriteLine("Hello, my name is " + name);
    }

    public void Grow()
    {
        age++;
    }

    public void AddFriend(string friend)
    {
        friends.Add(friend);
    }
}