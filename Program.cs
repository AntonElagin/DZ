using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;


// Вариант F2 (Елагин Антон гр.Иу6-74)
namespace DZ
{

    class Author
    {
        public string Name { get; set; }
        public string Surname { get; set;  }
    }


    class Song
    {
       
        private List<string> styles;

        public string Name { get; set; }
        public DateTime Year { get; set; }
        public List<string> Styles { 
            get
            {
                return styles;
            } 
            set {
                if (value.Count >= 1)
                {
                    styles = value;
                    return;
                }
                Console.WriteLine("Песня должна относиться хотя бы к одному жанру!");
            }
        }
        public List<Author> Author { get; set; }
    }

    class Album
    {
        private List<Song> songs;

        public string Name { get; set; }
        public DateTime Year { get; set; }

        public List<Song> Songs
        {
            get
            {
                return songs;
            }
            set
            {
                if (value.Count > 1)
                {
                    songs = value;
                    return;
                }
                Console.WriteLine("Альбом должен содержать 2 и более песен!");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                AllowTrailingCommas = true
            };
            using (FileStream fs = new FileStream("Album.json", FileMode.OpenOrCreate))
            {
                var jack = new Author()
                {
                    Name = "Jack",
                    Surname = "Smith",
                };
                var snoopDogg = new Author()
                {
                    Name = "Snoop",
                    Surname = "Dogg",
                };
                var authors = new List<Author>() { jack, snoopDogg };

                var merryChristmas = new Song()
                {
                    Name = "Merry Christmas",
                    Year = new DateTime(),
                    Styles = new List<string>() { "folk", "rap", "rock" },
                    Author = authors,
                };

                var christmasTree = new Song()
                {
                    Name = "Christmas tree",
                    Year = new DateTime(),
                    Styles = new List<string>() { "rock", "morden" },
                    Author = new List<Author>(),
                };

                var album = new Album()
                {
                    Name = "Christmas",
                    Year = new DateTime(),
                    Songs = new List<Song>() { christmasTree, merryChristmas }
                };
                JsonSerializer.SerializeAsync<Album>(fs, album, options);
            }
        }
    }
}
