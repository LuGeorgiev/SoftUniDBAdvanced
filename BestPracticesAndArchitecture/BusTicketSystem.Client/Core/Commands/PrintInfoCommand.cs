using BusTicketsSystem.Client.Core.Contracts;
using BusTicketsSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Client.Core.Commands
{
    class PrintInfoCommand : ICommand
    {
        public string Execute(BusTicketsSystemContext db, string[] data)
        {
            int stationId = int.Parse(data[0]);

            var station = db.BusStations.Find(stationId);

            if (station==null)
            {
                return $"Station# {stationId} do not exist!";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Station: {station.Name}, {station.Town.Name}");
            sb.AppendLine($"Arrives:");
            foreach (var arrivinTrip in station.DestinationStationTrips)
            { 
            
                sb.AppendLine($"From: {arrivinTrip.OriginStation.Town.Name} | Arrive at: {arrivinTrip.ArrivalTime} | Status: {arrivinTrip.Status.ToString()}");
            }
            sb.AppendLine("Departures:");
            foreach (var departTrip in station.OriginStationTrips)
            {
                sb.AppendLine($"To: {departTrip.DestinationStation.Town.Name} | Depert at: {departTrip.DepertureTime} |  Status: {departTrip.Status.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
