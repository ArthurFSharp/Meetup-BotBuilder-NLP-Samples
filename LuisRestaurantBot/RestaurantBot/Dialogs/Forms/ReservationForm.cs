﻿using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Linq;

namespace RestaurantBot.Dialogs.Forms
{
    public class ReservationForm
    {
        public string RestaurantName { get; set; }
        public int? PeopleCount { get; set; }
        public DateTime? ReservationDate { get; set; }

        public static ReservationForm ReadFromLuis(LuisResult luisResult)
        {
            var form = new ReservationForm();

            if (luisResult.TryFindEntity("RestaurantName", out var restaurantNameEntity))
            {
                form.RestaurantName = restaurantNameEntity.Entity;
            }

            var countEntity = luisResult.Entities.Where(e => e.Type == "builtin.number").Skip(1).FirstOrDefault();
            if (countEntity != null)
            {
                form.PeopleCount = int.Parse(countEntity.Entity);
            }

            var reservationDateEntity = luisResult.Entities.Where(e => e.Type == "builtin.datetimeV2.time").Skip(1).FirstOrDefault();
            if (reservationDateEntity != null)
            {
                form.ReservationDate = LuisHelper.ResolveDateTime(reservationDateEntity);
            }

            return form;
        }
    }
}