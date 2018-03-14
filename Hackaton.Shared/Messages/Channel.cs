﻿namespace Hackaton.Shared.Messages
{
    public class Channel
    {
        public Channel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}