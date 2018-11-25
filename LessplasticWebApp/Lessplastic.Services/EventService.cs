﻿using Lessplastic.Models;
using Lessplastic.Models.ViewModels.Events;
using Lessplastic.Services.Contracts;
using LessplasticWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lessplastic.Services
{
    public class EventService : IEventService
    {
        private ApplicationDbContext context;

        public EventService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateEvent(EventViewModel model)
        {
            var towns = model.Towns.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            
            var myEvent = new Event
            {
                Name = model.Name,
                Description = model.Description,
                EventDate = model.EventDate
            };

            foreach (var town in towns)
            {
                var townExists = this.context.Towns.FirstOrDefault(x => x.TownName == town);
                if (townExists != null)
                {
                    EventTowns eventTowns = new EventTowns
                    {
                        Event = myEvent,
                        Town = townExists
                    };
                    
                    this.context.EventsTowns.Add(eventTowns);
                    this.context.SaveChanges();
                }
                else
                {
                    Town townForEvent = new Town
                    {
                        TownName = town
                    };

                    EventTowns eventTowns = new EventTowns
                    {
                        Event = myEvent,
                        Town = townForEvent
                    };
                    
                    this.context.Towns.Add(townForEvent);
                    this.context.EventsTowns.Add(eventTowns);
                    this.context.SaveChanges();
                }
            }
        }

        public void DeleteEvent(Event myEvent)
        {
            this.context.Events.Remove(myEvent);
            this.context.SaveChanges();
        }

        public void EditEvent(Event myEvent, UpdateDeleteEventViewModel model)
        {
            myEvent.Name = model.Name;
            myEvent.Description = model.Description;
            myEvent.EventDate = model.EventDate;

            this.context.SaveChanges();
        }

        public Event GetEvent(int id)
        {
            var myEvent = this.context.Events.FirstOrDefault(x => x.Id == id);

            return myEvent;
        }

        public Event[] GetEvents()
        {
            var myEvents = this.context.Events.ToArray();

            return myEvents;
        }

        public void IncrementParticipants(Event myEvent)
        {
            throw new NotImplementedException();
        }
    }
}
