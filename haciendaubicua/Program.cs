// See https://aka.ms/new-console-template for more information

using haciendaubicua.Device;
using haciendaubicua.Singleton;
using Serilog;

ILogger logger = CustomLogger.GetInstance();
Console.WriteLine("Iniciando App, Bienvenido");

#region TaskToRun
BancaDeLoteria bancaDeLoteria = new();
Hidrocarburo hidrocarburos = new();
AiresAcondicionado airesAcondicionado = new();
GPS gPS = new();
PensionEvent pensionEvent = new();

Task banca =  bancaDeLoteria.GenerateAsync();
Task hidrocarburo = hidrocarburos.GenerateAsync();
Task aires = airesAcondicionado.GenerateAsync();
Task gps = gPS.GenerateAsync();
Task pension = pensionEvent.GenerateAsync();

await banca;
await hidrocarburo;
await aires;
await gps;
await pension;

#endregion


logger.Information("Terminating the application...");

