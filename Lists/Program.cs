using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of flights
            var flights = new List<Flight>
            {
                new Flight("FL123", "New York", "A1", "10:00 AM", "Departed"),
                new Flight("FL456", "London", "A2", "12:00 PM", "Delayed"),
                new Flight("FL789", "Tokyo", "A1", "03:00 PM", "Cancelled"),
                new Flight("FL101", "Sydney", "A3", "08:00 PM", "Boarding"),
            };

            // Create and display the table of flights
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Green)
                .AddColumn("[yellow]Flight No.[/]")
                .AddColumn("[yellow]Destination[/]")
                .AddColumn("[yellow]Gate[/]")
                .AddColumn("[yellow]Depart Time[/]")
                .AddColumn("[yellow]Status[/]");


            foreach (var flight in flights)
            {
                table.AddRow(flight.FlightNumber, flight.Destination, flight.Gate, flight.DepartureTime, flight.Status);
            }

            var rule = new Rule("[yellow]Available Flights[/]")
                .LeftJustified()
                .RuleStyle("green");
            AnsiConsole.Write(rule);

            AnsiConsole.Write(table);

            // Prompt the user to select a flight (row in the table)
            var flightSelection = AnsiConsole.Prompt(
                new SelectionPrompt<Flight>()
                    .Title("Select a flight to book:")
                    .PageSize(10)
                    .AddChoices(flights)
                    .UseConverter(flight =>
                        $"{flight.FlightNumber} - {flight.Destination} {flight.Gate} ({flight.DepartureTime}) {flight.Status}")
            );

            AnsiConsole.Clear();

            // Display the selected flight details
            AnsiConsole.MarkupLine($"[bold green]You have selected the following flight:[/]");
            AnsiConsole.MarkupLine($"[blue]Flight Number:[/] {flightSelection.FlightNumber}");
            AnsiConsole.MarkupLine($"[blue]Destination:[/] {flightSelection.Destination}");
            AnsiConsole.MarkupLine($"[blue]Gate:[/] {flightSelection.Gate}");
            AnsiConsole.MarkupLine($"[blue]Departure Time:[/] {flightSelection.DepartureTime}");
            AnsiConsole.MarkupLine($"[blue]Status:[/] {flightSelection.Status}");

            var thankYouRule = new Rule("[yellow]Thank you for booking![/]")
                .Centered()
                .RuleStyle("green");

            AnsiConsole.Write(thankYouRule);

            // Create a list of Items, apply separate styles to each
            var rows = new List<Text>()
            {
                new Text("Item 1", new Style(Color.Red, Color.Black)),
                new Text("Item 2", new Style(Color.Green, Color.Black)),
                new Text("Item 3", new Style(Color.Blue, Color.Black))
            };

// Renders each item with own style
            AnsiConsole.Write(new Rows(rows));

            Console.ReadKey();
        }
    }

    // Define a Flight class
    public record Flight(string FlightNumber, string Destination, string Gate, string DepartureTime, string Status);
}