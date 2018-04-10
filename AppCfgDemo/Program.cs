﻿using AppCfg;
using System;
using System.Collections.Generic;

namespace AppCfgDemo
{
    public interface ISetting
    {
        [Option(DefaultValue = 99)] int DemoInt { get; }
        [Option(Alias = "long-key")] long DemoLong { get; }
        List<int> Numbers { get; }
        Guid ThisIsGuid { get; }
    }

    public interface IJsonSetting
    {
        [Option(Alias = "cute_animal")] Animal CuteAnimal { get; }
    }

    public interface IJsonSettingWithCustomJsonSetting
    {
        [Option(Alias = "test_setting_with_js_config")] Machine Optimus { get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // register custom type parser
            MyAppCfg.TypeParserFactory.AddParser(new ListIntParser());

            // setup json serializer settings
            MyAppCfg.JsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                DateFormatString = "dd+MM+yyyy" // ex: setup default format
            };

            Console.WriteLine($"DemoInt: {MyAppCfg.Get<ISetting>().DemoInt}");
            Console.WriteLine($"DemoLong: {MyAppCfg.Get<ISetting>().DemoLong}");
            Console.WriteLine($"ThisIsGuid: {MyAppCfg.Get<ISetting>().ThisIsGuid}\n");

            Console.WriteLine($"Numbers:");
            foreach (var num in MyAppCfg.Get<ISetting>().Numbers)
            {
                Console.WriteLine($"   + {num}");
            }

            Console.WriteLine($"\nAnimal - Name: {MyAppCfg.Get<IJsonSetting>().CuteAnimal.Name}");
            Console.WriteLine($"Animal - Legs: {MyAppCfg.Get<IJsonSetting>().CuteAnimal.Legs}");
            Console.WriteLine($"Animal - CanSwim: {MyAppCfg.Get<IJsonSetting>().CuteAnimal.CanSwim}");
            Console.WriteLine($"Animal - SampleDay: {MyAppCfg.Get<IJsonSetting>().CuteAnimal.SampleDay:MMM dd, yyyy}\n");

            Console.WriteLine($"Machine - DayWithNewFormat: {MyAppCfg.Get<IJsonSettingWithCustomJsonSetting>().Optimus.DayWithNewFormat:MM-dd-yyyy}");

            Console.ReadKey();
        }
    }
}
