using System.Collections.Generic;
using System.Linq;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public static class DapperExtensions
{
    private static readonly char[] separator = ['。', '；'];

    public static List<WorkInfo> ToWorks(this IEnumerable<dynamic> items)
    {
        var works = new List<WorkInfo>();
        foreach (var item in items)
        {
            var work = new WorkInfo();
            foreach (KeyValuePair<string, object> i in item)
            {
                switch (i.Key)
                {
                    case "Id":
                        work.Id = int.Parse(i.Value.ToString() ?? "0");
                        break;
                    case "Title":
                        work.Title = i.Value.ToString() ?? string.Empty;
                        break;
                    case "Author":
                        work.Author = i.Value.ToString() ?? string.Empty;
                        break;
                    case "Content":
                        work.Content = i.Value.ToString()?.Split(separator).FirstOrDefault() ?? "";
                        break;
                    case "Dynasty":
                        work.Dynasty = i.Value.ToString() ?? string.Empty;
                        break;
                    case "Translation":
                        work.Translation = i.Value.ToString() ?? string.Empty;
                        break;
                    case "Intro":
                        work.Intro = i.Value.ToString() ?? string.Empty;
                        break;
                }
            }

            works.Add(work);
        }

        return works;
    }
    
    public static List<Collection> ToCollections(this IEnumerable<dynamic> items)
    {
        var entities = new List<Collection>();
        foreach (var item in items)
        {
            var entity = new Collection();
            foreach (KeyValuePair<string, object> i in item)
            {
                switch (i.Key)
                {
                    case "Id":
                        entity.Id = int.Parse(i.Value.ToString() ?? "0");
                        break;
                    case "Name":
                        entity.Name = i.Value.ToString() ?? string.Empty;
                        break;
                }
            }

            entities.Add(entity);
        }

        return entities;
    }
    
    public static List<CollectionWork> ToCollectionWorks(this IEnumerable<dynamic> items)
    {
        var entities = new List<CollectionWork>();
        foreach (var item in items)
        {
            var entity = new CollectionWork();
            foreach (KeyValuePair<string, object> i in item)
            {
                switch (i.Key)
                {
                    case "Id":
                        entity.Id = int.Parse(i.Value.ToString() ?? "0");
                        break;
                    case "WorkId":
                        entity.WorkId = int.Parse(i.Value.ToString() ?? "0");
                        break;
                    case "CollectionId":
                        entity.CollectionId = int.Parse(i.Value.ToString() ?? "0");
                        break;
                }
            }

            entities.Add(entity);
        }

        return entities;
    }
}