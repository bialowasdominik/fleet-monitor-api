﻿using FleetMonitorAPI.Entities;
using System.Timers;

namespace FleetMonitorAPI.Utilities
{
    public class ScheduleGenerator
    {

        private readonly FleetMonitorDbContext _dbContext;
        private static System.Timers.Timer _timer;
        private int i = 0;
        private double[][] testData =
            {
                new double[]{ 54.35163168059999, 18.64930611126887, 2 },
                new double[]{ 52.22563057891971, 21.00697511603385, 1 },
                new double[]{ 52.22572447024569, 21.006919765385838, 1},
                new double[]{ 52.22559406557265, 21.00630665051562, 1 },
                new double[]{ 52.22534107941472, 21.005118740454567, 1 },
                new double[]{ 52.22510113244402, 21.00405004710359, 1 },
                new double[]{ 52.225030712979446, 21.003722200951756, 1 },
                new double[]{ 52.225377592971135, 21.00355189113033, 1 },
                new double[]{ 52.22576098349381, 21.003343261699, 1 },
                new double[]{ 52.226739005538455, 21.002700342596516, 1 },
                new double[]{ 52.227503151783125, 21.002227733152324, 1 },
                new double[]{ 52.22854372568854, 21.00155075208326, 1 },
                new double[]{ 52.229435626762736, 21.000954668150015, 1 },
                new double[]{ 52.23028056914374, 21.00047780100457, 1 },
                new double[]{ 52.23047093974202, 21.0006055332692, 1 },
                new double[]{ 52.230590898885694, 21.00102279206536, 1 },
                new double[]{ 52.23077605256772, 21.001921175659923, 1 },
                new double[]{ 52.23080473828038, 21.002253279547958, 1 },
                new double[]{ 52.230841247342376, 21.002568352467375, 1 },
                new double[]{ 52.23100553775613, 21.003330488416314, 1 },
                new double[]{ 52.2311046332635, 21.003854190701293, 1 },
                new double[]{ 52.2306952109205, 21.00408836651978, 1 },
                new double[]{ 52.23048919442818, 21.00424590297949, 1 },
                new double[]{ 52.23023102050217, 21.004369377555133, 1 },
                new double[]{ 52.229696413175354, 21.004675934990246, 1 },
                new double[]{ 52.229696413175354, 21.004675934990246, 1 },
                new double[]{ 52.229446058247746, 21.004833471500113, 1 },
                new double[]{ 52.229446058247746, 21.004833471500113, 1 },
                new double[]{ 52.228757574958365, 21.005216668294004, 1 },
                new double[]{ 52.22861153166578, 21.00526350348595, 1 },
                new double[]{ 52.22861153166578, 21.00526350348595, 1 },
                new double[]{ 52.22861153166578, 21.00526350348595, 1 },
                new double[]{ 52.22840028963942, 21.005446586398584, 1 },
                new double[]{ 52.22861361972988, 21.006291244894932, 1 },
                new double[]{ 52.22884833190267, 21.007508959151064, 1 },
                new double[]{ 52.228983942814736, 21.008028403693892, 1 },
                new double[]{ 52.229233022969, 21.009266948638142, 1 },
                new double[]{ 52.229501634167335, 21.01050169402456, 1 },
                new double[]{ 52.229553791491945, 21.01140007761912, 1 },
                new double[]{ 52.22954382946574, 21.011644233490305, 1 },
                new double[]{ 52.22954382946574, 21.011644233490305, 1 },
                new double[]{ 52.22962682572286, 21.012097651767405, 1 },
                new double[]{ 52.23002903615553, 21.012066381541402, 1 },
                new double[]{ 52.23002903615553, 21.012066381541402, 1 },
                new double[]{ 52.23027163751181, 21.011831854846346, 1 },
                new double[]{ 52.230728107547336, 21.011336742919543, 1 },
                new double[]{ 52.230728107547336, 21.011336742919543, 1 },
                new double[]{ 52.23143674400794, 21.010935441680772, 1 },
                new double[]{ 52.23143674400794, 21.010935441680772, 1 },
                new double[]{ 52.23180382601872, 21.010716550071432, 1 },
                new double[]{ 52.23180382601872, 21.010716550071432, 1 },
                new double[]{ 52.232285816047394, 21.01039863610885, 1 },
                new double[]{ 52.232285816047394, 21.01039863610885, 1 },
                new double[]{ 52.232991235726054, 21.00998169974253, 1 },
                new double[]{ 52.232991235726054, 21.00998169974253, 1 },
                new double[]{ 52.233920074407386, 21.00944489417333, 1 },
                new double[]{ 52.234513754937545, 21.0090904982208, 1 },
                new double[]{ 52.23521913920067, 21.00871525550871, 1 },
                new double[]{ 52.23521913920067, 21.00871525550871, 1 },
                new double[]{ 52.23518083808052, 21.0083817064313, 1 },
                new double[]{ 52.234615892719454, 21.008725678917383, 1 },
                new double[]{ 52.234615892719454, 21.008725678917383, 1 },
                new double[]{ 52.23368289264635, 21.009295618805073, 1 },
                new double[]{ 52.23368289264635, 21.009295618805073, 1 },
                new double[]{ 52.2326162637174, 21.009927179974216, 1 },
                new double[]{ 52.2326162637174, 21.009927179974216, 1 },
                new double[]{ 52.23240918626987, 21.010042009266257, 1 },
                new double[]{ 52.23230610768337, 21.00997850370273, 1 },
                new double[]{ 52.23230610768337, 21.00997850370273, 1 },
                new double[]{ 52.23221038376606, 21.009762534350926, 1 },
                new double[]{ 52.23221038376606, 21.009762534350926, 1 },
                new double[]{ 52.23219297939527, 21.009555090105128, 1 },
                new double[]{ 52.23206940816651, 21.009293653521375, 1 },
                new double[]{ 52.2317335015313, 21.009404479865783, 1 },
            };

        public ScheduleGenerator(FleetMonitorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GenerateScheduleData()
        {
            _timer = new System.Timers.Timer(1500);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            if (i < testData.Length)
            {
                _dbContext.Positions.Add(new Positions() { Latitude = testData[i][0],Longitude = testData[i][1], DeviceId = (long)testData[i][2], Time=DateTime.Now});
                _dbContext.SaveChanges();
                Console.WriteLine($"Data generated - Latitude: {testData[i][0]}, Longitude: {testData[i][1]}, Device: {testData[i][2]}, Time: {DateTime.Now}");
            }
            i++;
        }
    }
}
