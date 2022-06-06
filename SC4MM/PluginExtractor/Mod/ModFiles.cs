using System.Collections.Generic;

namespace SC4MM
{
    public class ModFiles
    {
        public LinkedList<string> Enabled { get; init; } = new();
        public LinkedList<string> Disabled { get; init; } = new();
        public List<string> Readme { get; init; } = new();
    }
}