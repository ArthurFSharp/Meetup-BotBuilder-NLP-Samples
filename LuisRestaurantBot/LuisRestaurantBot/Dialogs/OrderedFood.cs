namespace LuisRestaurantBot.Dialogs
{
    public class OrderedFood
    {
        public OrderedFood(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}