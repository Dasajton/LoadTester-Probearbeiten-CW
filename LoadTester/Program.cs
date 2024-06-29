
public class Container
{
    public int length;
    public int width;
    public int height;
    public int maxHeight;
    public int usedHeight;
    public int loadedItems;
    public List<Item> contents;
    
    public Container(int length, int width, int height, int maxHeight)
    {
        this.length = length;
        this.width = width;
        this.height = height;
        this.maxHeight = maxHeight;
        this.contents = new List<Item>();
    }

    public bool CanLoadItem(Item item)
    {
        return this.usedHeight + item.height <= this.maxHeight;
    }

    public void AddItem(Item item)
    {
        this.usedHeight += item.height;
        this.loadedItems++;
    }

}

public class Item
{
    public int length;
    public int width;
    public int height;

    public Item(int length, int width, int height)
    {
        this.length = length;
        this.width = width;
        this.height = height;
    }
}

public class LoadTester
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Länge des Containers: ");
        int containerLength = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Breite des Containers: ");
        int containerWidth = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Höhe des Containers: ");
        int containerHeight = Convert.ToInt32(Console.ReadLine());
        int containerMaxHeight;
        do
        {
            Console.WriteLine("Bitte geben Sie die maximal Stapelbare Höhe des Containers an: ");
            containerMaxHeight = Convert.ToInt32(Console.ReadLine());

            if (containerMaxHeight > containerHeight)
            {
                Console.WriteLine("Die maximal Stapelbare Höhe kann nicht über der insgesamt vorhandenen Höhe liegen. Bitte geben Sie einen passenden Höhenwert ein: ");
            }
        }
        while (containerMaxHeight > containerHeight);

        Console.WriteLine("Containerparameter wurden erstellt! Länge: {0}cm, Breite: {1}cm, Höhe: {2}cm.", containerLength, containerWidth, containerHeight);
        Console.WriteLine("Die maximal Stapelbare Höhe beträgt {0}cm.", containerMaxHeight);
        Console.WriteLine("________________________");

        Queue<Item> itemsQueue = new Queue<Item>();

        string answer;
        do
        {
            Console.WriteLine("Möchten Sie der Ladeliste ein Packstück hinzufügen? Bitte mit 'Ja' oder 'Nein' beantworten: ");
            answer = Console.ReadLine();

            if (answer == "Ja")
            {
                int itemLength;
                do
                {
                    Console.WriteLine("Länge des Packstücks: ");
                    itemLength = Convert.ToInt32(Console.ReadLine());

                    if (itemLength > containerLength)
                    {
                        Console.WriteLine("Die Länge des Packstücks kann nicht über der maximalen Länge des Containers liegen. Bitte geben Sie einen passenden Längenwert ein: ");
                    }
                }
                while (itemLength > containerLength);

                int itemWidth;
                do
                {
                    Console.WriteLine("Breite des Packstücks: ");
                    itemWidth = Convert.ToInt32(Console.ReadLine());

                    if (itemWidth > containerWidth)
                    {
                        Console.WriteLine("Die Breite des Packstücks kann nicht über der maximalen Breite des Containers liegen. Bitte geben Sie einen passenden Breitenwert ein: ");
                    }
                }
                while (itemWidth > containerWidth);

                int itemHeight;
                do
                {
                    Console.WriteLine("Höhe des Packstücks: ");
                    itemHeight = Convert.ToInt32(Console.ReadLine());

                    if (itemHeight > containerMaxHeight)
                    {
                        Console.WriteLine("Die Höhe des Packstücks kann nicht über der maximal Stapelbaren Höhe des Containers liegen. Bitte geben Sie einen passenden Höhenwert ein: ");
                    }
                }
                while (itemHeight > containerMaxHeight);

                Item item = new Item(itemLength, itemWidth, itemHeight);
                itemsQueue.Enqueue(item);
                Console.WriteLine("Packstück wurde der Ladeliste erfolgreich hinzugefügt!");
                Console.WriteLine("________________________");
            }
        }
        while (answer == "Ja");

        Console.WriteLine("________________________");
        Console.WriteLine("{0} Packstücke wurden erfolgreich erstellt: ", itemsQueue.Count);
        foreach (Item item in itemsQueue)
        {
            Console.WriteLine("Packstück: Länge: {0}cm, Breite: {1}cm, Höhe: {2}cm", item.length, item.width, item.height);

        }
        Console.WriteLine("________________________");

        List<Container> containers = new List<Container>();
        containers.Add(new Container(containerLength, containerWidth, containerHeight, containerMaxHeight));
        int currentContainerIndex = 0;

        while (itemsQueue.Count > 0)
        {
            Item currentItem = itemsQueue.Dequeue();
            if (!containers[currentContainerIndex].CanLoadItem(currentItem))
            {
                containers.Add(new Container(containerLength, containerWidth, containerHeight, containerMaxHeight));
                currentContainerIndex++;
            }
            containers[currentContainerIndex].AddItem(currentItem);
        }


        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine("Container {0} enthält {1} Packstücke:", i + 1, containers[i].loadedItems);
            foreach (Item item in containers[i].contents)
            {
                Console.WriteLine("Packstück: Länge: {0}cm, Breite: {1}cm, Höhe: {2}cm", item.length, item.width, item.height);
            }
            Console.WriteLine("Aktuell genutzte Höhe: {0}cm", containers[i].usedHeight);
            Console.WriteLine("________________________");
        }

    }
}
