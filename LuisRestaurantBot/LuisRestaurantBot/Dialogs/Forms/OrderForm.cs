using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuisRestaurantBot.Dialogs.Forms
{
    [Serializable]
    public class OrderForm
    {
        public List<string> Items { get; set; } = new List<string>();
        public List<int> Counts { get; set; } = new List<int>();

        public static OrderForm ReadFromLuis(LuisResult luisResult)
        {
            var form = new OrderForm();

            var foods = luisResult.Entities.Where(e => e.Type == "Food").ToList();
            var quantities = luisResult.Entities.Where(e => e.Type == "builtin.number").ToList();

            if (foods.Count > 0)
            {
                foreach (var item in foods)
                {
                    form.Items.Add(item.Entity);
                }
            }

            if (quantities.Count > 0)
            {
                foreach (var item in quantities)
                {
                    form.Counts.Add(LuisHelper.ResolveQuantity(item));
                }
            }

            return form;
        }
    }
}